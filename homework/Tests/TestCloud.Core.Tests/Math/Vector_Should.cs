using System;
using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Math;

namespace TestCloud.Core.Tests.Math
{
    [TestFixture(Author = "bastrakov.sergei@skbkontur.ru", Category = "Math")]
    public class Vector_Should
    {
        [Test]
        public void BeSame_WhenCompare()
        {
            Vector a = new Vector(10, 0);
            Vector b = new Vector(10, 0);

            a.Should().Be(b);
        }

        [Test]
        public void BeInitialized_WhenCreatedWithArguments()
        {
            Vector vector = new Vector(10, 4);

            vector.X.Should().Be(10);
            vector.Y.Should().Be(4);
        }

        [Test]
        public void ToString_ReturnHumanReadableFormat()
        {
            Vector vector = new Vector(10.13, -4);
            vector.ToString().Should().Be("(10.1300, -4.0000)");
        }

        [Test]
        [TestCaseSource(typeof(VectorTestData), nameof(VectorTestData.SumTestCases))]
        public Vector SumTwoVectors(Vector a, Vector b)
        {
            return a + b;
        }

        [Test]
        [TestCaseSource(typeof(VectorTestData), nameof(VectorTestData.SubTestCases))]
        public Vector SubtractOneVectorFromOther(Vector a, Vector b)
        {
            return a - b;
        }

        [Test]
        [TestCaseSource(typeof(VectorTestData), nameof(VectorTestData.MulByConstantTestCases))]
        public Vector MultiplyVectorByConstant(Vector a, double b)
        {
            return a * b;
        }

        [Test]
        [TestCaseSource(typeof(VectorTestData), nameof(VectorTestData.DivByConstantTestCases))]
        public Vector DivideVectorByConstant(Vector a, double b)
        {
            return a / b;
        }

        [Test]
        public void ThrowDivideByZeroException_WhenDivideByZero()
        {
            Vector vector = new Vector(1, 1);

            Vector _;
            vector.Invoking(v => { _ = v / 0.0D; }).ShouldThrowExactly<DivideByZeroException>();
        }

        [Test]
        [TestCaseSource(typeof(VectorTestData), nameof(VectorTestData.LengthTestCases))]
        public double ReturnLength(Vector vector)
        {
            return vector.Length;
        }

        private class VectorTestData
        {
            public static IEnumerable SumTestCases
            {
                // ReSharper disable once UnusedMember.Local
                get
                {
                    yield return new TestCaseData(new Vector(1, 2), new Vector(3, 4)).Returns(new Vector(4, 6));
                    yield return new TestCaseData(new Vector(-1, 2), new Vector(3, -4)).Returns(new Vector(2, -2));
                }
            }

            public static IEnumerable SubTestCases
            {
                // ReSharper disable once UnusedMember.Local
                get
                {
                    yield return new TestCaseData(new Vector(1, 2), new Vector(3, 4)).Returns(new Vector(-2, -2));
                    yield return new TestCaseData(new Vector(-1, 2), new Vector(3, -4)).Returns(new Vector(-4, 6));
                }
            }

            public static IEnumerable MulByConstantTestCases
            {
                // ReSharper disable once UnusedMember.Local
                get
                {
                    yield return new TestCaseData(new Vector(1, 2), 3).Returns(new Vector(3, 6));
                    yield return new TestCaseData(new Vector(-1, 2), -2).Returns(new Vector(2, -4));
                    yield return new TestCaseData(new Vector(-1, 2), 0).Returns(new Vector(0, 0));
                }
            }

            public static IEnumerable DivByConstantTestCases
            {
                // ReSharper disable once UnusedMember.Local
                get
                {
                    yield return new TestCaseData(new Vector(8, 2), 2).Returns(new Vector(4, 1));
                    yield return new TestCaseData(new Vector(-6, 4), -2).Returns(new Vector(3, -2));
                }
            }

            public static IEnumerable LengthTestCases
            {
                // ReSharper disable once UnusedMember.Local
                get
                {
                    yield return new TestCaseData(new Vector(3, 4)).Returns(5);
                    yield return new TestCaseData(new Vector(-3, 4)).Returns(5);
                    yield return new TestCaseData(new Vector(0, 0)).Returns(0);
                }
            }
        }
    }
}
