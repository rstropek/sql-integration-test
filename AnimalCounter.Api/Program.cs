using AnimalCounter.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnimalCounter.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Length > 0 && args[0] == "Fill")
            {
                using var scope = host.Services.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<AnimalCounterDbContext>();

                context.RemoveRange(await context.AnimalFrequencies.ToArrayAsync());

                var animals = JsonSerializer.Deserialize<IEnumerable<AnimalFrequency>>(await File.ReadAllTextAsync("data.json"))!;
                context.AnimalFrequencies.AddRange(animals);
                await context.SaveChangesAsync();

                return;
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
