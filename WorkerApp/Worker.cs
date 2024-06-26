using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text;
using WorkerApp.Models;

namespace WorkerApp
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                using (var scope = _serviceProvider.CreateScope())
                { 
                    var dbContext = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
                    var orderCustomerEmail = dbContext.Orders.Include(ord => ord.Customer).ToList();

                    var sender = new SmtpSender(() => new SmtpClient("localhost")
                    {
                        EnableSsl = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Port = 25
                    });
                    StringBuilder template = new StringBuilder();
                    template.AppendLine("Dear @Model.email");
                    template.AppendLine("<p>@Model.name</p>");
                    template.AppendLine("Team OnlineBook");

                    Email.DefaultSender = sender;
                    Email.DefaultRenderer = new RazorRenderer();

                    foreach (var orderCust in orderCustomerEmail)
                    {
                        var email = await Email
                                    .From("Tim@tim.com")
                                    .To(orderCust.Customer.Email, orderCust.Customer.Name)
                                    .Subject("Thanks!")
                                    .UsingTemplate(template.ToString(),
                                                    new
                                                    {
                                                        email = orderCust.Customer.Email,
                                                        name = orderCust.Customer.Name
                                                    })
                                    .SendAsync();
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
