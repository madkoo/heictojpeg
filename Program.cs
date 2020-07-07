using System;
using System.IO;

using ImageMagick;

namespace HeicToJpeg
{


    // How to publishwith single exe and trim down the size : dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Start Converting HEIC to Jpeg");

            Console.WriteLine("Type folder path where HEIC files are:");

            var filePath = Console.ReadLine();

            var files = Directory.EnumerateFiles(filePath);

            foreach (var file in files)
            {

                var extension = Path.GetExtension(file);
                if (extension.Equals(".heic", StringComparison.InvariantCultureIgnoreCase))
                {
                    using (MagickImage image = new MagickImage(file))
                    {
                        // Sets the output format to jpeg
                        image.Format = MagickFormat.Jpeg;
                        // Create byte array that contains a jpeg file
                        var filename = Path.GetFileNameWithoutExtension(file);
                        var dir = Path.GetDirectoryName(file) ??
                                  throw new ArgumentNullException("Path.GetDirectoryName(file)");
                        var newPath = Path.Combine(dir, $"{filename}.jpeg");

                        Console.WriteLine($"Saving converted file to: {newPath}");

                        image.Write(newPath);
                    }
                }
            }

            Console.WriteLine("Completed");
        }

    }
}
