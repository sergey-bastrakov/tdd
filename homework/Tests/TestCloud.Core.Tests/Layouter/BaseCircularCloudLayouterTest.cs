using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.Core.Drawing;
using TagCloud.Core.Layouters;
using TagCloud.Core.Layouters.Contract;
using TagCloud.Core.Math;
using Rectangle = TagCloud.Core.Math.Rectangle;

namespace TestCloud.Core.Tests.Layouter
{
    public abstract class BaseCircularCloudLayouterTest
    {
        private TrackedLayouter _trackedLayouter;

        [SetUp]
        public void SetUp()
        {
            _trackedLayouter = null;
        }

        [TearDown]
        public void TearDown()
        {
            if (_trackedLayouter == null)
            {
                return;
            }

            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            {
                return;
            }

            if (_trackedLayouter.Rectangles.Count <= 0)
            {
                return;
            }
            
            DumpResult();
            _trackedLayouter = null;
        }

        private void DumpResult()
        {
            List<Rectangle> rectangles = _trackedLayouter.Rectangles;
            Bitmap bitmap = rectangles.Colorize().ToBitmap();

            string temporaryFile = Path.GetTempFileName() + ".png";

            bitmap.Save(temporaryFile, ImageFormat.Png);

            TestContext.Out.WriteLine($"Tag cloud visualization saved to file {temporaryFile}");
        }

        public IRectangleLayouter CreateTrackedLayouter(double radius, int segments, double accuracy)
        {
            _trackedLayouter = new TrackedLayouter(new CircularCloudLayouter(radius, segments, accuracy));
            return _trackedLayouter;
        }

        private class TrackedLayouter : IRectangleLayouter
        {
            private readonly IRectangleLayouter _baseLayouter;

            public readonly List<Rectangle> Rectangles = new List<Rectangle>();

            public TrackedLayouter(IRectangleLayouter baseLayouter)
            {
                this._baseLayouter = baseLayouter;
            }

            public Rectangle Place(Vector rectangleSize)
            {
                Rectangle rect = _baseLayouter.Place(rectangleSize);
                Rectangles.Add(rect);
                return rect;
            }
        }
    }
}