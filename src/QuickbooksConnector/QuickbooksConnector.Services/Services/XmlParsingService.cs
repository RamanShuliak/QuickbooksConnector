using QuickbooksConnector.Services.Models;
using System.Xml;
using System.Xml.Linq;

namespace QuickbooksConnector.Services.Services;

public interface IXmlParsingService
{
    CompanyMainInfoRsModel ParseCompanyMainInfo(string xml);
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
}
