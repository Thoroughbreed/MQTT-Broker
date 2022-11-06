using System.Security.Policy;
using System.Text;
using MQTTAPI.Helpers;
using MQTTnet;
using MQTTnet.Client;

namespace MQTTAPI.Model.Service;

public class MQTTService : IMQTTService
{
    private static IMqttClient _client;
    
    public MQTTService()
    {
        var config = ConfigHelper.ReadConfig();
        var options = new MqttClientOptionsBuilder()
            .WithClientId(config.Users.First().ClientID)
            .WithTcpServer("62.66.208.26")
            .WithCredentials(config.Users.First().Username, config.Users.First().Password)
            .WithCleanSession()
            .Build();
        
        _client = new MqttFactory().CreateMqttClient();
        _client.ConnectAsync(options);
    }

    public async Task<int> Publish()
    {
        if (!_client.IsConnected) return 404;
        await _client.PublishAsync(new MqttApplicationMessage
        {
            Topic = "test",
            Payload = Encoding.UTF8.GetBytes("Hello, world!")
        });
        return 200;
    }
    
}