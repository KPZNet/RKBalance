using System;

namespace RKBalance.Models
{
    public class BindingVector
    {
        public BindingVector()
        {
            MagnitudeStr = "1.0";
            PhaseStr = "70";
        }

        public double Magnitude
        {
            get
            {
                return Convert.ToDouble(MagnitudeStr);
            }
            set
            {
                MagnitudeStr = value.ToString("N2");
            }
        }

        public int Phase
        {
            get
            {
                return Convert.ToInt32(PhaseStr);
            }
            set
            {
                PhaseStr = value.ToString();
            }
        }

        public string MagnitudeStr
        {
            get; set;
        }

        public string PhaseStr
        {
            get; set;
        }
    }
}