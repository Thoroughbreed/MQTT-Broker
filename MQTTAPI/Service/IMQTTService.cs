namespace MQTTAPI.Model.Service;

public interface IMQTTService
{
    public Task<int> Publish();
}