using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Storage.Data;

public class Stock
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Product> Products { get; set; }

    public Stock()
    {
        Products = new List<Product>();
    }
}