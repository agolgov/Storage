namespace StorageApi.Data;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Total { get; set; }

    public Stock? Stock { get; set; }
    public int? StockId { get; set; }
}
