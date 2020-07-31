using CrazyScraper.Commands;
using CrazyScraper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

/*
 * TODO:
 * - Features for Scraping Javascript Rendered Websites
 * - Scrape Stock Websites for prices
 * - Scrape Whether Temperature Forecast Websites
 * - Scrape Facebook Website
 * - Scrape Twitter
 */
namespace CrazyScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<InstagramService>();
                    hostContext.Configuration = Configuration;
                });

            try
            {
                await builder.RunCommandLineApplicationAsync<MainCommand>(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
