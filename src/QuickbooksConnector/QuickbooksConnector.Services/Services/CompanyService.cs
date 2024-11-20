using QuickbooksConnector.Services.Models;

namespace QuickbooksConnector.Services.Services;

public interface ICompanyService
{
    Task<CompanyMainInfoRsModel> GetCompanyMainInfoAsync();
}

public class CompanyService : ICompanyService
{
    private readonly IQuickBooksClientService _quickBooksClientService;
    private readonly IXmlParsingService _xmlParsingService;

    public CompanyService(
        IQuickBooksClientService quickBooksClientService, 
        IXmlParsingService xmlParsingService)
    {
        _quickBooksClientService = quickBooksClientService;
        _xmlParsingService = xmlParsingService;
    }

    public async Task<CompanyMainInfoRsModel> GetCompanyMainInfoAsync()
    {
        var qbxmlRequest = @"<?xml version=""1.0""?>
            <?qbxml version=""8.0""?>
            <QBXML>
               <QBXMLMsgsRq onError=""stopOnError"">
                  <CompanyQueryRq requestID=""1"" />
               </QBXMLMsgsRq>
            </QBXML>";

        var responseString = await _quickBooksClientService.SendRequestToQuickBooksAsync(qbxmlRequest);

        var responseModel = _xmlParsingService.ParseCompanyMainInfo(responseString);

        return responseModel;
    }
}
