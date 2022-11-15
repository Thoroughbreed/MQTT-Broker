using System.Globalization;
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
builder.Services.AddSwaggerGen();

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

var config = ConfigHelper.ReadConfig();
var port = config.Port;
var secPort = config.SecPort;

app.Urls.Add($"http://*:{port}");
// app.Urls.Add($"https://*:{secPort}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var decimalPoint = new NumberFormatInfo
{
    CurrencyDecimalSeparator = "."
};

// Smoke test
app.MapGet("/", () => "Hello World.");

app.MapGet("/all", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync()).RequireAuthorization();

app.MapGet("/info", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("info"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync()).RequireAuthorization();

app.MapGet("/debug", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("debug"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync());

app.MapGet("/system", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("system"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync()).RequireAuthorization();

app.MapGet("/critical", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("critical"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync()).RequireAuthorization();

app.MapGet("/kitchen", async (APIContext db, DateTime ts) =>
{
    List<Measurements> result = new();
    var temp = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("kitchen/temp"))
        .Where(m => m.Timestamp > ts.AddDays(-1))
        .OrderByDescending(m => m.Id)
        .ToListAsync();
    var humid = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("kitchen/humid"))
        .Where(m => m.Timestamp > ts.AddDays(-1))
        .OrderByDescending(m => m.Id)
        .ToListAsync();
    try
    {
        for (int i = 0; i < temp.Count && i < humid.Count; i++)
        {
            result.Add(new Measurements
            {
                Temperature = Convert.ToDecimal(temp[i].Message, decimalPoint), 
                Humidity = Convert.ToDouble(humid[i].Message, decimalPoint),
                Timestamp = humid[i].Timestamp
            });
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    return result;
}).RequireAuthorization();

app.MapGet("/kitchen/1", async (APIContext db) =>
{
    Measurements result = new();
    var temp = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("kitchen/temp"))
        .OrderByDescending(m => m.Id)
        .FirstOrDefaultAsync();
    var humid = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("kitchen/humid"))
        .OrderByDescending(m => m.Id)
        .FirstOrDefaultAsync();
    try
    {
        result.Temperature = Convert.ToDecimal(temp.Message, decimalPoint);
        result.Humidity = Convert.ToDouble(humid.Message, decimalPoint);
        result.Timestamp = temp.Timestamp;
    }
    catch (Exception)
    {
        // ignored
    }
    return result;
}).RequireAuthorization();

app.MapGet("/bedroom/1", async (APIContext db) =>
{
    Measurements result = new();
    var temp = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("bedroom/temp"))
        .OrderByDescending(m => m.Id)
        .FirstOrDefaultAsync();
    var humid = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("bedroom/humid"))
        .OrderByDescending(m => m.Id)
        .FirstOrDefaultAsync();
    try
    {
        result.Temperature = Convert.ToDecimal(temp.Message, decimalPoint);
        result.Humidity = Convert.ToDouble(humid.Message, decimalPoint);
        result.Timestamp = temp.Timestamp;
    }
    catch (Exception)
    {
        // ignored
    }
    return result;
}).RequireAuthorization();

app.MapGet("/livingroom/1", async (APIContext db) =>
{
    Measurements result = new();
    var temp = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("livingroom/temp"))
        .OrderByDescending(m => m.Id)
        .FirstOrDefaultAsync();
    var humid = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("livingroom/humid"))
        .OrderByDescending(m => m.Id)
        .FirstOrDefaultAsync();
    try
    {
        result.Temperature = Convert.ToDecimal(temp.Message, decimalPoint);
        result.Humidity = Convert.ToDouble(humid.Message, decimalPoint);
        result.Timestamp = temp.Timestamp;
    }
    catch (Exception)
    {
        // ignored
    }
    return result;
}).RequireAuthorization();

app.MapGet("/bedroom", async (APIContext db, DateTime ts) =>
{
    List<Measurements> result = new();
    var temp = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("bedroom/temp"))
        .Where(m => m.Timestamp > ts.AddDays(-1))
        .OrderByDescending(m => m.Id)
        .ToListAsync();
    var humid = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("bedroom/humid"))
        .Where(m => m.Timestamp > ts.AddDays(-1))
        .OrderByDescending(m => m.Id)
        .ToListAsync();
    try
    {
        for (int i = 0; i < temp.Count && i < humid.Count ; i++)
        {
            result.Add(new Measurements
            {
                Temperature = Convert.ToDecimal(temp[i].Message, decimalPoint), 
                Humidity = Convert.ToDouble(humid[i].Message, decimalPoint),
                Timestamp = humid[i].Timestamp
            });
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    return result;
}).RequireAuthorization();

app.MapGet("/livingroom", async (APIContext db, DateTime ts) =>
{
    List<Measurements> result = new();
    var temp = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("livingroom/temp"))
        .Where(m => m.Timestamp > ts.AddDays(-1))
        .OrderByDescending(m => m.Id)
        .ToListAsync();
    var humid = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("livingroom/humid"))
        .Where(m => m.Timestamp > ts.AddDays(-1))
        .OrderByDescending(m => m.Id)
        .ToListAsync();
    try
    {
        for (int i = 0; i < temp.Count && i < humid.Count; i++)
        {
            result.Add(new Measurements
            {
                Temperature = Convert.ToDecimal(temp[i].Message, decimalPoint), 
                Humidity = Convert.ToDouble(humid[i].Message, decimalPoint),
                Timestamp = humid[i].Timestamp
            });
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    return result;
}).RequireAuthorization();

app.MapGet("/airq", async (APIContext db) =>
{
    var qual = await db.Messages
        .AsNoTracking()
        .Where(m => m.Topic.Contains("airquality"))
        .OrderByDescending(m => m.Id)
        .Take(30)
        .ToListAsync();
    
    List<AirQuality> result = qual.Select(t => new AirQuality { Quality = Convert.ToInt16(t.Message), Timestamp = t.Timestamp }).ToList();

    return result;
}).RequireAuthorization();

app.Run();