using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Todos.Api.Extensions
{
    public static class DatabaseExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                logger.LogInformation("Migrating postgres database.");

                var connection = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(connection))
                {
                    logger.LogError("Connection string is required");
                    return host;
                }

                logger.LogInformation($"Connection string: {connection}");

                // this will ensure that the database is created
                EnsureDatabase.For.PostgresqlDatabase(connection);

                var upgrader = DeployChanges.To
                    .PostgresqlDatabase(connection)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    logger.LogError(result.Error, "An error occurred while migrating the postgres database");
                    return host;
                }

                logger.LogInformation("Migrated postgres database.");
            }

            return host;
        }
    }
}