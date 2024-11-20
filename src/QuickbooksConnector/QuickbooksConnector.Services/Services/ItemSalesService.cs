using QuickbooksConnector.Services.Models;

namespace QuickbooksConnector.Services.Services;

public interface IItemSalesService
{
    Task<ItemSalesMainInfoRsModel> GetItemSalesMainInfoAsync();
}

public class ItemSalesService : IItemSalesService
{
    private readonly IQuickBooksClientService _quickBooksClientService;
    private readonly IXmlParsingService _xmlParsingService;

    public ItemSalesService(
        IQuickBooksClientService quickBooksClientService, 
        IXmlParsingService xmlParsingService)
    {
        _quickBooksClientService = quickBooksClientService;
        _xmlParsingService = xmlParsingService;
    }

    public async Task<ItemSalesMainInfoRsModel> GetItemSalesMainInfoAsync()
    {
        var qbxmlRequest = @"<?xml version=""1.0"" ?>
            <?qbxml version=""8.0""?>
            <QBXML>
               <QBXMLMsgsRq onError=""stopOnError"">
                  <ItemSalesTaxQueryRq requestID=""4"">
                  </ItemSalesTaxQueryRq>
               </QBXMLMsgsRq>
            </QBXML>";

        var responseString = await _quickBooksClientService.SendRequestToQuickBooksAsync(qbxmlRequest);

        var responseModel = _xmlParsingService.ParseItemSalesMainInfo(responseString);

        return responseModel;
    }
}
