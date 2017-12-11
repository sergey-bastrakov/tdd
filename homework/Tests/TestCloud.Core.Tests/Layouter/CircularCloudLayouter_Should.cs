using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Layouters;
using TagCloud.Core.Math;

namespace TestCloud.Core.Tests.Layouter
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        [Test]
        public void Return_Rectangles()
        {
            CircularCloudLayouter layouter = new CircularCloudLayouter(10, 8, 0.1);

            Rectangle rectA = layouter.Place(new Vector(10, 10));
            Rectangle rectB = layouter.Place(new Vector(10, 10));

            rectA.ShouldBeEquivalentTo(new Rectangle(Vector.Empty, new Vector(10, 10)));
            rectB.Center.Should().NotBe(Vector.Empty);
        }

        [Test]
        public void GenerateRectangles_WhichPlacedWithMaxDensity()
        {
            CircularCloudLayouter layouter = new CircularCloudLayouter(10, 32, 0.1);
            Vector rectangleSize = new Vector(40, 40);
            
            List<Rectangle> rectangles = Enumerable.Repeat(rectangleSize, 97).Select(size => layouter.Place(size)).ToList();

            Rectangle bounds = rectangles.Aggregate(new Rectangle(), (current, rect) => current.Encapsulate(rect));

            double circleArea = System.Math.PI * 220 * 220;
            double shapeArea = bounds.Area;

            System.Math.Abs(circleArea / shapeArea).Should().BeGreaterThan(0.75);
        }

        [Test]
        public void GenerateRectangles_WhichPlacedInCircle()
        {
            CircularCloudLayouter layouter = new CircularCloudLayouter(10, 32, 0.1);
            Vector rectangleSize = new Vector(40, 40);

            List<Rectangle> rectangles = Enumerable.Repeat(rectangleSize, 97).Select(size => layouter.Place(size)).ToList();
            foreach (double distanceToCenter in rectangles.Select(rect => rect.Center.Length))
            {
                distanceToCenter.Should().BeLessThan(220);
            }
        }
    }
}
