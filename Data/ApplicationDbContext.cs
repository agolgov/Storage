using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Storage.Data;

public class ApplicationDbContext : DbContext
{

    public static readonly string RowVersion = nameof(RowVersion);
    public static readonly string StoragesDb = nameof(StoragesDb).ToLower();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Debug.WriteLine($"{ContextId} context created.");
    }
    
    public DbSet<Stock>? Stocks { get; set; }
    public DbSet<Product>? Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>()
            .Property<byte[]>(RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Product>()
            .Property<byte[]>(RowVersion)
            .IsRowVersion();

        base.OnModelCreating(modelBuilder);
    }

    public override void Dispose()
    {
        Debug.WriteLine($"{ContextId} context disposed.");
        base.Dispose();
    }

    public override ValueTask DisposeAsync()
    {
        Debug.WriteLine($"{ContextId} context disposed async.");
        return base.DisposeAsync();
    }

    // public override int SaveChanges()
    // {
    //     return base.SaveChanges();
    // }

}
