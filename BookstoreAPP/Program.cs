using BookstoreAPP.DAL;
using BookstoreAPP.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var pathDatabase = builder.Configuration.GetConnectionString("HangfireDB");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure BookstoreDbContext
builder.Services.AddDbContext<BookstoreDbContext>(options =>
{
    options.UseSqlServer(pathDatabase);
});

// Configure AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(pathDatabase);
});

builder.Services.AddScoped<IBook, BooksEF>();
builder.Services.AddScoped<ICustomer, CustomersEF>();
builder.Services.AddScoped<IOrderDetail, OrderDetailsEF>();
builder.Services.AddScoped<IOrder, OrdersEF>();
builder.Services.AddScoped<IReview, ReviewsEF>();

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Sesuaikan sesuai kebutuhan
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
    });

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

// Add middleware for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
