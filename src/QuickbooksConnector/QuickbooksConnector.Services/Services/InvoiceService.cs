using QuickbooksConnector.Services.Models;

namespace QuickbooksConnector.Services.Services;

public interface IInvoiceService
{
    Task<InvoiceMainInfoRsModel> GetInvoiceMainInfoAsync();
}

public class InvoiceService : IInvoiceService
{
    private readonly IQuickBooksClientService _quickBooksClientService;
    private readonly IXmlParsingService _xmlParsingService;

    public InvoiceService(
        IQuickBooksClientService quickBooksClientService, 
        IXmlParsingService xmlParsingService)
    {
        _quickBooksClientService = quickBooksClientService;
        _xmlParsingService = xmlParsingService;
    }

    public async Task<InvoiceMainInfoRsModel> GetInvoiceMainInfoAsync()
    {
        var qbxmlRequest = @"<?xml version=""1.0"" ?>
            <?qbxml version=""8.0""?>
            <QBXML>
               <QBXMLMsgsRq onError=""stopOnError"">
                  <InvoiceQueryRq requestID=""4"">
                  </InvoiceQueryRq>
               </QBXMLMsgsRq>
            </QBXML>";

        var responseString = await _quickBooksClientService.SendRequestToQuickBooksAsync(qbxmlRequest);

        var responseModel = _xmlParsingService.ParseInvoiceMainInfo(responseString);

        return responseModel;
    }
}
