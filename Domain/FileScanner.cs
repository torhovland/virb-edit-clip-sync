using System.Xml;

namespace Domain
{
    public class FileScanner
    {
        public string ScanXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement;
            var innerXml = root.SelectSingleNode("/RawMovie_t/TelemetryTypeAssociations").InnerXml;
            return string.IsNullOrWhiteSpace(innerXml) ? null : innerXml;
        }
    }
}