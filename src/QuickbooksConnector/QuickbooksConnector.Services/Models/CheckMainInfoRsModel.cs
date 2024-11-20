namespace QuickbooksConnector.Services.Models;

public class CheckMainInfoRsModel
{
    public List<CheckRet> CheckRets { get; set; } = new List<CheckRet>();
}

public class CheckRet
{
    public string TxnID { get; set; } = default!;
    public DateTime TimeCreated { get; set; }
    public DateTime TxnDate { get; set; }
    public int TxnNumber { get; set; }
    public bool IsToBePrinted { get; set; }
}
