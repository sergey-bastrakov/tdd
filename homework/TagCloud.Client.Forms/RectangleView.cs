using System.Drawing;

namespace TagCloud.Client.Forms
{
    public class RectangleView
    {
        public Rectangle Rectangle { get; }
        public Color Color { get; }

        public RectangleView(Rectangle rectangle, Color color)
        {
            Rectangle = rectangle;
            Color = color;
        }
    }
}