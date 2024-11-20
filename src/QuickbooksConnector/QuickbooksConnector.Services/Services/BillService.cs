using QuickbooksConnector.Services.Models;

namespace QuickbooksConnector.Services.Services;

public interface IBillService
{
    Task<BillMainInfoRsModel> GetBillMainInfoAsync();
}

public class BillService : IBillService
{
    private readonly IQuickBooksClientService _quickBooksClientService;
    private readonly IXmlParsingService _xmlParsingService;

    public BillService(
        IQuickBooksClientService quickBooksClientService, 
        IXmlParsingService xmlParsingService)
    {
        _quickBooksClientService = quickBooksClientService;
        _xmlParsingService = xmlParsingService;
    }

    public async Task<BillMainInfoRsModel> GetBillMainInfoAsync()
    {
        var qbxmlRequest = @"<?xml version=""1.0"" ?>
            <?qbxml version=""8.0""?>
            <QBXML>
               <QBXMLMsgsRq onError=""stopOnError"">
                  <BillQueryRq requestID=""1"" />
               </QBXMLMsgsRq>
            </QBXML>";

        var responseString = await _quickBooksClientService.SendRequestToQuickBooksAsync(qbxmlRequest);

        var responseModel = _xmlParsingService.ParseBillMainInfo(responseString);

        return responseModel;
    }
}
