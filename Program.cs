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
                });

            try
            {
                await builder.RunCommandLineApplicationAsync<CrazyScraperCmd>(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
