using System.Drawing;

namespace TagCloud.Core.Drawing
{
    public struct RectangleView
    {
        public readonly Math.Rectangle Rectangle;
        public readonly Color Color;

        public RectangleView(Math.Rectangle rectangle, Color color)
        {
            Rectangle = rectangle;
            Color = color;
        }
    }
}