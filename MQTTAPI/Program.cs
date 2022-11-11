using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MQTTAPI.Data;
using MQTTAPI.Helpers;
using MQTTAPI.Model;
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
var secPort = config.SecPort;

app.Urls.Add($"http://*:{port}");
// app.Urls.Add($"https://*:{secPort}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

app.MapGet("/system", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("system"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync());

app.MapGet("/critical", async (APIContext db) => await db.Messages
    .AsNoTracking()
    .Where(m => m.Topic.Contains("critical"))
    .OrderByDescending(m => m.Id)
    .Take(50)
    .ToListAsync());

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
        for (int i = 0; i < temp.Count; i++)
        {
            result.Add(new Measurements
            {
                Temperature = Convert.ToDecimal(temp[i].Message, decimalPoint), 
                Humidity = Convert.ToDouble(humid[i].Message, decimalPoint)
            });
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    return result;
});

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
        for (int i = 0; i < temp.Count; i++)
        {
            result.Add(new Measurements
            {
                Temperature = Convert.ToDecimal(temp[i].Message, decimalPoint), 
                Humidity = Convert.ToDouble(humid[i].Message, decimalPoint)
            });
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    return result;
});

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
        for (int i = 0; i < temp.Count; i++)
        {
            result.Add(new Measurements
            {
                Temperature = Convert.ToDecimal(temp[i].Message, decimalPoint), 
                Humidity = Convert.ToDouble(humid[i].Message, decimalPoint)
            });
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    return result;
});
// app.MapGet("/publish", async (IMQTTService service) => await service.Publish());

app.Run();