using RKBalance.Models;
using SkiaSharp;
using System;
using System.Numerics;

namespace RKBalance.Services
{
    public enum SPStep { none = 0, initial = 1, trialweight = 2, influence = 3, weightplacement = 4 };

    public class SinglePlaneBalanceVectorTools : VectorTools
    {
        public SinglePlaneBalanceVectorTools()
        {
        }

        public void DrawGraph(SKImageInfo info, SKCanvas canvas)

        {
            DrawBalanceGraph(info, canvas);
        }

        public void DrawInitialVector(SKImageInfo info, SKCanvas canvas, SPBalanceRun br)

        {
            double MaxVector = 0.0;
            double ScaleFactor = 0.0;

            DrawBalanceGraph(info, canvas);

            if (br.InitialRunVector != null)
            {
                if (br.InitialRunVector.Magnitude > MaxVector) MaxVector = br.InitialRunVector.Magnitude;
            }

            ScaleFactor = 85.0 / MaxVector;

            if (br.InitialRunVector != null)
            {
                DrawVector(canvas, "Initial", br.InitialRunVector, ScaleFactor);
                DrawT(canvas, br.InitialRunVector.Str(), br.InitialRunVector.Color, 1,1);
            }
        }

        public void DrawTrialWeight(SKImageInfo info, SKCanvas canvas, SPBalanceRun br)

        {
            double MaxVector = 0.0;
            double ScaleFactor = 0.0;

            DrawBalanceGraph(info, canvas);

            if (br.InitialRunVector != null)
            {
                if (br.InitialRunVector.Magnitude > MaxVector) MaxVector = br.InitialRunVector.Magnitude;
            }

            ScaleFactor = 85.0 / MaxVector;

            if (br.InitialRunVector != null)
            {
                DrawVector(canvas, "Initial", br.InitialRunVector, ScaleFactor);
                DrawT(canvas, br.InitialRunVector.Str(), br.InitialRunVector.Color, 1,1);
            }
            if (br.TrialWeightVector != null)
            {
                DrawBalanceWeight(canvas, br.TrialWeightVector.Magnitude, br.TrialWeightVector.Phase, br.TrialWeightVector.Color);
                DrawT(canvas, br.TrialWeightVector.Str(), br.TrialWeightVector.Color, 1,2);
            }
        }

        public void DrawInfluenceVector(SKImageInfo info, SKCanvas canvas, SPBalanceRun br)

        {
            double MaxVector = 0.0;
            double ScaleFactor = 0.0;

            DrawBalanceGraph(info, canvas);

            if (br.InitialRunVector != null)
            {
                if (br.InitialRunVector.Magnitude > MaxVector) MaxVector = br.InitialRunVector.Magnitude;
            }
            if (br.TrialRunVector != null)
            {
                if (br.TrialRunVector.Magnitude > MaxVector) MaxVector = br.TrialRunVector.Magnitude;
            }
            if (br.InfluenceVector != null)
            {
                if (br.InfluenceVector.Magnitude > MaxVector) MaxVector = br.InfluenceVector.Magnitude;
            }

            ScaleFactor = 85.0 / MaxVector;

            if (br.TrialRunVector != null)
            {
                //DrawConnectVectors(canvas, "Influence", br.InfluenceVector.Color, br.InitialRunVector, br.TrialRunVector, ScaleFactor);
                DrawVector(canvas, "Trial", br.TrialRunVector, ScaleFactor);
                //DrawVector(canvas, "Influence", br.InfluenceVector, ScaleFactor);
                DrawT(canvas, br.TrialRunVector.Str(), br.TrialRunVector.Color, 2,1);
                //DrawT(canvas, br.InfluenceVector.Str(), br.InfluenceVector.Color, 3, 1);
            }
            if (br.InitialRunVector != null)
            {
                DrawVector(canvas, "Initial", br.InitialRunVector, ScaleFactor);
                DrawT(canvas, br.InitialRunVector.Str(), br.InitialRunVector.Color, 1,1);
            }
            if (br.TrialWeightVector != null)
            {
                DrawBalanceWeight(canvas, br.TrialWeightVector.Magnitude, br.TrialWeightVector.Phase, br.TrialWeightVector.Color);
                DrawT(canvas, br.TrialWeightVector.Str(),br.TrialWeightVector.Color, 1,2);
            }
        }

        public void DrawBalanceWeight(SKImageInfo info, SKCanvas canvas, SPBalanceRun br)

