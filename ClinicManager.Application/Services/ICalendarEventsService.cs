using ClinicManager.Application.DTOs;
using Google.Apis.Calendar.v3.Data;

namespace ClinicManager.Application.Services
{
    public interface ICalendarEventsService
    {
        Task<Event> CreateEvent(CalendarEventDTO request);
    }
}