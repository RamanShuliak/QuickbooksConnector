using Microsoft.AspNetCore.Mvc;
using QuickbooksConnector.Services.Services;

namespace QuickbooksConnector.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BillController : ControllerBase
{
    private readonly IBillService _billService;

    public BillController(
        IBillService billService)
    {
        _billService = billService;
    }

    [HttpGet("get-bill-information")]
    public async Task<IActionResult> GetBillMainInfoAsync()
    {
        var itemSalesInfo = await _billService.GetBillMainInfoAsync();

        return Ok(itemSalesInfo);
    }
}
