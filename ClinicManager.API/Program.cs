using ClinicManager.Application.Commands.CreatePatient;
using ClinicManager.Application.Mappers;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientCommand>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// context
builder.Services.AddDbContext<ClinicManagerDbContext>(options => options.UseInMemoryDatabase("Database"));

// mediatR
var myHandlers = AppDomain.CurrentDomain.Load("ClinicManager.Application");
builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblies(myHandlers));

// fluentValidation

// automapper
builder.Services.AddAutoMapper(typeof(PatientMapper));

// interfaces
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// auth
builder.Services.AddSwaggerGen(c =>
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

builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

var dbContext = builder.Services.AddDbContext<ClinicManagerDbContext>(options => options.UseInMemoryDatabase("Database")).BuildServiceProvider().GetService<ClinicManagerDbContext>();

dbContext.Database.EnsureCreated();


// Se o usuário não existir, pré-cadastre-o

    var newUser = new User
    {
        FirstName = "recep",
        LastName = "sionista",
        CPF = "19328324084",
        Birthday = DateTime.Now.AddYears(-18),
        Phone = "11987655678",
        Email = "vitor@email.com",
        Password = "0ec1e4da4796ebea025b56a83687054c7e7dfe8da80a3f1507e847e117846869",
        Role = RoleEnum.Receptionist,
        BloodType = BloodTypeEnum.ABPositivo,
        Height = 190,
        Weight = 80
    };

    dbContext.Users.Add(newUser);
    dbContext.SaveChanges();


var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
