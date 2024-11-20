using QuickbooksConnector.Services.Models;
using System.Xml;

namespace QuickbooksConnector.Services.Services;

public interface IXmlParsingService
{
    CompanyMainInfoRsModel ParseCompanyMainInfo(string xml);
    InvoiceMainInfoRsModel ParseInvoiceMainInfo(string xml);
    ItemSalesMainInfoRsModel ParseItemSalesMainInfo(string xml);
    BillMainInfoRsModel ParseBillMainInfo(string xml);
    CheckMainInfoRsModel ParseCheckMainInfo(string xml);
}

public class XmlParsingService : IXmlParsingService
{
    public CompanyMainInfoRsModel ParseCompanyMainInfo(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        var model = new CompanyMainInfoRsModel
        {
            CompanyName = doc.SelectSingleNode("//CompanyName")?.InnerText ?? string.Empty,
            Country = doc.SelectSingleNode("//Country")?.InnerText ?? string.Empty,
            State = doc.SelectSingleNode("//State")?.InnerText ?? string.Empty,
            City = doc.SelectSingleNode("//City")?.InnerText ?? string.Empty,
            Address = doc.SelectSingleNode("//Address")?.InnerText ?? string.Empty,
            PhoneNumber = doc.SelectSingleNode("//Phone")?.InnerText ?? string.Empty
        };

        return model;
    }

    public InvoiceMainInfoRsModel ParseInvoiceMainInfo(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        var model = new InvoiceMainInfoRsModel();

        var invoiceNodes = doc.SelectNodes("//InvoiceRet");

        if (invoiceNodes != null)
        {
            foreach (XmlNode invoiceNode in invoiceNodes)
            {
                var invoiceRet = new InvoiceRet
                {
                    TxnId = invoiceNode.SelectSingleNode("TxnId")?.InnerText ?? string.Empty,
                    TxnNumber = int.TryParse(invoiceNode.SelectSingleNode("TxnNumber")?.InnerText, out var txnNumber) ? txnNumber : 0,
                    TimeCreated = DateTime.TryParse(invoiceNode.SelectSingleNode("TimeCreated")?.InnerText, out var timeCreated) ? timeCreated : DateTime.MinValue,
                    TxnDate = DateTime.TryParse(invoiceNode.SelectSingleNode("TxnDate")?.InnerText, out var txnDate) ? txnDate : DateTime.MinValue
                };

                model.InvoiceRets.Add(invoiceRet);
            }
        }

        return model;
    }

    public ItemSalesMainInfoRsModel ParseItemSalesMainInfo(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        var model = new ItemSalesMainInfoRsModel();

        var itemSalesNodes = doc.SelectNodes("//ItemSalesTaxRet");
        if (itemSalesNodes != null)
        {
            foreach (XmlNode itemNode in itemSalesNodes)
            {
                var item = new ItemSalesTaxRet
                {
                    ListId = itemNode.SelectSingleNode("ListID")?.InnerText ?? string.Empty,
                    TimeCreated = DateTime.TryParse(itemNode.SelectSingleNode("TimeCreated")?.InnerText, out var timeCreated) ? timeCreated : DateTime.MinValue,
                    Name = itemNode.SelectSingleNode("Name")?.InnerText ?? string.Empty,
                    IsActive = bool.TryParse(itemNode.SelectSingleNode("IsActive")?.InnerText, out var isActive) && isActive
                };

                model.ItemSalesRets.Add(item);
            }
        }

        return model;
    }

    public BillMainInfoRsModel ParseBillMainInfo(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        var model = new BillMainInfoRsModel();

        var billNodes = doc.SelectNodes("//BillRet");
        if (billNodes != null)
        {
            foreach (XmlNode billNode in billNodes)
            {
                var bill = new BillRet
                {
                    TxnID = billNode.SelectSingleNode("TxnID")?.InnerText ?? string.Empty,
                    TimeCreated = DateTime.TryParse(billNode.SelectSingleNode("TimeCreated")?.InnerText, out var timeCreated) ? timeCreated : DateTime.MinValue,
                    TxnDate = DateTime.TryParse(billNode.SelectSingleNode("TxnDate")?.InnerText, out var txnDate) ? txnDate : DateTime.MinValue,
                    TxnNumber = int.TryParse(billNode.SelectSingleNode("TxnNumber")?.InnerText, out var txnNumber) ? txnNumber : 0,
                    IsPaid = bool.TryParse(billNode.SelectSingleNode("IsPaid")?.InnerText, out var isPaid) && isPaid
                };

                model.BillRets.Add(bill);
            }
        }

        return model;
    }

    public CheckMainInfoRsModel ParseCheckMainInfo(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        var model = new CheckMainInfoRsModel();

        var checkNodes = doc.SelectNodes("//CheckRet");
        if (checkNodes != null)
        {
            foreach (XmlNode checkNode in checkNodes)
            {
                var check = new CheckRet
                {
                    TxnID = checkNode.SelectSingleNode("TxnID")?.InnerText ?? string.Empty,
                    TimeCreated = DateTime.TryParse(checkNode.SelectSingleNode("TimeCreated")?.InnerText, out var timeCreated) ? timeCreated : DateTime.MinValue,
                    TxnDate = DateTime.TryParse(checkNode.SelectSingleNode("TxnDate")?.InnerText, out var txnDate) ? txnDate : DateTime.MinValue,
                    TxnNumber = int.TryParse(checkNode.SelectSingleNode("TxnNumber")?.InnerText, out var txnNumber) ? txnNumber : 0,
                    IsToBePrinted = bool.TryParse(checkNode.SelectSingleNode("IsToBePrinted")?.InnerText, out var isToBePrinted) && isToBePrinted
                };

                model.CheckRets.Add(check);
            }
        }

        return model;
    }
}
