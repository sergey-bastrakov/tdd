namespace TagCloud.Core.Math
{
    public static class DoubleExtensions
    {
        public static bool IsEqual(this double a, double b, double accuracy = double.Epsilon)
        {
            return a - b < accuracy;
        }
    }
}
