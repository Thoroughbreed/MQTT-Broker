using Microsoft.EntityFrameworkCore;
using MQTTAPI.Helpers;
using MQTTAPI.Model;

namespace MQTTAPI.Data;

public class APIContext : DbContext
{
    public DbSet<LogMessage> Messages { get; set; }
    public Config config { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        config = ConfigHelper.ReadConfig();
        optionsBuilder.UseMySQL(config.ConnectionString);
    }
}