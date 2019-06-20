using System.Collections.Generic;
using System.Xml;

namespace Domain
{
    public class VideoXml
    {
        public string ScanForGMetrix(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement;
            var innerXml = root.SelectSingleNode("/RawMovie_t/TelemetryTypeAssociations").InnerXml;
            return string.IsNullOrWhiteSpace(innerXml) ? null : innerXml;
        }

        public string SetGMetrix(string xml, string innerXml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement;
            var node = root.SelectSingleNode("/RawMovie_t/TelemetryTypeAssociations");
            node.InnerXml = innerXml;
            return doc.OuterXml;
        }
    }
}