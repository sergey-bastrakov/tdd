using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.Math
{
    public static class RectangleExtensions
    {
        public static Rectangle GetBounds(this IEnumerable<Rectangle> rectangles)
        {
            Rectangle bounds = rectangles.Aggregate(new Rectangle(), (current, view) => current.Encapsulate(view));
            return bounds;
        }
    }
}
