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
    }
}
