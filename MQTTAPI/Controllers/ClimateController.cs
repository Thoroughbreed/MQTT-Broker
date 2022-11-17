using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQTTAPI.Data;
using MQTTAPI.Model;

namespace MQTTAPI.Controllers;

[Authorize]
[ApiController]
public class ClimateController
{
    private readonly NumberFormatInfo _decimalPoint;
    public ClimateController()
    {
        _decimalPoint = new NumberFormatInfo
        {
            CurrencyDecimalSeparator = "."
        };
    }
    
    /// <summary>
    /// Get all temperature and humidity readings from the kitchen since ts
    /// </summary>
    /// <param name="ts">Timestamp (format: yyyy-MM-dd HH:mm:ss)</param>
    [Route("/kitchen")]
    [HttpGet]
    public async Task<List<Measurements>> GetKitchen([FromServices] APIContext db, DateTime ts)
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
                    Temperature = Convert.ToDecimal(temp[i].Message, _decimalPoint), 
                    Humidity = Convert.ToDouble(humid[i].Message, _decimalPoint),
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
    }
    
    /// <summary>
    /// Get last temperature and humidity reading from the kitchen
    /// </summary>
    [Route("/kitchen/1")]
    [HttpGet]
    public async Task<Measurements> GetSingleKitchen([FromServices] APIContext db)
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
            result.Temperature = Convert.ToDecimal(temp.Message, _decimalPoint);
            result.Humidity = Convert.ToDouble(humid.Message, _decimalPoint);
            result.Timestamp = temp.Timestamp;
        }
        catch (Exception)
        {
            // ignored
        }
        return result;
    }
    
    /// <summary>
    /// Get all temperature and humidity readings from the bedroom since ts
    /// </summary>
    /// <param name="ts">Timestamp (format: yyyy-MM-dd HH:mm:ss)</param>
    [Route("/bedroom")]
    [HttpGet]
    public async Task<List<Measurements>> GetBedroom([FromServices] APIContext db, DateTime ts)
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
                    Temperature = Convert.ToDecimal(temp[i].Message, _decimalPoint), 
                    Humidity = Convert.ToDouble(humid[i].Message, _decimalPoint),
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
    }
    
    /// <summary>
    /// Get last temperature and humidity reading from the bedroom
    /// </summary>
    [Route("/bedroom/1")]
    [HttpGet]
    public async Task<Measurements> GetSingleBedroom([FromServices] APIContext db)
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
            result.Temperature = Convert.ToDecimal(temp.Message, _decimalPoint);
            result.Humidity = Convert.ToDouble(humid.Message, _decimalPoint);
            result.Timestamp = temp.Timestamp;
        }
        catch (Exception)
        {
            // ignored
        }
        return result;
    }
    
    /// <summary>
    /// Get all temperature and humidity readings from the living room since ts
    /// </summary>
    /// <param name="ts">Timestamp (format: yyyy-MM-dd HH:mm:ss)</param>
    [Route("/livingroom")]
    [HttpGet]
    public async Task<List<Measurements>> GetLivingRoom([FromServices] APIContext db, DateTime ts)
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
                    Temperature = Convert.ToDecimal(temp[i].Message, _decimalPoint), 
                    Humidity = Convert.ToDouble(humid[i].Message, _decimalPoint),
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
    }
    
    /// <summary>
    /// Get last temperature and humidity reading from the living room
    /// </summary>
    [Route("/livingroom/1")]
    [HttpGet]
    public async Task<Measurements> GetSingleLivingRoom([FromServices] APIContext db)
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
            result.Temperature = Convert.ToDecimal(temp.Message, _decimalPoint);
            result.Humidity = Convert.ToDouble(humid.Message, _decimalPoint);
            result.Timestamp = temp.Timestamp;
        }
        catch (Exception)
        {
            // ignored
        }
        return result;
    }

    /// <summary>
    /// Get all air quality readings since ts
    /// </summary>
    /// <param name="ts">Timestamp (format: yyyy-MM-dd HH:mm:ss)</param>
    [Route("/airq")]
    [HttpGet]
    public async Task<List<AirQuality>> GetAirQuality([FromServices] APIContext db, DateTime ts)
    {
        var qual = await db.Messages
                           .AsNoTracking()
                           .Where(m => m.Topic.Contains("airquality"))
                           .OrderByDescending(m => m.Id)
                           .Take(30)
                           .ToListAsync();
    
        List<AirQuality> result = qual.Select(t => new AirQuality { Quality = Convert.ToInt16(t.Message), Timestamp = t.Timestamp }).ToList();

        return result;
    }

    /// <summary>
    /// Get last air quality reading
    /// </summary>
    [Route("/airq/1")]
    [HttpGet]
    public async Task<AirQuality> GetSingleQuality([FromServices] APIContext db)
    {
        var qual = await db.Messages
                           .AsNoTracking()
                           .Where(m => m.Topic.Contains("airquality"))
                           .OrderByDescending(m => m.Id)
                           .FirstOrDefaultAsync();
        return qual != null 
               ? new AirQuality { Quality = Convert.ToInt16(qual.Message), Timestamp = qual.Timestamp } 
               : new AirQuality{ Quality = 0, Timestamp = DateTime.Now };
    }
}