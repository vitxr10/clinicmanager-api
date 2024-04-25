using ClinicManager.Application.Commands.CreatePatient;
using ClinicManager.Application.Mappers;
using ClinicManager.Application.Services;
using ClinicManager.Core.Entities;
using ClinicManager.Core.Enums;
using ClinicManager.Core.Repositories;
using ClinicManager.Core.Services;
using ClinicManager.Infrastructure.Persistence.Context;
using ClinicManager.Infrastructure.Persistence.Repositories;
using ClinicManager.Infrastructure.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ClinicManager.API.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region
            //services.AddDbContext<ClinicManagerDbContext>(options => options.UseInMemoryDatabase("Database"));
            //var dbContext = services.AddDbContext<ClinicManagerDbContext>(options => options.UseInMemoryDatabase("Database")).BuildServiceProvider().GetService<ClinicManagerDbContext>();

            //dbContext.Database.EnsureCreated();

            //    var newUser = new User
            //    {
            //        FirstName = "recep",
            //        LastName = "sionista",
            //        CPF = "19328324084",
            //        Birthday = DateTime.Now.AddYears(-18),
            //        Phone = "11987655678",
            //        Email = "vitor@email.com",
            //        Password = "0ec1e4da4796ebea025b56a83687054c7e7dfe8da80a3f1507e847e117846869",
            //        Role = RoleEnum.Receptionist,
            //        BloodType = BloodTypeEnum.ABPositivo,
            //        Height = 190,
            //        Weight = 80
            //    };

            //    dbContext.Users.Add(newUser);
            //    dbContext.SaveChanges();
            #endregion

            var connectionString = configuration.GetConnectionString("ClinicManagerDb");
            services.AddDbContext<ClinicManagerDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var myHandlers = AppDomain.CurrentDomain.Load("ClinicManager.Application");
            services.AddMediatR(m => m.RegisterServicesFromAssemblies(myHandlers));

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientCommand>());

            services.AddAutoMapper(typeof(PatientMapper));

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<ICalendarEventsService, CalendarEventsService>();

            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClinicManager.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usando o esquema Bearer."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                      new OpenApiSecurityScheme
                      {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                      },
                      new string[] {}
                   }
                });
            });

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            return services;
        }


    }
}
