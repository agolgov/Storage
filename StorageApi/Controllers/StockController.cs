using Microsoft.AspNetCore.Mvc;
using StorageApi.Data;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace StorageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private IStockService _stockService;
    private readonly ILogger<StockController>? _logger;

    public StockController(IStockService stockService,
        ILogger<StockController> logger)
    {
        _stockService = stockService;
        _logger = logger;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var stocks = _stockService.GetAllAsync();
        return Ok(JsonSerializer.Serialize(stocks));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var stock = _stockService.GetOneAsync(id);
        return Ok(JsonSerializer.Serialize(stock));
    }

    [HttpPost("create")]
    public IActionResult Create(Stock model)
    {
        _stockService.CreateAsync(model);

        return Ok(new { message = "Склад добавлен" });
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Stock model)
    {
        _stockService.UpdateAsync(model);
        return Ok(new { message = "Информация обновлена" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _stockService.DeleteAsync(id);
        return Ok(new { message = "Склад удален" });
    }
}
