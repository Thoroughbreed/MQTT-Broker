using Microsoft.EntityFrameworkCore;
using MQTTAPI.Data;
using MQTTAPI.Helpers;
using MQTTAPI.Model.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<APIContext>();
builder.Services.AddScoped<IMQTTService, MQTTService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var config = ConfigHelper.ReadConfig();
var port = config.Port;

app.Urls.Add($"http://*:{port}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Smoke test
app.MapGet("/", () => "Hello World.");

app.MapGet("/all", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync());

app.MapGet("/info", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("info"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync());

app.MapGet("/debug", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("debug"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync());

app.MapGet("/critical", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("critical"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync());
// app.MapGet("/publish", async (IMQTTService service) => await service.Publish());

app.Run();