using CommonLibrary.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ReportApi.DbLayer
{
    public class DatabaseContext : CommonLibrary.BaseClasses.BaseDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options, configuration)
        {

        }

        public DbSet<Tables.Report> Reports { get; set; }
        public DbSet<Tables.ReportItem> ReportItems { get; set; }
    }
}