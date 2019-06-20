using System.Xml;
using Domain;
using Xunit;

namespace Tests
{
    public class VideoXmlTests
    {
        readonly VideoXml _videoXml = new VideoXml();

        [Fact]
        public void ScanForGMetrix_NoTelemetryTypeAssociations_Null()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <TelemetryTypeAssociations />
                </RawMovie_t>";

            var result = _videoXml.ScanForGMetrix(xml);
            Assert.Null(result);
        }

        [Fact]
        public void ScanForGMetrix_FoundTelemetryTypeAssociations_Returned()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <TelemetryTypeAssociations>
                    <Foo />
                  </TelemetryTypeAssociations>
                </RawMovie_t>";

            var result = _videoXml.ScanForGMetrix(xml);
            Assert.Equal("<Foo />", result);
        }

        [Fact]
        public void SetGMetrix()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <TelemetryTypeAssociations />
                </RawMovie_t>";

            var expectedDoc = new XmlDocument();
            expectedDoc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <TelemetryTypeAssociations>
                    <Foo />
                  </TelemetryTypeAssociations>
                </RawMovie_t>");

            var result = _videoXml.SetGMetrix(xml, "<Foo />");
            Assert.Equal(expectedDoc.OuterXml, result);
        }

        [Fact]
        public void UpdateTimings()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <SourceFiles>
                    <MediaSourceFile_t>
                      <DurationTicks>123600000</DurationTicks>
                    </MediaSourceFile_t>
                  </SourceFiles>
                  <CreateDate>2019-06-08T04:03:47Z</CreateDate>
                  <MediaTimeMappings />
                </RawMovie_t>";

            var expectedDoc = new XmlDocument();
            // 12.36 second clip starting at 04:03:47
            expectedDoc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <SourceFiles>
                    <MediaSourceFile_t>
                      <DurationTicks>123600000</DurationTicks>
                    </MediaSourceFile_t>
                  </SourceFiles>
                  <CreateDate>2019-06-08T04:03:47Z</CreateDate>
                  <MediaTimeMappings>
                    <MediaTimeMapping_t>
                      <TelemetryTime>2019-06-08T04:03:47.0000000Z</TelemetryTime>
                      <MediaTimeTicks>0</MediaTimeTicks>
                    </MediaTimeMapping_t>
                    <MediaTimeMapping_t>
                      <TelemetryTime>2019-06-08T04:03:59.3600000Z</TelemetryTime>
                      <MediaTimeTicks>123600000</MediaTimeTicks>
                    </MediaTimeMapping_t>
                  </MediaTimeMappings>
                </RawMovie_t>");

            var result = _videoXml.UpdateTimings(xml);

            var actualDoc = new XmlDocument();
            actualDoc.LoadXml(result);

            Assert.Equal(expectedDoc.OuterXml, actualDoc.OuterXml);
        }
    }
}
