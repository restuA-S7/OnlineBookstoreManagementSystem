using Microsoft.EntityFrameworkCore;
using WorkerApp;
using WorkerApp.Models;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<BookstoreDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("HangfireDB"));
});


builder.Services.AddHostedService<Worker>();
/*builder.Services.AddScoped<I>*/
//builder.Services.AddScoped<IHostedService,BookstoreDbContext>();

var host = builder.Build();
host.Run();
