namespace TagCloud.Core.Math
{
    public struct Rectangle
    {
        public readonly Vector Center;
        public readonly Vector Size;

        public Vector MinPoint => Center - Size / 2;
        public Vector MaxPoint => Center + Size / 2;

        public double Left => MinPoint.X;
        public double Right => MaxPoint.X;
        public double Bottom => MinPoint.Y;
        public double Top => MaxPoint.Y;

        public Rectangle(Vector center, Vector size)
        {
            Center = center;
            Size = size;
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
