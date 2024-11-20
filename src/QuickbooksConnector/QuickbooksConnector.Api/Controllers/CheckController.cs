using Microsoft.AspNetCore.Mvc;
using QuickbooksConnector.Services.Services;

namespace QuickbooksConnector.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckController : ControllerBase
{
    private readonly ICheckService _checkService;

    public CheckController(
        ICheckService checkService)
    {
        _checkService = checkService;
    }

    [HttpGet("get-check-information")]
    public async Task<IActionResult> GetCheckMainInfoAsync()
    {
        var checkInfo = await _checkService.GetCheckMainInfoAsync();

        return Ok(checkInfo);
    }
}
