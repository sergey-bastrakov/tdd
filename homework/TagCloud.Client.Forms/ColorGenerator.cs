using System;
using System.Drawing;

namespace TagCloud.Client.Forms
{
    public class ColorGenerator
    {
        private readonly Random _random;

        public ColorGenerator(Random random)
        {
            _random = random;
        }

        public Color GetRandomColor()
        {
            return Color.FromArgb(_random.Next(50, 255), _random.Next(50, 255), _random.Next(50, 255));
        }
    }
}