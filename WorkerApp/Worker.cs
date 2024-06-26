using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
                    var orderCustomerEmail = dbContext.Orders
                                            .Include(ord => ord.Customer)
                                            .Include(ord => ord.OrderDetails).ThenInclude(ordet => ordet.Book)
                                            .ToList();

                    string fromMail = "smtpcoba2@gmail.com";
                    string fromPassword = "xqlncjhfonnfcsyj";

                    //judul buku, quantity, price
                    var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true
                    });

                    StringBuilder template = new StringBuilder();
                    Email.DefaultSender = sender;
                    Email.DefaultRenderer = new RazorRenderer();


                    foreach (var orderCust in orderCustomerEmail)
                    {

                        var email = await Email
                                    .From(fromMail)
                                    .To(orderCust.Customer.Email)
                                    .Subject("Order Confirmation")
                                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/TemplateEmail.cshtml", orderCust)
                                    .SendAsync();
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
