using System.Text.Json.Serialization;

namespace MQTTBroker;

public class User
{
    public string UserName { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    [JsonIgnore]
    public string Password { get; set; } = string.Empty;
    public TopicTuple SubscriptionTopicLists { get; set; } = new();
    public TopicTuple PublishTopicLists { get; set; } = new();
    public string ClientIdPrefix { get; set; } = string.Empty;
    public bool ValidateClientId { get; set; }
    public bool ThrottleUser { get; set; }
    public long? MonthlyByteLimit { get; set; }
}

public class TopicTuple
{
    public List<string> WhitelistTopics { get; set; } = new();
    public List<string> BlacklistTopics { get; set; } = new();
}