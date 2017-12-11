using System.Drawing;

namespace TagCloud.Client.BitmapExporter
{
    public class CaseResult
    {
        public readonly string Description;
        public readonly Bitmap Image;

        public CaseResult(string description, Bitmap image)
        {
            Description = description;
            Image = image;
        }

        public static CaseResult Create(Case c, Bitmap image)
        {
            return new CaseResult(c.GetDescription(), image);
        }
    }
}