using MQTTnet.Server;
using MQTTnet;
using System.Text;
using MQTTBroker.Helpers;
using MQTTnet.Protocol;
using static System.Console;

namespace MQTTBroker
{
    internal class Program
    {
        private static readonly List<string> ClientIdPrefixesUsed = new();
        private static Config config = new();

        static async Task Main(string[] args)
        {
            config = ConfigHelper.ReadConfig();
            var option = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint();

            // Create a new mqtt server 
            var server = new MqttFactory().CreateMqttServer(option.Build());
            server.InterceptingPublishAsync += Server_InterceptingPublishAsync;
            server.ValidatingConnectionAsync += ValidateConnectionAsync;
            await server.StartAsync();

            // Keep application running until user press a key
            WriteLine("Press ENTER to quit.");
            ReadLine():;
        }

        private static Task ValidateConnectionAsync(ValidatingConnectionEventArgs args)
        {
            try
            {
                var currentUser = config.Users.FirstOrDefault(u => u.UserName == args.UserName);

                if (currentUser == null)
                {
                    args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    WriteLine("currentUser == Null");
                    // LogMessage(args, true);
                    return Task.CompletedTask;
                }

                if (args.UserName != currentUser.UserName)
                {
                    args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    WriteLine("UserName != UserName");
                    // LogMessage(args, true);
                    return Task.CompletedTask;
                }

                if (args.Password != currentUser.Password)
                {
                    args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                    WriteLine("Password != Password");
                    // LogMessage(args, true);
                    return Task.CompletedTask;
                }

                if (!currentUser.ValidateClientId)
                {
                    args.ReasonCode = MqttConnectReasonCode.Success;
                    args.SessionItems.Add(args.ClientId, currentUser);
                    WriteLine("!ValidateClientID");
                    WriteLine(currentUser.ClientId);
                    WriteLine(currentUser.ClientIdPrefix);
                    // LogMessage(args, false);
                    return Task.CompletedTask;
                }

                if (string.IsNullOrWhiteSpace(currentUser.ClientIdPrefix))
                {
                    if (args.ClientId != currentUser.ClientId)
                    {
                        args.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                        WriteLine("Bad username or pwd");
                        // LogMessage(args, true);
                        return Task.CompletedTask;
                    }

                    args.SessionItems.Add(currentUser.ClientId, currentUser);
                }
                else
                {
                    if (!ClientIdPrefixesUsed.Contains(currentUser.ClientIdPrefix))
                    {
                        ClientIdPrefixesUsed.Add(currentUser.ClientIdPrefix);
                    }

                    args.SessionItems.Add(currentUser.ClientIdPrefix, currentUser);
                }

                args.ReasonCode = MqttConnectReasonCode.Success;
                WriteLine("yay!");
                // LogMessage(args, false);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Logger.Error("An error occurred: {Exception}.", ex);
                return Task.FromException(ex);
            }
        }

        static Task Server_InterceptingPublishAsync(InterceptingPublishEventArgs args)
        {
            WriteLine(args.ClientId);
            var clientIdPrefix = GetClientIdPrefix(args.ClientId);
            User? currentUser = null;

            if (string.IsNullOrWhiteSpace(clientIdPrefix))
            {
                if (args.SessionItems.Contains(args.ClientId))
                {
                    currentUser = args.SessionItems[args.ClientId] as User;
                    WriteLine(currentUser.ClientId);
                    WriteLine(currentUser.ClientIdPrefix);
                }
            }
            else
            {
                if (args.SessionItems.Contains(clientIdPrefix))
                {
                    currentUser = args.SessionItems[clientIdPrefix] as User;
                    WriteLine(currentUser.ClientId);
                    WriteLine(currentUser.ClientIdPrefix);
                }
            }
            
            // Convert Payload to string
            var payload = args.ApplicationMessage?.Payload == null
                ? null
                : Encoding.UTF8.GetString(args.ApplicationMessage?.Payload);


            WriteLine(
                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",
                DateTime.Now,
                args.ClientId,
                args.ApplicationMessage?.Topic,
                payload,
                args.ApplicationMessage?.QualityOfServiceLevel,
                args.ApplicationMessage?.Retain);
            return Task.CompletedTask;
        }

        private static string GetClientIdPrefix(string clientId)
        {
            foreach (var clientIdPrefix in ClientIdPrefixesUsed)
            {
                if (clientId.StartsWith(clientIdPrefix))
                {
                    return clientIdPrefix;
                }
            }

            return string.Empty;
        }
    }
}