using QuickbooksConnector.Services.Models;

namespace QuickbooksConnector.Services.Services;

public interface ICheckService
{
    Task<CheckMainInfoRsModel> GetCheckMainInfoAsync();
}

public class CheckService : ICheckService
{
    private readonly IQuickBooksClientService _quickBooksClientService;
    private readonly IXmlParsingService _xmlParsingService;

    public CheckService(
        IQuickBooksClientService quickBooksClientService, 
        IXmlParsingService xmlParsingService)
    {
        _quickBooksClientService = quickBooksClientService;
        _xmlParsingService = xmlParsingService;
    }

    public async Task<CheckMainInfoRsModel> GetCheckMainInfoAsync()
    {
        var qbxmlRequest = @"<?xml version=""1.0"" ?>
            <?qbxml version=""8.0""?>
            <QBXML>
               <QBXMLMsgsRq onError=""stopOnError"">
                  <CheckQueryRq requestID=""1"" />
               </QBXMLMsgsRq>
            </QBXML>";

        var responseString = await _quickBooksClientService.SendRequestToQuickBooksAsync(qbxmlRequest);

        var responseModel = _xmlParsingService.ParseCheckMainInfo(responseString);

        return responseModel;
    }
}
