using SkiaSharp;

namespace RKBalance.Models
{
    public class STEVector : STVector
    {
        public SKColor Color;
        public string Label;

        public STEVector()
        {
            Magnitude = 0.0;
            Phase = 0;
            Color = SKColors.Black;
            Label = "unset";
        }

        public STEVector cpy()
        {
            var c = new STEVector();
            c.Color = Color;
            c.Label = Label;
            c.Magnitude = Magnitude;
            c.Phase = Phase;

            return c;
        }
    }
}