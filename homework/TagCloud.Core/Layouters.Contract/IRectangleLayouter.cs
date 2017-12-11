using TagCloud.Core.Math;

namespace TagCloud.Core.Layouters.Contract
{
    public interface IRectangleLayouter
    {
        Rectangle Place(Vector rectangleSize);
    }
}