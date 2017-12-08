using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace TagCloud.Core.Math
{
    public struct Vector
    {
        public readonly double X;
        public readonly double Y;

        public double Length => System.Math.Sqrt(X * X + Y * Y);

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector && Equals((Vector) obj);
        }

        [Pure]
        public bool Equals(Vector other)
        {
            return X - other.X < double.Epsilon && Y - other.Y < double.Epsilon;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
        
        public override string ToString()
        {
            return $"({X.ToString("F4", new CultureInfo("en-US"))}, {Y.ToString("F4", new CultureInfo("en-US"))})";
        }
        
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }
        
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }
        
        public static Vector operator *(Vector vector, double c)
        {
            return new Vector(vector.X * c, vector.Y * c);
        }
        
        public static Vector operator /(Vector vector, double c)
        {
            if (System.Math.Abs(c) < double.Epsilon)
            {
                throw new DivideByZeroException();
            }

            return new Vector(vector.X / c, vector.Y / c);
        }
    }
}
