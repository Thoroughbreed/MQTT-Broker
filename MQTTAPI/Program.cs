using System.Globalization;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MQTTAPI.Data;
using MQTTAPI.Helpers;
using MQTTAPI.Model;
using MQTTAPI.Model.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
#pragma warning disable CS8602

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<APIContext>();
builder.Services.AddScoped<IMQTTService, MQTTService>();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://tved-it.eu.auth0.com/";
    options.Audience = "https://mqtt-api.tved.it";
});

var app = builder.Build();
app.MapControllers();

app.Services.GetService<DbContext>()?.Database.EnsureCreated();

var config = ConfigHelper.ReadConfig();
var port = config.Port;
var secPort = config.SecPort;

app.Urls.Add($"http://*:{port}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();