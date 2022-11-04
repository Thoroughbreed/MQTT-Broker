using System.Reflection;
using MQTTnet.Server;
using MQTTnet;
using System.Text;
using MQTTnet.Protocol;
using Newtonsoft.Json;
using static System.Console;

// See https://aka.ms/new-console-template for more information
namespace MQTTBroker
{
    internal class Program
    {
        private static readonly List<string> ClientIdPrefixesUsed = new();
        private static Config config = new();
        private static List<LogMessage> _LogMessages = new();

        static async Task Main(string[] args)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            config = ReadConfiguration(currentPath);
            var option = new MqttServerOptionsBuilder().WithDefaultEndpoint();

            var server = new MqttFactory().CreateMqttServer(option.Build());
            server.InterceptingPublishAsync += Server_InterceptingPublishAsync;
            server.ValidatingConnectionAsync += ValidateConnectionAsync;
            await server.StartAsync();


            WriteLine("Press ENTER to quit, press L to show log - press A to show all logs (last 50)");
            ConsoleKeyInfo key;
            key = ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                Clear();
                switch (key.Key)
                {
                    case ConsoleKey.L:
                    {
                        WriteLine();
                        WriteLine(" ---- Last 10 info:");
                        foreach (var logMessage in _LogMessages.Where(l => l.Topic.Contains("info")).Take(10))
                        {
                            WriteLine(
                                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",
                                DateTime.Now,
                                logMessage.Timestamp.ToString(),
                                logMessage.Client,
                                logMessage.Message,
                                logMessage.QoS,
                                logMessage.Retain);
                        }

                        WriteLine();
                        WriteLine(" ---- Last 10 critical:");
                        foreach (var logMessage in _LogMessages.Where(l => l.Topic.Contains("critical")).Take(10))
                        {
                            WriteLine(
                                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",
                                DateTime.Now,
                                logMessage.Timestamp.ToString(),
                                logMessage.Client,
                                logMessage.Message,
                                logMessage.QoS,
                                logMessage.Retain);
                        }

                        WriteLine();
                        WriteLine(" ---- Last 10 debug:");
                        foreach (var logMessage in _LogMessages.Where(l => l.Topic.Contains("debug")).Take(10))
                        {
                            WriteLine(
                                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",
                                DateTime.Now,
                                logMessage.Timestamp.ToString(),
                                logMessage.Client,
                                logMessage.Message,
                                logMessage.QoS,
                                logMessage.Retain);
                        }
                        break;
                    }
                    case ConsoleKey.A:
                    {
                        WriteLine(" ---- Last 50:");
                        foreach (var logMessage in _LogMessages.Take(50))
                        {
                            WriteLine(
                                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",
                                DateTime.Now,
                                logMessage.Timestamp.ToString(),
                                logMessage.Client,
                                logMessage.Message,
                                logMessage.QoS,
                                logMessage.Retain);
                        }
                        break;
                    }
                }
                key = ReadKey(true);
            }
        }

        private static Task ValidateConnectionAsync(ValidatingConnectionEventArgs args)
        {
            try
            {
                var currentUser = config.Users.FirstOrDefault(u => u.UserName == args.UserName);


                if (currentUser == null)
                {
                    args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    return Task.CompletedTask;
                }

                if (args.UserName != currentUser.UserName)
                {
                    args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    return Task.CompletedTask;
                }

                if (args.Password != currentUser.Password)
                {
                    args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    return Task.CompletedTask;
                }

                args.ReasonCode = MqttConnectReasonCode.Success;
                WriteLine("yay!");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        static Task Server_InterceptingPublishAsync(InterceptingPublishEventArgs args)
        {
            User? currentUser = null;

            var payload = args.ApplicationMessage?.Payload == null
                ? null
                : Encoding.UTF8.GetString(args.ApplicationMessage?.Payload);

            LogMessages(args, payload);
            return Task.CompletedTask;
        }

        private static void LogMessages(InterceptingPublishEventArgs args, string payload)
        {
            Clear();
            SetCursorPosition(0, 3);
            WriteLine("Press ENTER to quit, press L to show log - press A to show all logs (last 50)");
            Write("Amount: ");
            Write(_LogMessages.Count);
            _LogMessages.Add(new LogMessage
            {
                Client = args.ClientId,
                Message = payload,
                QoS = args.ApplicationMessage?.QualityOfServiceLevel,
                Retain = (bool)args.ApplicationMessage?.Retain,
                Timestamp = DateTime.Now,
                Topic = args.ApplicationMessage?.Topic
            });
        }

        private static Config ReadConfiguration(string currentPath)
        {
            var filePath = $"{currentPath}/config.json";

            if (File.Exists(filePath))
            {
                Config config;
                using (var r = new StreamReader(filePath))
                {
                    var json = r.ReadToEnd();
                    config = JsonConvert.DeserializeObject<Config>(json) ?? new();
                }


                return config;
            }

            return new Config();
        }
    }
}