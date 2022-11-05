using Microsoft.EntityFrameworkCore;
using MQTTBroker.Helpers;

namespace MQTTBroker.Data;

public class BrokerContext : DbContext
{
    public DbSet<LogMessage> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Config config = ConfigHelper.ReadConfig();
        optionsBuilder.UseMySQL(config.ConnectionString);
    }
}