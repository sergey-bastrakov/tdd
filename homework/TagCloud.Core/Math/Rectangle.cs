namespace TagCloud.Core.Math
{
    public struct Rectangle
    {
        public readonly Vector Center;
        public readonly Vector Size;

        public Vector MinPoint { get; }
        public Vector MaxPoint { get; }

        public double Left { get; }
        public double Right { get; }
        public double Bottom { get; }
        public double Top { get; }

        public Rectangle(Vector center, Vector size)
        {
            Center = center;
            Size = size;

            MinPoint = Center - Size / 2;
            MaxPoint = Center + Size / 2;

            Left = MinPoint.X;
            Right = MaxPoint.X;
            Bottom = MinPoint.Y;
            Top = MaxPoint.Y;
        }

        public Rectangle Move(Vector vector) => new Rectangle(Center + vector, Size);
        public Rectangle Move(double x, double y) => Move(new Vector(x, y));
        public bool IntersectsWith(Rectangle other) => Right > other.Left &&
                                                       other.Right > Left &&
                                                       Bottom < other.Top &&
                                                       other.Bottom < Top;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rectangle && Equals((Rectangle) obj);
        }

        public bool Equals(Rectangle other)
        {
            return Center.Equals(other.Center) && Size.Equals(other.Size);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Center.GetHashCode() * 397) ^ Size.GetHashCode();
            }
        }
        
        public override string ToString()
        {
            return $"{{ Center: {Center}, Size: {Size} }}";
        }
    }
}