        {
            double MaxVector = 0.0;
            double ScaleFactor = 0.0;

            DrawBalanceGraph(info, canvas);

            if (br.InitialRunVector != null)
            {
                if (br.InitialRunVector.Magnitude > MaxVector) MaxVector = br.InitialRunVector.Magnitude;
            }
            if (br.TrialRunVector != null)
            {
                if (br.TrialRunVector.Magnitude > MaxVector) MaxVector = br.TrialRunVector.Magnitude;
            }
            if (br.InfluenceVector != null)
            {
                if (br.InfluenceVector.Magnitude > MaxVector) MaxVector = br.InfluenceVector.Magnitude;
            }

            ScaleFactor = 85.0 / MaxVector;

            if (br.TrialRunVector != null)
            {
                DrawConnectVectors(canvas, "Influence", br.InfluenceVector.Color, br.InitialRunVector, br.TrialRunVector, ScaleFactor);
                DrawVector(canvas, "Trial", br.TrialRunVector, ScaleFactor);
                DrawVector(canvas, "Influence", br.InfluenceVector, ScaleFactor);
                DrawT(canvas, br.TrialRunVector.Str(), br.TrialRunVector.Color, 2,1);
                DrawT(canvas, br.InfluenceVector.Str(), br.InfluenceVector.Color, 3, 1);
            }
            if (br.InitialRunVector != null)
            {
                DrawVector(canvas, "Initial", br.InitialRunVector, ScaleFactor);
                DrawT(canvas, br.InitialRunVector.Str(), br.InitialRunVector.Color, 1,1);
            }
            if (br.TrialWeightVector != null)
            {
                DrawBalanceWeight(canvas, br.TrialWeightVector.Magnitude, br.TrialWeightVector.Phase, br.TrialWeightVector.Color);
                DrawT(canvas, br.TrialWeightVector.Str(), br.TrialWeightVector.Color, 1,2);
            }
            if (br.TrialRunVector != null && br.InfluenceVector != null && br.TrialWeightVector != null)
            {
                DrawBalanceWeight(canvas, br.WeightPlacementVector.Magnitude, br.WeightPlacementVector.Phase, br.WeightPlacementVector.Color, 1.25f);
                DrawT(canvas, br.WeightPlacementVector.Str(), br.WeightPlacementVector.Color, 2,2);
            }
        }

        public STEVector GetInfluenceVector(STEVector InitialVector, STEVector TrialVector)
        {
            STEVector InfVector = new STEVector();
            InfVector.Color = SKColors.LightBlue;
            SKPoint fromPoint = new SKPoint();
            SKPoint toPoint = new SKPoint();

            fromPoint.X = ((float)Math.Cos((float)InitialVector.Phase * cvt) * (float)InitialVector.Magnitude);
            fromPoint.Y = ((float)Math.Sin((float)InitialVector.Phase * cvt) * (float)InitialVector.Magnitude);

            toPoint.X = ((float)Math.Cos((float)TrialVector.Phase * cvt) * (float)TrialVector.Magnitude);
            toPoint.Y = ((float)Math.Sin((float)TrialVector.Phase * cvt) * (float)TrialVector.Magnitude);

            toPoint.X -= fromPoint.X;
            toPoint.Y -= fromPoint.Y;

            InfVector.Magnitude = Math.Sqrt(Math.Pow(toPoint.X, 2.0) + Math.Pow(toPoint.Y, 2.0));

            var p = Math.Atan2(toPoint.Y, toPoint.X);
            p /= cvt;
            InfVector.Phase = (int)Math.Round(p, 0);

            return InfVector;
        }

        public int AngleBetween(Vector2 v1, Vector2 v2)
        {
            var ca = Vector2.Dot(v1, v2) / (Math.Abs(v1.Length()) * Math.Abs(v2.Length()));

            double mycalcInRadians = Math.Acos(ca);
            double mycalcInDegrees = mycalcInRadians * 180.0f / Math.PI;
            var rDeg = (int)Math.Round(mycalcInDegrees, 0);
            return rDeg;
        }

        public STEVector CalcBalanceWeight(STEVector initVector, STEVector trialWeight, STEVector inflVector)
        {
            var balWeight = new STEVector();

            var balWeightRatio = trialWeight.Magnitude / inflVector.Magnitude;

            balWeight.Magnitude = balWeightRatio * initVector.Magnitude;

            var negInitVectorVector2 = Vector2.Negate(initVector.XYCoord());
            var inflVectorVector2 = inflVector.XYCoord();
            var ab = AngleBetween(negInitVectorVector2, inflVectorVector2);
            balWeight.Phase = trialWeight.Phase + ab;

            balWeight.Color = SKColors.LightGreen;
            return balWeight;
        }
    }
}