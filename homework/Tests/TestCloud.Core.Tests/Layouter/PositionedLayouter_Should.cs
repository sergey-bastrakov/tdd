using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Layouters;
using TagCloud.Core.Layouters.Contract;
using TagCloud.Core.Math;

namespace TestCloud.Core.Tests.Layouter
{
    [TestFixture]
    public class PositionedLayouter_Should
    {
        [Test]
        public void MoveGeneratedRectangles_RelativeCenter()
        {
            PositionedLayouter layouter = new PositionedLayouter(new LayouterMock(), new Vector(11, 22));
            Rectangle rectangle = layouter.Place(new Vector(15, 12));

            rectangle.Center.Should().Be(new Vector(11, 22));
        }

        public class LayouterMock : IRectangleLayouter
        {
            public Rectangle Place(Vector rectangleSize)
            {
                return new Rectangle(Vector.Empty, rectangleSize);
            }
        }
    }
}