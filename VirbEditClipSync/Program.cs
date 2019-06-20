using System;
using System.IO;
using System.Threading.Tasks;
using Domain;

namespace VirbEditClipSync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var virbDatabase = Path.Combine(appData, "GARMIN\\VIRB Edit\\Database\\7");
            var rawMovies = Path.Combine(virbDatabase, "RawMovies");
            var videoXml = new VideoXml();
            string scanResultToUse = null;

            foreach (var clipDirectory in Directory.EnumerateDirectories(rawMovies))
            {
                var videoXmlPath = Path.Combine(clipDirectory, "video.xml");
                var videoXmlFile = await File.ReadAllTextAsync(videoXmlPath);
                var scanResult = videoXml.ScanForGMetrix(videoXmlFile);

                if (scanResult != null)
                    scanResultToUse = scanResult;
            }

            if (scanResultToUse == null)
                throw new ScanException("Did not find any video clip with G-Metrix connected.");

            Console.WriteLine($"Will use the following G-Metrix data on all clips:\n{scanResultToUse}\n");

            foreach (var clipDirectory in Directory.EnumerateDirectories(rawMovies))
            {
                var videoXmlPath = Path.Combine(clipDirectory, "video.xml");
                var videoXmlFile = await File.ReadAllTextAsync(videoXmlPath);
                var updatedFile = videoXml.SetGMetrix(videoXmlFile, scanResultToUse);
                await File.WriteAllTextAsync(videoXmlPath, updatedFile);
            }

            Console.WriteLine("Updated all clips.");
        }
    }
}
