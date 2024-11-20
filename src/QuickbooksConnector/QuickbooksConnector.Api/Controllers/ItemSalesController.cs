using Microsoft.AspNetCore.Mvc;
using QuickbooksConnector.Services.Services;

namespace QuickbooksConnector.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemSalesController : ControllerBase
{
    private readonly IItemSalesService _itemSalesService;

    public ItemSalesController(
        IItemSalesService itemSalesService)
    {
        _itemSalesService = itemSalesService;
    }

    [HttpGet("get-iten-sales-information")]
    public async Task<IActionResult> GetInvoiceMainInfoAsync()
    {
        var itemSalesInfo = await _itemSalesService.GetItemSalesMainInfoAsync();

        return Ok(itemSalesInfo);
    }
}
