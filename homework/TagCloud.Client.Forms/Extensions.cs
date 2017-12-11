using System.Drawing;

namespace TagCloud.Client.Forms
{
    public static class Extensions
    {
        public static System.Drawing.Rectangle ToRectangle(this TagCloud.Core.Math.Rectangle rectangle)
        {
            return new System.Drawing.Rectangle(rectangle.MinPoint.ToPoint(), rectangle.Size.ToSize());
        }

        public static Point ToPoint(this TagCloud.Core.Math.Vector vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static Size ToSize(this TagCloud.Core.Math.Vector vector)
        {
            return new Size((int)vector.X, (int)vector.Y);
        }
    }
}