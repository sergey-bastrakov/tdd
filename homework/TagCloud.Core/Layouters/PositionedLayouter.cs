using TagCloud.Core.Layouters.Contract;
using TagCloud.Core.Math;

namespace TagCloud.Core.Layouters
{
    public class PositionedLayouter : IRectangleLayouter
    {
        private readonly IRectangleLayouter _rectangleLayouter;
        private readonly Vector _center;

        public PositionedLayouter(IRectangleLayouter rectangleLayouter, Vector center)
        {
            _rectangleLayouter = rectangleLayouter;
            _center = center;
        }

        public Rectangle Place(Vector rectangleSize)
        {
            return _rectangleLayouter.Place(rectangleSize).Move(_center);
        }
    }
}