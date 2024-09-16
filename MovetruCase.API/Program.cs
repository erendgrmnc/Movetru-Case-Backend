using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovetruCase.Business.Abstract;
using MovetruCase.Business.Concrete;
using MovetruCase.Core.Helpers;
using MovetruCase.Core.Helpers.Authentication;
using MovetruCase.DataAccessLayer.Abstract.Repositories;
using MovetruCase.DataAccessLayer.Concrete.Repositories;
using MovetruCaseDataAccessLayer.Concrete;
using NLog.Web;


var builder = WebApplication.CreateBuilder(args);

var firebaseProjectName = "movetru-case";


Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "firebase-config.json");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddScoped<IAuthHelper, FirebaseAuthenticationHelper>();

builder.Services.AddScoped<MovetruContext>();

builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();
builder.Services.AddScoped<IDailyStepLogRepository, DailyStepLogRepository>();

builder.Services.AddScoped<IStepService, StepManager>();
builder.Services.AddScoped<IUserDataService, UserDataManager>();



builder.Services.AddSingleton(FirebaseApp.Create());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://securetoken.google.com/{firebaseProjectName}";
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = $"https://securetoken.google.com/{firebaseProjectName}",
            ValidAudience = firebaseProjectName,
        };
    });


var app = builder.Build();

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
