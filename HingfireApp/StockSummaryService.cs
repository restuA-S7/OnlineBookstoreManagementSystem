using System;
using System.Linq;
using System.Threading.Tasks;
using HangfireApp.Models;
using Microsoft.EntityFrameworkCore;

public class StockSummaryService
{
    private readonly BookstoreDbContext _context;

    public StockSummaryService(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task<DateTime> SendDailyStockSummary()
    {
        // Query the database for stock levels
        var stockSummary = await _context.Books.ToListAsync();

        foreach (var book in stockSummary)
        {
            Console.WriteLine($"Book: {book.Title}, Stock Level: {book.Stock}");
        }
        return DateTime.UtcNow;
    }
}
