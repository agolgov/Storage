using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace StorageApi.Data;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetOneAsync(int id);
    Task<bool> CreateAsync(Product newProduct);
    Task<bool> UpdateAsync(Product editedProduct);
    Task<bool> DeleteAsync(int id);
    Task<bool> IncrementAsync(int id);
    Task<bool> DecrementAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public ProductService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        using var context = _dbFactory.CreateDbContext();
        //var products = await context.Products.Include(b => b.Stock).ToListAsync();
        var products = await context.Products.ToListAsync();

        return products;
    }

    public async Task<Product> GetOneAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();

        //Product product = await context.Products.Where(p => p.Id == id).Include(p => p.Stock).FirstOrDefaultAsync();
        Product product = await context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        return product;
    }

    public async Task<bool> CreateAsync(Product newProduct)
    {
        using var context = _dbFactory.CreateDbContext();
        var products = context.Products;
        await products.AddAsync(newProduct);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Product editedProduct)
    {
        using var context = _dbFactory.CreateDbContext();
        var product = context.Products.Where(p => p.Id == editedProduct.Id).FirstOrDefault();

        product.Name = editedProduct.Name;
        product.Total = editedProduct.Total;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        var products = context.Products;
        var product = await products.Where(p => p.Id == id).FirstOrDefaultAsync();
        if(product != null)
        {
            products.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> IncrementAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        var product = context.Products.Where(p => p.Id == id).FirstOrDefault();

        product.Total = product.Total + 1;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DecrementAsync(int id)
    {
        using var context = _dbFactory.CreateDbContext();
        var product = context.Products.Where(p => p.Id == id).FirstOrDefault();

        if (product.Total > 0)
        {
            product.Total = product.Total - 1;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
