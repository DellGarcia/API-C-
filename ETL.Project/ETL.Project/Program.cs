using ETL.Project.Database;
using Microsoft.EntityFrameworkCore;
using ETL.Project.Utils;
using System.Text.Json;
using System;
using ETL.Project.Analysis.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace ETL.Project
{
    public class Program
    {
        private readonly static IHost host = CreateDefaultBuilder().Build();
        private readonly static IServiceScope serviceScope = host.Services.CreateScope();
        public readonly static IServiceProvider Provider = serviceScope.ServiceProvider;

        public static void Main()
        {
            var mainInstance = Provider.GetRequiredService<Main>();
            mainInstance.Start();

            host.Run();
        }


        static IHostBuilder CreateDefaultBuilder()
        {
            var tempHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                    app.AddJsonFile("appsettings.Development.json");
                });

            //var config = Provider.GetService<IConfiguration>();

            tempHost.ConfigureServices(services =>
                {
                    services.AddSingleton<Main>();

                    string clientString = "server=localhost;port=3306;database=clientETL-database;uid=root;password=";

                    services.AddDbContext<ClientDBContext>(options =>
                        options.UseMySql(clientString, ServerVersion.AutoDetect(clientString)));

                    string analystString = "server=localhost;port=3306;database=analystETL-database;uid=root;password=";

                    services.AddDbContext<AnalystDBContext>(options =>
                        options.UseMySql(analystString, ServerVersion.AutoDetect(analystString)));
                });


            return tempHost;
        }

    }
}
