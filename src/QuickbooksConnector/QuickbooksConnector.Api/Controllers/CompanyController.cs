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
    public async Task<IActionResult> GetCompanyMainInformationAsync()
    {
        var qbxmlRequest = @"<?xml version=""1.0""?>
            <?qbxml version=""8.0""?>
            <QBXML>
               <QBXMLMsgsRq onError=""stopOnError"">
                  <CompanyQueryRq requestID=""1"" />
               </QBXMLMsgsRq>
            </QBXML>";

        var companyInformation = await _companyService.GetCompanyMainInformationAsync();

        return Ok(companyInformation);
    }
}
