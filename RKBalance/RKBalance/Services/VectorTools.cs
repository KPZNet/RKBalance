using RKBalance.Models;
using SkiaSharp;
using System;

namespace RKBalance.Services
{
    public class VectorTools
    {
        private const int smallStroke = 1;
        protected float cvt = (float)(Math.PI) / 180.0f;

        protected float GRAPH_RADIUS = 100;
        protected float GRAPH_MARGIN = 40;
        protected float GRAPH_SIZE = 120;

        protected float xmax;
        protected float ymax;

        private SKPaint paintBlack = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.Black,
            StrokeWidth = smallStroke,
            IsAntialias = true
        };

        private SKPaint paintBlackFill = new SKPaint
        {
            Style = SKPaintStyle.StrokeAndFill,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.Black,
            StrokeWidth = smallStroke,
            IsAntialias = true
        };

        private SKPaint paintGraphFill = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            StrokeCap = SKStrokeCap.Round,
            Color = SKColors.LightSlateGray,
            StrokeWidth = smallStroke,
            IsAntialias = true
        };

        private SKPaint paintBalanceHoles = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = smallStroke,
            IsAntialias = true
        };

        protected void DrawBalanceGraph(SKImageInfo info, SKCanvas canvas)
        {
            float scaler = (GRAPH_RADIUS * 2) + GRAPH_MARGIN;

            canvas.Clear();

            var scaleCart = Math.Min(info.Width, info.Height);
            var scaleX = scaleCart / scaler;
            var scaleY = scaleCart / scaler;

            canvas.Translate(info.Width / 2.0f, info.Height / 2.0f);
            canvas.Scale(scaleX, -scaleY);
            canvas.RotateDegrees(0);

            xmax = (float)info.Width / scaleX / 2.0f;
            ymax = (float)info.Height / scaleY / 2.0f;

            canvas.DrawCircle(0, 0, GRAPH_RADIUS, paintBlack);
            canvas.DrawCircle(0, 0, GRAPH_RADIUS, paintGraphFill);

            canvas.DrawCircle(0, 0, 2, paintBlackFill);

            canvas.Save();
            for (int i = 0; i < 360; i += 15)
            {
                canvas.DrawCircle(0, GRAPH_RADIUS - (0.06f * GRAPH_RADIUS), (GRAPH_RADIUS * 0.03f), paintBalanceHoles);
                canvas.RotateDegrees(15);
            }
            canvas.Restore();

            canvas.Save();
            canvas.Scale(1, -1);

            for (int t = 0; t < 360; t += 45)
            {
                SKPaint textPaint = new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true
                };

                string degText = t.ToString();
                SKRect textRect = new SKRect();
                textPaint.MeasureText(degText, ref textRect);
                textPaint.TextSize = GRAPH_RADIUS * 0.08f;
                textPaint.MeasureText(degText, ref textRect);

                var rads = cvt * t;
                var xCos = (float)Math.Cos(rads);
                var ySin = (float)Math.Sin(rads);
                var xPos = (GRAPH_RADIUS + 3f) * xCos;
                var yPos = (GRAPH_RADIUS + 3f) * ySin;

                var xSlider = ((xCos - 1f) / 2.0f) * textRect.Width;
                var ySlider = (textRect.Height / 2f) - ((ySin / 2f) * textRect.Height);

                canvas.DrawText(degText, xPos + xSlider, -yPos + ySlider, textPaint);
            }

            canvas.Restore();
        }

        public int RoundTo(int value, int roundTo)
        {
            return (int)Math.Round((double)value / roundTo) * roundTo;
        }

        protected void DrawBalanceWeight(SKCanvas canvas, double balanceWeight, int degrees, SKColor color, float sizeMultiplier = 1.0f)
        {
            SKPaint paintWeightFill = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = color,
                StrokeWidth = smallStroke,
                IsAntialias = true
            };
            SKPaint paintWeightStroke = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = smallStroke,
                IsAntialias = true
            };

            degrees = RoundTo(degrees, 15);

            float x = (float)Math.Cos((float)degrees * cvt) * (GRAPH_RADIUS - (0.06f * GRAPH_RADIUS));
            float y = (float)Math.Sin((float)degrees * cvt) * (GRAPH_RADIUS - (0.06f * GRAPH_RADIUS));

            canvas.Save();
            canvas.DrawCircle(x, y, (GRAPH_RADIUS * (0.04f) * sizeMultiplier), paintWeightFill);
            canvas.DrawCircle(x, y, (GRAPH_RADIUS * (0.04f) * sizeMultiplier), paintWeightStroke);
            canvas.Restore();
        }

        protected void DrawT(SKCanvas canvas, string label, SKColor color, int row, int quadrant, bool bolder = false)
        {
            SKPaint ballPaint = new SKPaint
            {
                Color = color,
                IsAntialias = true,
                Style = SKPaintStyle.StrokeAndFill,
            };
            SKPaint textPaint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.StrokeAndFill,
            };
            SKPaint textPaintBold = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.StrokeAndFill,
                
            };
            SKPaint textPainter = bolder ? textPaintBold : textPaint;

            float ballSize, ballX, ballY, YSpot, XSpot;
            GetSlot(canvas, label, row, quadrant, textPainter, out ballSize, out ballX, out ballY, out YSpot, out XSpot);

            canvas.DrawCircle(ballX, ballY, ballSize, ballPaint);
            canvas.DrawText(label, XSpot, YSpot, textPainter);
            canvas.Restore();
        }

        private void GetSlot(SKCanvas canvas, string label, int row, int quadrant, SKPaint textPaint, out float ballSize, 
                             out float ballX, out float ballY, out float YSpot, out float XSpot)
        {
            textPaint.TextSize = GRAPH_RADIUS * 0.08f;

            SKRect textRect = new SKRect();
            textPaint.MeasureText(label, ref textRect);

            canvas.Save();
            canvas.Scale(1, -1);

            ballSize = (GRAPH_RADIUS * (0.04f));
            float ballMargin = ballSize * 2.0f;

            YSpot = 0.0f;
            XSpot = 0.0f;
            ballX = 0.0f;
            ballY = 0.0f;
            float margin = 1.50f;

            if (quadrant == 1)
            {
                float X = -xmax;
                float Y = -ymax;
                
                YSpot = (float)(Y + (textRect.Height * margin * row));
                XSpot = X + ballMargin;
                ballX = X + ballSize;

                ballY = YSpot - (0.5f * textRect.Height);
            }
            if (quadrant == 2)
            {
                float X = xmax;
                float Y = -ymax;

                YSpot = (float)(Y + (textRect.Height * margin * row));
                XSpot = X;
                XSpot -= (textRect.Width * 1.1f);

                ballX = XSpot - ballSize;
                ballY = YSpot - (0.5f * textRect.Height);
            }
            if (quadrant == 3)
            {
                float X = -xmax;
                float Y = ymax - (textRect.Height * 0.25f); //bottombump

                YSpot = (float)(Y - (textRect.Height * margin * (row -1) ));
                XSpot = X + ballMargin;
                ballX = X + ballSize;

                ballY = YSpot - (0.5f * textRect.Height);
            }
            if (quadrant == 4)
            {
                float X = xmax;
                float Y = ymax - (textRect.Height * 0.25f); //bottombump

                YSpot = (float)(Y - (textRect.Height * margin * (row - 1)));
                XSpot = X;
                XSpot -= (textRect.Width * 1.1f);

                ballX = XSpot - ballSize;
                ballY = YSpot - (0.5f * textRect.Height);
            }
        }

        protected void DrawLegend(SKCanvas canvas, string label, SKPoint fromPoint, SKPoint toPoint, SKColor color, float ScaleFactor)
        {
            // Create an SKPaint object to display the text
            SKPaint textPaint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true
            };

            SKPoint midPoint = new SKPoint();

            SKRect textRect = new SKRect();

            textPaint.TextSize = GRAPH_RADIUS * 0.08f;
            textPaint.MeasureText(label, ref textRect);

            const float margin = 0.50f;

            midPoint.X = fromPoint.X + ((toPoint.X - fromPoint.X) * margin);
            midPoint.Y = fromPoint.Y + ((toPoint.Y - fromPoint.Y) * margin);

            SKPoint IMidPoint = new SKPoint
            {
                X = -midPoint.X,
                Y = -midPoint.Y
            };

            //DrawTextRect(canvas, midPoint, textRect);

            // And draw the text
            canvas.Save();
            canvas.Translate(midPoint);
            canvas.Scale(1, -1);

            canvas.Translate(IMidPoint);

            canvas.DrawText(label, midPoint.X, midPoint.Y, textPaint);
            canvas.Restore();
        }

        protected static void DrawTextRect(SKCanvas canvas, SKPoint midPoint, SKRect textRect)
        {
            canvas.Save();
            // Create a new SKRect object for the frame around the text
            SKRect frameRect = textRect;
            //frameRect.Offset(midPoint.X, midPoint.Y);
            //frameRect.Inflate(3, 2);
            // Create an SKPaint object to display the frame
            SKPaint framePaint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                StrokeWidth = 0.5f,
                Color = SKColors.White
            };
            canvas.Translate(midPoint);
            canvas.Scale(1, -1);
            //canvas.Translate(IMidPoint);
            canvas.DrawRoundRect(frameRect, 2, 2, framePaint);
            canvas.Restore();
        }

        protected void DrawConnectVectors(SKCanvas canvas, string label, SKColor color, STEVector v1, STEVector v2, double ScaleFactor)
        {
            SKPoint fromPoint = new SKPoint();
            SKPoint toPoint = new SKPoint();

            fromPoint.X = ((float)Math.Cos((float)v1.Phase * cvt) * (float)v1.Magnitude);
            fromPoint.Y = ((float)Math.Sin((float)v1.Phase * cvt) * (float)v1.Magnitude);

            toPoint.X = ((float)Math.Cos((float)v2.Phase * cvt) * (float)v2.Magnitude);
            toPoint.Y = ((float)Math.Sin((float)v2.Phase * cvt) * (float)v2.Magnitude);

            const float margin = 0.0f;

            fromPoint.X += ((toPoint.X - fromPoint.X) * margin);
            fromPoint.Y += ((toPoint.Y - fromPoint.Y) * margin);

            toPoint.X -= ((toPoint.X - fromPoint.X) * margin);
            toPoint.Y -= ((toPoint.Y - fromPoint.Y) * margin);

            DrawCartVector(canvas, label, fromPoint, toPoint, color, ScaleFactor);
        }

        protected void ArrowHead(SKCanvas canvas, SKPoint pFrom, SKPoint pTo, SKColor color)
        {
            canvas.Save();

            SKPath kPath = new SKPath();

            kPath.MoveTo(new SKPoint(-1.3f, -3));
            kPath.LineTo(new SKPoint(0, 0));
            kPath.LineTo(new SKPoint(1.3f, -3));
            kPath.LineTo(new SKPoint(-1.3f, -3));

            kPath.Close();

            float theta = (float)Math.Atan2((float)pTo.Y - pFrom.Y, (float)pTo.X - pFrom.X);

            canvas.Translate(pTo);
            canvas.RotateRadians(theta - (float)Math.PI / 2.0f);

            SKPaint paintBrush = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                StrokeCap = SKStrokeCap.Round,
                Color = color,
                StrokeWidth = 1,
                IsAntialias = true
            };
            canvas.DrawPath(kPath, paintBrush);

            canvas.Restore();
        }

        protected void ArrowHead(SKCanvas canvas, SKPoint pTo, SKColor color)
        {
            canvas.Save();

            SKPath kPath = new SKPath();

            kPath.MoveTo(new SKPoint(-1.3f, 0));
            kPath.LineTo(new SKPoint(0, 3));
            kPath.LineTo(new SKPoint(1.3f, 0));
            kPath.LineTo(new SKPoint(-1.3f, 0));

            kPath.Close();

            float theta = (float)Math.Atan2((float)pTo.Y, (float)pTo.X);

            canvas.Translate(pTo);
            canvas.RotateRadians(theta - (float)Math.PI / 2.0f);

            SKPaint paintBrush = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                StrokeCap = SKStrokeCap.Round,
                Color = color,
                StrokeWidth = 1,
                IsAntialias = true
            };
            canvas.DrawPath(kPath, paintBrush);

            canvas.Restore();
        }

        protected void DrawCartVector(SKCanvas canvas, string label, SKPoint pFrom, SKPoint pTo, SKColor color, double ScaleFactor)
        {
            SKPath kPath = new SKPath();

            pFrom.X *= (float)ScaleFactor;
            pFrom.Y *= (float)ScaleFactor;

            pTo.X *= (float)ScaleFactor;
            pTo.Y *= (float)ScaleFactor;

            kPath.MoveTo(pFrom);
            kPath.LineTo(pTo.X, pTo.Y);

            kPath.Close();

            SKPaint paintBrush = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Round,
                Color = color,
                StrokeWidth = 1,
                IsAntialias = true
            };
            canvas.DrawPath(kPath, paintBrush);
            ArrowHead(canvas, pFrom, pTo, color);

            DrawLegend(canvas, label, pFrom, pTo, color, (float)ScaleFactor);
        }

        protected void DrawVector(SKCanvas canvas, string label, STEVector v, double ScaleFactor)
        {
            float x = (float)Math.Cos((float)v.Phase * cvt) * (float)v.Magnitude;
            float y = (float)Math.Sin((float)v.Phase * cvt) * (float)v.Magnitude;

            DrawCartVector(canvas, label, new SKPoint(0, 0), new SKPoint(x, y), v.Color, ScaleFactor);
        }
    }
}