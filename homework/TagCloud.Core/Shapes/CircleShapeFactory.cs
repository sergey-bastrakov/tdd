using System.Collections.Generic;
using TagCloud.Core.Math;

namespace TagCloud.Core.Shapes
{
    public class CircleShapeFactory
    {
        private readonly double _raduis;
        private readonly int _segments;

        public CircleShapeFactory(double raduis, int segments)
        {
            _raduis = raduis;
            _segments = segments;
        }

        public IEnumerable<Vector> Create()
        {
            return new Circle(_raduis, _segments);
        }
    }
}
