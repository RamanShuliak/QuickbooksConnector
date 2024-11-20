namespace QuickbooksConnector.Services.Models;

public class ItemSalesMainInfoRsModel
{
    public List<ItemSalesTaxRet> ItemSalesRets { get; set; } = new List<ItemSalesTaxRet>();
}
public class ItemSalesTaxRet
{
    public string ListId { get; set; } = default!;
    public DateTime TimeCreated { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
}
