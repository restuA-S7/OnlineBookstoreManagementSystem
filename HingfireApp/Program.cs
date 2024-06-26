using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HangfireApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                // Configure DbContext
                var hangfireDb = configuration.GetConnectionString("BookstoreDb");

                // Register DbContext
                services.AddDbContext<BookstoreDbContext>(options =>
                    options.UseSqlServer(hangfireDb));

                // Add Hangfire services
                services.AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(hangfireDb, new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    })
                );
                services.AddHangfireServer();

                // Register StockSummaryService
                services.AddScoped<StockSummaryService>(); // Use Scoped instead of Transient

                services.AddControllersWithViews();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure(app =>
                {
                    var env = app.ApplicationServices.GetService<IWebHostEnvironment>();
                    var serviceProvider = app.ApplicationServices;

                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                    }
                    else
                    {
                        app.UseExceptionHandler("/Home/Error");
                        app.UseHsts();
                    }

                    app.UseHttpsRedirection();
                    app.UseStaticFiles();

                    app.UseRouting();

                    app.UseAuthorization();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapDefaultControllerRoute();
                    });

                    app.UseHangfireDashboard();

                    // Use custom job activator
                    var customActivator = new CustomJobActivator(serviceProvider);
                    GlobalConfiguration.Configuration.UseActivator(customActivator);

                    // Schedule the daily stock summary job
                    var recurringJobs = app.ApplicationServices.GetService<IRecurringJobManager>();
                    recurringJobs.AddOrUpdate(
                        "daily-stock-summary",
                        () => customActivator.BeginScope(null).Resolve(typeof(StockSummaryService)).As<StockSummaryService>().SendDailyStockSummary(),
                        Cron.Daily);
                });
            });
}
