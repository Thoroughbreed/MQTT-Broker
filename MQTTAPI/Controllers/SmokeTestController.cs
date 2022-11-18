using Microsoft.AspNetCore.Mvc;

namespace MQTTAPI.Controllers;

[ApiController]
public class SmokeTestController
{
    /// <summary>
    /// Smoke test
    /// </summary>
    /// <returns>Returns "Hello World." if the service is available</returns>
    [Route("/")]
    [HttpGet]
    public string SmokeTest()
    {
        return "Hello World.";
    }
}