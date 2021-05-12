using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Webapi.Models;

namespace Webapi
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();

      await CreateDbIfNotExists(host);

      host.Run();
    }

    public static async Task CreateDbIfNotExists(IWebHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          await DbInitializer.Init(services);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred creating the DB.");
        }
      }
    }
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
