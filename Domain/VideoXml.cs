using System;
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

        public string UpdateTimings(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement;
            var durationNode = root.SelectSingleNode("/RawMovie_t/SourceFiles/MediaSourceFile_t/DurationTicks");
            var duration = long.Parse(durationNode.InnerText);
            var createDateNode = root.SelectSingleNode("/RawMovie_t/CreateDate");
            var createDate = DateTime.Parse(createDateNode.InnerText).ToUniversalTime();
            var endTime = createDate.AddTicks(duration);
            Console.WriteLine($"Clip starting {createDate} with duration {duration} is set to end {endTime}.");
            var mediaTimeNode = root.SelectSingleNode("/RawMovie_t/MediaTimeMappings");
            mediaTimeNode.InnerXml = $@"<MediaTimeMapping_t>
                  <TelemetryTime>{createDate.ToString("O")}</TelemetryTime>
                  <MediaTimeTicks>0</MediaTimeTicks>
                </MediaTimeMapping_t>
                <MediaTimeMapping_t>
                  <TelemetryTime>{endTime.ToString("O")}</TelemetryTime>
                  <MediaTimeTicks>{duration}</MediaTimeTicks>
                </MediaTimeMapping_t>";
            return doc.OuterXml;
        }
    }
}