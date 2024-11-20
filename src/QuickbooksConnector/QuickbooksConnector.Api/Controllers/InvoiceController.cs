using Microsoft.AspNetCore.Mvc;
using QuickbooksConnector.Services.Services;

namespace QuickbooksConnector.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(
        IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("get-invoice-information")]
    public async Task<IActionResult> GetInvoiceMainInfoAsync()
    {
        var invoiceInfo = await _invoiceService.GetInvoiceMainInfoAsync();

        return Ok(invoiceInfo);
    }
}
