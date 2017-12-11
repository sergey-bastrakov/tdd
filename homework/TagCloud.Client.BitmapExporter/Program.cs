using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Core.Drawing;
using TagCloud.Core.Layouters;
using TagCloud.Core.Layouters.Contract;
using TagCloud.Core.Math;
using Rectangle = TagCloud.Core.Math.Rectangle;

namespace TagCloud.Client.BitmapExporter
{
    class Program
    {
        private const string OutDirectoryName = @"generated";
        private const string ReadmeName = @"README.md";

        private static string OutDirectoryPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, OutDirectoryName);
        private static string ReadmePath => Path.Combine(OutDirectoryPath, ReadmeName);

        static void Main(string[] args)
        {
            int seed = 10;
            Case[] cases =
            {
                new Case {Seed = seed},
                new Case {Seed = seed, Accuracy = 5},
                new Case {Seed = seed, Accuracy = 50},
                new Case {Seed = 20}
            };

            CleanUpOutput(OutDirectoryPath);

            List<CaseResult> results = cases.Zip(cases.Select(DrawCase), CaseResult.Create).ToList();
            
            for (int i = 0; i < results.Count; i++)
            {
                string imagePath = SaveImage(OutDirectoryPath, $"{i:D3}", results[i].Image);
                AddToMarkdown(ReadmePath, Path.GetFileName(imagePath), results[i].Description);
            }
        }

        private static void CleanUpOutput(string directory)
        {
            if (Directory.Exists(directory))
            {
                foreach (string file in Directory.EnumerateFiles(directory))
                {
                    File.Delete(file);
                }
            }
            else
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static void AddToMarkdown(string mdFile, string imageName, string description)
        {
            File.AppendAllText(mdFile, $"# {description}\r\n![Generated image {description}]({imageName})\r\n");
        }

        private static string SaveImage(string outDirectory, string filename, Bitmap bitmap)
        {
            string imageName = Path.Combine(outDirectory, filename + ".png");
            bitmap.Save(imageName, ImageFormat.Png);
            return imageName;
        }

        private static Bitmap DrawCase(Case c)
        {
            Random random = new Random(c.Seed);
            ColorGenerator colorGenerator = new ColorGenerator(random);
            IRectangleLayouter layouter = new PositionedLayouter(new CircularCloudLayouter(c.PlacementRadius, c.PlacementSegments, c.Accuracy), new Vector(860, 540));

            List<Rectangle> rectangles = Enumerable.Range(0, c.Count).Select(_ => layouter.Place(GetRandomSize(random, c))).ToList();

            Bitmap result = rectangles.Colorize(colorGenerator.GetRandomColor).ToBitmap(new Size(1920, 1080));

            Graphics graphics = Graphics.FromImage(result);
            graphics.DrawString(c.GetDescription(), new Font("Tahoma", 16), Brushes.White, 10, 10);
            return result;
        }

        private static Vector GetRandomSize(Random random, Case c)
        {
            return new Vector(random.Next(c.MinWidth, c.MaxWidth), random.Next(c.MinHeight, c.MaxHeght));
        }
    }
}
