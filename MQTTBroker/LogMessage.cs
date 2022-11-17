using MQTTnet.Protocol;

namespace MQTTBroker;

public class LogMessage
{
    public int Id { get; set; }
    public string Client { get; set; }
    public string Message { get; set; }
    public string Topic { get; set; }
    public DateTime Timestamp { get; set; }
    public bool Retain { get; set; }
    public MqttQualityOfServiceLevel? QoS { get; set; }
}