using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Layouters.Contract;
using TagCloud.Core.Math;
using TagCloud.Core.Shapes;

namespace TagCloud.Core.Layouters
{
    public class CircularCloudLayouter : IRectangleLayouter
    {
        private readonly CircleShapeFactory _placementShapeFactory;
        private readonly double _accuracy;

        private readonly List<Rectangle> _rectangles = new List<Rectangle>();

        public CircularCloudLayouter(double placementCircleRadius, int placementCircleSegments, double accuracy)
        {
            _placementShapeFactory = new CircleShapeFactory(placementCircleRadius, placementCircleSegments);
            _accuracy = accuracy;
        }
        
        public Rectangle Place(Vector size)
        {
            IEnumerable<Vector> placementShape = _placementShapeFactory.Create();
            IEnumerable<Rectangle> aroundShapeRectangles = PlaceAroundShape(placementShape, size);
            IEnumerable<Rectangle> arrangedRectangles = aroundShapeRectangles.Select(Arrange);
            Rectangle rectangle =  FindClosestRectangle(arrangedRectangles);

            _rectangles.Add(rectangle);

            return rectangle;
        }

        private Rectangle Arrange(Rectangle rectangle)
        {
            rectangle = ArrangeMoveOut(rectangle);
            rectangle = ArrangeMoveInto(rectangle);

            return rectangle;
        }

        private Rectangle ArrangeMoveInto(Rectangle rectangle)
        {
            Vector rectangleCenter;

            do
            {
                rectangleCenter = rectangle.Center;
                rectangle = ArrangeMoveIntoStep(rectangle, Vector.Empty);

                Vector verticalTarget = new Vector(rectangle.Center.X, 0);
                rectangle = ArrangeMoveIntoStep(rectangle, verticalTarget);

                Vector horisontalTarget = new Vector(0, rectangle.Center.Y);
                rectangle = ArrangeMoveIntoStep(rectangle, horisontalTarget);
            } while ((rectangleCenter - rectangle.Center).Length > _accuracy);

            return rectangle;
        }

        private Rectangle ArrangeMoveIntoStep(Rectangle rectangle, Vector target = default(Vector))
        {
            Vector vector = target - rectangle.Center;

            do
            {
                Rectangle nextRectangle = rectangle.Move(vector);

                bool isIntersects = IsIntersectsWithAny(nextRectangle);
                if (!isIntersects)
                {
                    rectangle = nextRectangle;
                    vector = (target - rectangle.Center) / 2D;
                }
                else
                {
                    vector /= 2;
                }

            } while (vector.Length > _accuracy);
            return rectangle;
        }

        private Rectangle ArrangeMoveOut(Rectangle rectangle)
        {
            Vector vector = rectangle.Center;
            bool isIntersects;
            Rectangle nextRectangle;

            do
            {
                nextRectangle = rectangle.Move(vector);
                isIntersects = IsIntersectsWithAny(nextRectangle);
                vector *= 2;

            } while (isIntersects);

            return nextRectangle;
        }

        private bool IsIntersectsWithAny(Rectangle rectangle)
        {
            for (var i = _rectangles.Count - 1; i >=0; i--)
            {
                Rectangle other = _rectangles[i];
                if (other.IntersectsWith(rectangle))
                {
                    return true;
                }
            }

            return false;
        }

        private static Rectangle FindClosestRectangle(IEnumerable<Rectangle> rectangles)
        {
            return rectangles.OrderBy(rect => rect.Center.Length).First();
        }

        private static IEnumerable<Rectangle> PlaceAroundShape(IEnumerable<Vector> shapePoints, Vector size)
        {
            return shapePoints.Select(position => new Rectangle(position, size));
        }
    }
}
