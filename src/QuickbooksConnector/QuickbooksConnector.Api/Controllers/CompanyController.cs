using Microsoft.AspNetCore.Mvc;
using QuickbooksConnector.Services.Services;

namespace QuickbooksConnector.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(
        ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet("get-company-information")]
    public async Task<IActionResult> GetCompanyMainInfoAsync()
    {
        var companyInfo = await _companyService.GetCompanyMainInfoAsync();

        return Ok(companyInfo);
    }
}
