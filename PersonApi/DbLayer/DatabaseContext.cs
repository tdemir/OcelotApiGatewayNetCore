using CommonLibrary.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PersonApi.DbLayer
{
    public class DatabaseContext : CommonLibrary.BaseClasses.BaseDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options, configuration)
        {
        }
        public DbSet<Tables.Person> People { get; set; }
        public DbSet<Tables.CommunicationInformation> CommunicationInformations { get; set; }
    }
}