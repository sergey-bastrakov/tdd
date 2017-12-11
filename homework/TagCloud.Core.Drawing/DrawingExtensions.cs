using System.Drawing;
using TagCloud.Core.Math;

namespace TagCloud.Core.Drawing
{
    public static class DrawingExtensions
    {
        public static System.Drawing.Rectangle ToSystemRectangle(this Math.Rectangle rectangle)
        {
            return new System.Drawing.Rectangle(rectangle.MinPoint.ToSystemPoint(), rectangle.Size.ToSystemSize());
        }

        public static Point ToSystemPoint(this Vector vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static Size ToSystemSize(this Vector vector)
        {
            return new Size((int)vector.X, (int)vector.Y);
        }
    }
}