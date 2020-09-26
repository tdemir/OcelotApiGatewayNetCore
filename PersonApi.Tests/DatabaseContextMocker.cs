using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
using PersonApi.DbLayer;

namespace PersonApi.Tests
{
    public class DatabaseContextMocker
    {
        public static DatabaseContext GetApiDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var configPath = System.AppContext.BaseDirectory;//appsettings.json file path
            var configuration = GetIConfigurationRoot(configPath);
            // Create instance of DbContext
            var dbContext = new DatabaseContext(options, configuration);


            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.Test.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}