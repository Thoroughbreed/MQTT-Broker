namespace MQTTAPI.Model;

public class Config
{
    public List<User> Users { get; set; }

    public string Audience { get; set; }
    public string Authority { get; set; }
    
    public int Port { get; set; }
    public int SecPort { get; set; }
    public string ConnectionString { get; set; }
}
