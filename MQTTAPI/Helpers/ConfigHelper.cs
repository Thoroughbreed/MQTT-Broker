using System.Reflection;
using MQTTAPI.Model;
using Newtonsoft.Json;

namespace MQTTAPI.Helpers;

public class ConfigHelper
{
    public static Config ReadConfig()
    {
        var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var filePath = $"{currentPath}/config.json";

        if (!File.Exists(filePath)) return new Config();

        string fileData = File.ReadAllText(filePath);
        var config = JsonConvert.DeserializeObject<Config>(fileData);

        if (config == null) throw new FileNotFoundException("The file config.json was not found");
        return config;
    }
}