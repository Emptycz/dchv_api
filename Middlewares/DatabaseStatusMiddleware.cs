using System.Net;
using dchv_api.Database;
using dchv_api.Database.Seed;

namespace dchv_api.Middlewares;

public static class DatabaseSeedManager
{
    public static void MigrateUp(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try {
                BaseDbContext context = services.GetRequiredService<BaseDbContext>();
                context.Database.EnsureCreated();
            } catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("[DatabaseSeedManager.MigrateUp] Could not reach the database");
                logger.LogDebug(ex.Message);
            }
        }
    }
    public static void MigrateSampleSeed(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                BaseDbContext context = services.GetRequiredService<BaseDbContext>();
                DbSampleSeed.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("[DatabaseSeedManager.MigrateSampleSeed] Error happened while migrating data");
                logger.LogDebug(ex.Message);
            }
        }
    }
}

