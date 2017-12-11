using System;

namespace TagCloud.Client.BitmapExporter
{
    public class Case
    {
        public int PlacementRadius = 200;
        public int PlacementSegments = 32;
        public double Accuracy = 0.1;
        public int Count = 100;
        public int MinWidth = 40;
        public int MaxWidth = 120;
        public int MinHeight = 40;
        public int MaxHeght = 80;
        public int Seed = DateTime.Now.Millisecond;

        public string GetDescription() => $"{GetFieldDescription(nameof(Seed), $"{Seed:D}")}" +
                                          $"{GetFieldDescription(nameof(PlacementRadius), $"{PlacementRadius:D}")}" +
                                          $"{GetFieldDescription(nameof(PlacementSegments), $"{PlacementSegments:D}")}" +
                                          $"{GetFieldDescription(nameof(Accuracy), $"{Accuracy:F2}")}" +
                                          $"{GetFieldDescription(nameof(Count), $"{Count:D}")}" +
                                          $"{GetFieldDescription(nameof(MinWidth), $"{MinWidth:D}")}" +
                                          $"{GetFieldDescription(nameof(MaxWidth), $"{MaxWidth:D}")}" +
                                          $"{GetFieldDescription(nameof(MinHeight), $"{MinHeight:D}")}" +
                                          $"{GetFieldDescription(nameof(MaxHeght), $"{Seed:D}")}";

        private static string GetFieldDescription(string name, string value) => $"{name}={value};";

        public override string ToString() => GetDescription();
    }
}