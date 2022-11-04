using MQTTnet.Protocol;

namespace MQTTBroker.Models;

public class MqttMessage
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string ClientId { get; set; }
    public string Topic { get; set; }
    public string Payload { get; set; }
    public MqttQualityOfServiceLevel QoS { get; set; }
    public bool Retain { get; set; }
}