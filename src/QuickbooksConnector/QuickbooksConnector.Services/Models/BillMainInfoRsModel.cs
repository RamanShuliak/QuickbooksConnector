namespace QuickbooksConnector.Services.Models;

public class BillMainInfoRsModel
{
    public List<BillRet> BillRets { get; set; } = new List<BillRet>();
}

public class BillRet
{
    public string TxnID { get; set; } = default!;
    public DateTime TimeCreated { get; set; }
    public DateTime TxnDate { get; set; }
    public int TxnNumber { get; set; }
    public bool IsPaid { get; set; }

}
