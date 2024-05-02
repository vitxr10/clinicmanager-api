using ClinicManager.Application.DTOs;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Services
{
    public class CalendarEventsService : ICalendarEventsService
    {
        private readonly IConfiguration _configuration;
        public CalendarEventsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        const string CALENDAR_ID = "primary";

        private async Task<CalendarService> ConnectGoogleCalendar(string[] scopes)
        {
            string applicationName = "Clinic Manager";
            UserCredential credential;
            var directory = _configuration["ApplicationLocation:Location"];

            using (var stream = new FileStream(Path.Combine(directory, "Services", "calendarcredential.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = $"{directory}\\CalendarAuthToken";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                );
            }

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return services;
        }

        public async Task<Event> CreateEvent(CalendarEventDTO request)
        {
            string[] scopes = { "https://www.googleapis.com/auth/calendar " };
            var services = await ConnectGoogleCalendar(scopes);

            Event eventCalendar = new Event()
            {
                Summary = request.Summary,
                Location = request.Location,
                Start = new EventDateTime
                {
                    DateTimeDateTimeOffset = request.Start.AddHours(3),
                },
                End = new EventDateTime
                {
                    DateTimeDateTimeOffset = request.End.AddHours(3),
                },
                Description = request.Description,
                Attendees = new List<EventAttendee>
                {
                    new EventAttendee { Email = request.PatientEmail },
                    new EventAttendee { Email = request.DoctorEmail },
                },
                ConferenceData = new ConferenceData()
                {
                    CreateRequest = new CreateConferenceRequest()
                    {
                        ConferenceSolutionKey = new ConferenceSolutionKey()
                        {
                            Type = "hangoutsMeet"
                        },
                        RequestId = Guid.NewGuid().ToString()
                    },
                },
            };

            var eventRequest = services.Events.Insert(eventCalendar, CALENDAR_ID);
            eventRequest.ConferenceDataVersion = 1;
            var createdEvent = await eventRequest.ExecuteAsync();

            return createdEvent;
        }
    }
}
