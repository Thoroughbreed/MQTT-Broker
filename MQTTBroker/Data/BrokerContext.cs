using Microsoft.EntityFrameworkCore;
using MQTTBroker.Helpers;
using MQTTBroker.Models;

namespace MQTTBroker.Data;

public class BrokerContext : DbContext
{
    public DbSet<MqttMessage> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Config config = ConfigHelper.ReadConfig();
        optionsBuilder.UseMySQL(config.ConnectionString);
    }
}