using Domain;
using Xunit;

namespace Tests
{
    public class VideoXmlTests
    {
        [Fact]
        public void ScanXml_NoTelemetryTypeAssociations_Null()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <TelemetryTypeAssociations />
                </RawMovie_t>";

            var scanner = new VideoXml();
            var result = scanner.ScanXml(xml);
            Assert.Null(result);
        }

        [Fact]
        public void ScanXml_FoundTelemetryTypeAssociations_Returned()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <TelemetryTypeAssociations>
                    <Foo />
                  </TelemetryTypeAssociations>
                </RawMovie_t>";

            var scanner = new VideoXml();
            var result = scanner.ScanXml(xml);
            Assert.Equal("<Foo />", result);
        }
    }
}
