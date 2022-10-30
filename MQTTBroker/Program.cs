using MQTTnet.Server;
using MQTTnet;
using System.Text;
using static System.Console;

// See https://aka.ms/new-console-template for more information
namespace MQTTBroker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Create the options for MQTT Broker
            var option = new MqttServerOptionsBuilder()
                //Set endpoint to localhost
                .WithDefaultEndpoint()
                //Add Interceptor for logging incoming messages
                .WithApplicationMessageInterceptor(OnNewMessage);

            // Create a new mqtt server 
            var mqttServer = new MqttFactory().CreateMqttServer();
            await mqttServer.StartAsync(option.Build());
            
            // Keep application running until user press a key
            WriteLine("Press ENTER to quit.");
            ReadLine();
        }

        static void OnNewMessage(MqttApplicationMessageInterceptorContext context)
        {
            // Convert Payload to string
            var payload = context.ApplicationMessage?.Payload == null
                ? null
                : Encoding.UTF8.GetString(context.ApplicationMessage?.Payload);


            WriteLine(
                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",
                DateTime.Now,
                context.ClientId,
                context.ApplicationMessage?.Topic,
                payload,
                context.ApplicationMessage?.QualityOfServiceLevel,
                context.ApplicationMessage?.Retain);
        }
    }
}