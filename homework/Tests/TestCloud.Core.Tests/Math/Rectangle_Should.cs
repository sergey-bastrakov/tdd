using System.Collections;
using FluentAssertions;
using TagCloud.Core.Math;

using NUnit.Framework;

namespace TestCloud.Core.Tests.Math
{
    [TestFixture]
    public class Rectangle_Should
    {
        [Test]
        public void BeInitialized_WhenCreatedWithArguments()
        {
            Rectangle rect = CreateRectangle(1, 2, 3, 4);

            rect.Center.Should().Be(new Vector(1, 2));
            rect.Size.Should().Be(new Vector(3, 4));
        }

        [Test]
        public void BeSame_WhenCompare()
        {
            Rectangle rectA = CreateRectangle(1, 2, 3, 4);
            Rectangle rectB = CreateRectangle(1, 2, 3, 4);

            rectA.Should().Be(rectB);
        }

        [Test]
        public void ToString_ReturnHumanReadableFormat()
        {
            Vector center = new Vector(1, 2);
            Vector size = new Vector(3, 4);

            Rectangle rect = new Rectangle(center, size);

            rect.ToString().Should().Be($"{{ Center: {center}, Size: {size} }}");
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.MinPointTestCases))]
        public Vector ReturnMinPoint(Rectangle rect)
        {
            return rect.MinPoint;
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.MaxPointTestCases))]
        public Vector ReturnMaxPoint(Rectangle rect)
        {
            return rect.MaxPoint;
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.LeftPointTestCases))]
        public double ReturnLeftPoint(Rectangle rect)
        {
            return rect.Left;
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.RightPointTestCases))]
        public double ReturnRightPoint(Rectangle rect)
        {
            return rect.Right;
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.BottomPointTestCases))]
        public double ReturnBottomPoint(Rectangle rect)
        {
            return rect.Bottom;
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.TopPointTestCases))]
        public double ReturnTopPoint(Rectangle rect)
        {
            return rect.Top;
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.MoveByVectorTestCases))]
        public Rectangle MoveByVector(Rectangle rect, Vector vector)
        {
            return rect.Move(vector);
        }

        [Test]
        [TestCaseSource(typeof(RectangleTestCaseData), nameof(RectangleTestCaseData.MoveByCoordinatesTestCases))]
        public Rectangle MoveByCoordinates(Rectangle rect, double x, double y)
        {
            return rect.Move(x, y);
        }

        [Test]
        public void Intersect_TwoRectangles()
        {
            Rectangle rectA = CreateRectangle(0, 0, 10, 10);
            Rectangle rectB = CreateRectangle(4, 4, 10, 10);

            rectA.IntersectsWith(rectB).Should().BeTrue();
        }

        [Test]
        public void NotIntersect_TwoRectangles()
        {
            Rectangle rectA = CreateRectangle(0, 0, 10, 10);
            Rectangle rectB = CreateRectangle(10, 10, 10, 10);

            rectA.IntersectsWith(rectB).Should().BeFalse();
        }

        private static Rectangle CreateRectangle(double x, double y, double width, double height)
        {
            return new Rectangle(new Vector(x, y), new Vector(width, height));
        }

        private class RectangleTestCaseData
        {
            public static IEnumerable MinPointTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8)).Returns(new Vector(-2, -4));
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8)).Returns(new Vector(-2, 0));
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8)).Returns(new Vector(0, 0));
                }
            }

            public static IEnumerable MaxPointTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8)).Returns(new Vector(2, 4));
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8)).Returns(new Vector(2, 8));
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8)).Returns(new Vector(4, 8));
                }
            }

            public static IEnumerable LeftPointTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8)).Returns(-2);
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8)).Returns(-2);
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8)).Returns(0);
                }
            }

            public static IEnumerable RightPointTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8)).Returns(2);
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8)).Returns(2);
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8)).Returns(4);
                }
            }

            public static IEnumerable BottomPointTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8)).Returns(-4);
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8)).Returns(0);
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8)).Returns(0);
                }
            }

            public static IEnumerable TopPointTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8)).Returns(4);
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8)).Returns(8);
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8)).Returns(8);
                }
            }

            public static IEnumerable MoveByVectorTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8), new Vector(0, 0)).Returns(CreateRectangle(0, 0, 4, 8));
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8), new Vector(2, 0)).Returns(CreateRectangle(2, 4, 4, 8));
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8), new Vector(0, 2)).Returns(CreateRectangle(2, 6, 4, 8));
                }
            }

            public static IEnumerable MoveByCoordinatesTestCases
            {
                get
                {
                    yield return new TestCaseData(CreateRectangle(0, 0, 4, 8), 0, 0).Returns(CreateRectangle(0, 0, 4, 8));
                    yield return new TestCaseData(CreateRectangle(0, 4, 4, 8), 2, 0).Returns(CreateRectangle(2, 4, 4, 8));
                    yield return new TestCaseData(CreateRectangle(2, 4, 4, 8), 0, 2).Returns(CreateRectangle(2, 6, 4, 8));
                }
            }
        }
    }
}
