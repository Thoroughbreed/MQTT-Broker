using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQTTAPI.Data;
using MQTTAPI.Model;

namespace MQTTAPI.Controllers;

[ApiController]
[Authorize]
public class LogController
{
    /// <summary>
    /// Get last 50 logs
    /// </summary>
    [Route("/all")]
    [HttpGet]
    public Task<List<LogMessage>> GetAll([FromServices] APIContext db)
    {
        return db.Messages
                 .AsNoTracking()
                 .OrderByDescending(m => m.Id)
                 .Take(50)
                 .ToListAsync();
    }
    
    /// <summary>
    /// Get last 50 logs with log level info
    /// </summary>
    [Route("/info")]
    [HttpGet]
    public Task<List<LogMessage>> GetInfo([FromServices] APIContext db)
    {
        return db.Messages
                 .AsNoTracking()
                 .Where(m => m.Topic.Contains("info"))
                 .OrderByDescending(m => m.Id)
                 .Take(50)
                 .ToListAsync();
    }
    
    /// <summary>
    /// Get last 50 logs with log level debug
    /// </summary>
    [Route("/debug")]
    [HttpGet]
    public Task<List<LogMessage>> GetDebug([FromServices] APIContext db)
    {
        return db.Messages
                 .AsNoTracking()
                 .Where(m => m.Topic.Contains("debug"))
                 .OrderByDescending(m => m.Id)
                 .Take(50)
                 .ToListAsync();
    }
    
    /// <summary>
    /// Get last 50 logs with log level system
    /// </summary>
    [Route("/system")]
    [HttpGet]
    public Task<List<LogMessage>> GetSystem([FromServices] APIContext db)
    {
        return db.Messages
                 .AsNoTracking()
                 .Where(m => m.Topic.Contains("system"))
                 .OrderByDescending(m => m.Id)
                 .Take(50)
                 .ToListAsync();
    }
    
    /// <summary>
    /// Get last 50 logs with log level critical
    /// </summary>
    [Route("/critical")]
    [HttpGet]
    public Task<List<LogMessage>> GetCritical([FromServices] APIContext db)
    {
        return db.Messages
                 .AsNoTracking()
                 .Where(m => m.Topic.Contains("critical"))
                 .OrderByDescending(m => m.Id)
                 .Take(50)
                 .ToListAsync();
    }
}