namespace MQTTAPI.Model;

public class Config
{
    public List<User> Users { get; set; }
    public int Port { get; set; }
    public int SecPort { get; set; }
    public string ConnectionString { get; set; }
}
