using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HangfireApp.Models;

public class AdminController : Controller
{
    private readonly BookstoreDbContext _context;
    private readonly StockSummaryService _stockSummaryService;
    private static DateTime _lastUpdated; // Static field to store the last update time

    public AdminController(BookstoreDbContext context, StockSummaryService stockSummaryService)
    {
        _context = context;
        _stockSummaryService = stockSummaryService;
    }

    public async Task<IActionResult> DailyStockSummary()
    {
        var books = await _context.Books.ToListAsync();

        if (_lastUpdated == default)
        {
            _lastUpdated = await _stockSummaryService.SendDailyStockSummary();
        }

        ViewBag.LastUpdated = _lastUpdated;
        return View(books);
    }
}