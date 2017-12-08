using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Math;

namespace TestCloud.Core.Tests.Math
{
    [TestFixture(Author = "bastrakov.sergei@skbkontur.ru", Category = "Math")]
    public class Double_Should
    {
        [Test]
        public void BeSame()
        {
            double a = 0.02;
            double b = 0.1 * 0.2;

            a.IsEqual(b).Should().BeTrue();
        }
    }
}