using Microsoft.AspNetCore.Mvc;
using StorageApi.Data;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace StorageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IProductService _productService;
    private readonly ILogger<ProductController>? _logger;

    public ProductController(IProductService productService,
        ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var products = _productService.GetAllAsync();
        return Ok(JsonSerializer.Serialize(products));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _productService.GetOneAsync(id);
        return Ok(JsonSerializer.Serialize(product));
    }

    [HttpPost("create")]
    public IActionResult Create(Product model)
    {
        _productService.CreateAsync(model);

        return Ok(new { message = "Товар добавлен" });
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Product model)
    {
        _productService.UpdateAsync(model);
        return Ok(new { message = "Информация обновлена" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _productService.DeleteAsync(id);
        return Ok(new { message = "Товар удален" });
    }
}
