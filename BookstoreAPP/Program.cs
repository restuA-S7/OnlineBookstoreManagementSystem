using BookstoreAPP.DAL;
using BookstoreAPP.Models;

//using BookstoreAPP.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var pathDatabase = builder.Configuration.GetConnectionString("HangfireDB");
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookstoreDbContext>(options =>
{
    options.UseSqlServer(pathDatabase);
});

builder.Services.AddScoped<IBook, BooksEF>();
builder.Services.AddScoped<ICustomer, CustomersEF>();
builder.Services.AddScoped<IOrderDetail, OrderDetailsEF>();
builder.Services.AddScoped<IOrder, OrdersEF>();
builder.Services.AddScoped<IReview, ReviewsEF>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
