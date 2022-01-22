using System;
using System.Numerics;

namespace RKBalance.Models
{
    public class STVector
    {
        protected float cvt = (float)(Math.PI) / 180.0f;

        public STVector()
        {
            Magnitude = 1.0;
            Phase = 90;
        }

        public double Magnitude { get; set; }
        public int Phase { get; set; }

        public Vector2 XYCoord()
        {
            Vector2 v = new Vector2
            {
                X = (float)((float)Math.Cos((float)Phase * cvt) * Magnitude),
                Y = (float)((float)Math.Sin((float)Phase * cvt) * Magnitude)
            };
            return v;
        }

        public float DotProduct(STEVector v2)
        {
            return Vector2.Dot(XYCoord(), v2.XYCoord());
        }

        public string Str()
        {
            return MagnitudeStr + "@" + PhaseStr;
        }

        public string MagnitudeStr
        {
            get { return Magnitude.ToString("N2"); }
            set
            {
                try
                {
                    Magnitude = Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    Magnitude = 0.0;
                }
            }
        }

        public string PhaseStr
        {
            get { return Phase.ToString(); }
            set
            {
                try
                {
                    Phase = Convert.ToInt32(value);
                }
                catch (Exception)
                {
                    Magnitude = 0;
                }
            }
        }
    }
}