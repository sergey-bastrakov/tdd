using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Math;

namespace TestCloud.Core.Tests.Math
{
    [TestFixture]
    public class Circle_Should
    {
        [Test]
        public void EnumerateNeededPointsCount()
        {
            int count = new Circle(10, 7).Count();
            count.Should().Be(7);
        }

        [Test]
        public void EnumerateVectors_BelongSpecifiedCircle()
        {
            new Circle(10, 8).Select(radiusVector => radiusVector.Length).ShouldAllBeEquivalentTo(10);
        }

        [Test]
        public void EnumerateVectors_AreDifferent()
        {
            Vector[] vectors = new Circle(10, 8).ToArray();
            (vectors.Distinct().Count() == vectors.Length).Should().BeTrue();
        }
    }
}