using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Utility;

namespace ReportApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var _host = CreateHostBuilder(args).Build();
            using (var db = _host.Services.GetService<DbLayer.DatabaseContext>())
            {
                //database yoksa olusturur. migration islemlerini de yapar.
                db.Database.Migrate();

                //database yoksa olusturur. migration ile ilgilenmez.
                //db.Database.EnsureCreated();
            }
            _host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:4002");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
