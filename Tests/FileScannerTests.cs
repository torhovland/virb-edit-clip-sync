using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class FileScannerTests
    {
        [Fact]
        public void ScanFile_NoTelemetryTypeAssociations_null()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <RawMovie_t xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                  <TelemetryTypeAssociations />
                </RawMovie_t>";

            var scanner = new FileScanner();
            var result = scanner.ScanXml(xml);
            Assert.Null(result);
        }
    }

    public class FileScanner
    {
        public IEnumerable<char> ScanXml(string xml)
        {
            return null;
        }
    }
}
