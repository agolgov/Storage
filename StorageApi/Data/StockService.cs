using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace StorageApi.Data;

public interface IStockService
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock> GetOneAsync(int id);
    Task<bool> CreateAsync(Stock newStock);
    Task<bool> UpdateAsync(Stock editedStock);
    Task<bool> DeleteAsync(int id);
}

public class StockService : IStockService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public StockService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        var stocks = await context.Stocks.Include(s => s.Products).ToListAsync();

        return stocks;
    }

    public async Task<Stock> GetOneAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        
        Stock stock = await context.Stocks.Where(s => s.Id == id).Include(s => s.Products).FirstOrDefaultAsync();

        return stock;
    }

    public async Task<bool> CreateAsync(Stock newStock)
    {
        using var context = _dbFactory.CreateDbContext();
        var stocks = context.Stocks;
        await stocks.AddAsync(newStock);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Stock editedStock)
    {
        using var context = _dbFactory.CreateDbContext();
        var stock = context.Stocks.Where(s => s.Id == editedStock.Id).FirstOrDefault();
        if(stock != null)
        {
            stock.Name = editedStock.Name;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        var stocks = context.Stocks;
        var stock = await stocks.Where(s => s.Id == id).FirstOrDefaultAsync();
        if(stock != null)
        {
            stocks.Remove(stock);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
