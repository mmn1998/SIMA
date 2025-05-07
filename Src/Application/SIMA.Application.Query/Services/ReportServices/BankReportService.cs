using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;
using System.Text;
using System.Xml.Linq;

namespace SIMA.Application.Query.Services.ReportServices;

public class BankReportService : IBankReportService
{
    public BankReportService()
    {
    }
    public GetDownloadDocumentQueryResult GenerateEceFile(GetReferalLetterQueryResult data)
    {
        var response = new GetDownloadDocumentQueryResult();
        XNamespace ns = "http://www.irica.com/ECE/1383-12/SendSchema";

        var guid = Guid.NewGuid().ToString();
        var xml = new XElement(ns + "Letter",
            new XElement(ns + "Protocol",
                new XAttribute("Version", "1.01"),
                new XAttribute("Name", "ECE")
            ),
            new XElement(ns + "Software",
                new XAttribute("GUID", $"{guid}"),
                new XAttribute("SoftwareDeveloper", "گام الكترونیك"),
                new XAttribute("Version", "8.4.10"),
                "GAMELECTRONICS"
            ),
            new XElement(ns + "Sender",
                new XAttribute("Department", ""),
                new XAttribute("Code", $"{guid};;3738"),
                new XAttribute("Position", $"رییس واحد مدیریت پرداختها اداره کل بین الملل (شرفی)"),
                new XAttribute("Organization", "حراست بانک ملت"),
                new XAttribute("Name", "مصطفی شرفی")
            ),
            new XElement(ns + "Receiver",
                new XAttribute("Organization", "سازمان های مخاطب"),
                new XAttribute("Department", ""),
                new XAttribute("Position", "شرکت تجارت سریر افروز کیش(سهامی خاص)"),
                new XAttribute("Code", "817;;"),
                new XAttribute("ReceiveType", "Origin")
            ),
            new XElement(ns + "LetterNo", $"{data.LetterNumber}"),
            new XElement(ns + "LetterDateTime",
                new XAttribute("ShowAs", "jalali"),
                $"{data.LetterDate}"
            ),
            new XElement(ns + "Subject", "مکاتبه, پاسخ"),
            new XElement(ns + "Priority",
                new XAttribute("Code", "22"),
                new XAttribute("Name", "فوری")
            ),
            new XElement(ns + "Classification",
                new XAttribute("Name", "محرمانه"),
                new XAttribute("Code", "2")
            ),
            new XElement(ns + "Keywords",
                new XElement(ns + "Keyword", "تاییدیه")
            ),
            new XElement(ns + "Origins",
                new XElement(ns + "Origin",
                    new XAttribute("ContentType", "application/msword"),
                    new XAttribute("Extension", "doc"),
                    new XAttribute("Description", "LetterBody_124457")
                )
            ),
            new XElement(ns + "Attachments")
        );
        response.FileContent = GetBase64OfXml(xml);
        response.Name = Guid.NewGuid().ToString();
        response.Extension = "xml";
        return response;
    }
    private byte[] GetBase64OfXml(XElement xml)
    {
        string xmlString = xml.ToString();

        // Convert the string to bytes
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlString);
        return xmlBytes;
        //// Convert bytes to Base64 string
        //string base64Xml = Convert.ToBase64String(xmlBytes);
        //return base64Xml;
    }
}
