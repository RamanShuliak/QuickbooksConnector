namespace QuickbooksConnector.Services.Models;

public class InvoiceMainInfoRsModel
{
    public List<InvoiceRet> InvoiceRets { get; set; } = new List<InvoiceRet>();
}

public class InvoiceRet
{
    public string TxnId { get; set; } = default!;
    public int TxnNumber { get; set; }
    public DateTime TimeCreated { get; set; }
    public DateTime TxnDate { get; set; }
}
