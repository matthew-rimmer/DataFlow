using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DataFlow.ChartClasses
{
    class ExponentialFit : Regression
    {
        public double A { get; set; }
        public double B { get; set; }

        public int DecayLineNum { get; set; } = 5;

        public ExponentialFit() { }

        public ExponentialFit(ObservableCollection<CoordPoint> coordinates, Canvas currentCanvas, ChartBounds CurrentBounds)
        {
            this.currentCanvas = currentCanvas;
            this.maxBoundsX = CurrentBounds.MaxBoundsX;
            this.minBoundsX = CurrentBounds.MinBoundsX;
            this.maxBoundsY = CurrentBounds.MaxBoundsY;
            this.minBoundsY = CurrentBounds.MinBoundsY;
            this.coordinates = coordinates;

            // Puts the X and Y values into their on list so they can be calculated on
            for (int i = 0; i < coordinates.Count; i++)
            {
                XValues.Add(coordinates[i].X);
            }

            for (int i = 0; i < coordinates.Count; i++)
            {
                YValues.Add(coordinates[i].Y);
            }

            // Sets the curve values
            A = GetA();
            B = GetB();


        }

        public double GetA()
        {
            // Calculations for A 
            double a = 0;
            double numerator = 0;
            double denominator = 0;

            numerator = (SumOf(false, XValues, false, XValues, false, YValues)
                * SumOf(false, YValues, true, YValues))
                -
                (SumOf(false, XValues, false, YValues)
                * SumOf(false, XValues, false, YValues, true, YValues));

            denominator = (SumOf(false, YValues)
                * SumOf(false, XValues, false, XValues, false, YValues))
                -
                Math.Pow(SumOf(false, XValues, false, YValues), 2);

            a = numerator / denominator;

            return Math.Exp(a);
        }

        public double GetB()
        {
            double b = 0;
            // Calculations for B 
            double numerator = 0;
            double denominator = 0;

            numerator = (SumOf(false, YValues)
                * SumOf(false, XValues, false, YValues, true, YValues))
                -
                (SumOf(false, XValues, false, YValues)
                * SumOf(false, YValues, true, YValues));

            denominator = (SumOf(false, YValues)
                * SumOf(false, XValues, false, XValues, false, YValues))
                -
                Math.Pow(SumOf(false, XValues, false, YValues), 2);

            b = numerator / denominator;

            return b;
        }


        public double HalfLife()
        {
            // Calculates half life
            double HalfLife = Math.Log(2) / B;
            return (HalfLife * -1);
        }


        public double RC()
        {
            // Calculates time constant
            double RC = 1 / B;
            return (RC * -1);
        }

        public virtual void Draw(DecayLine CurrentDecay, int DecayLines)
        {
            this.DecayLineNum = DecayLines;

            double xinc = (double)((maxBoundsX - minBoundsX) / (currentCanvas.Width / 5));

            for (double i = minBoundsX; i < maxBoundsX; i += xinc)
            {
                Line CurveLine = new Line();

                SolidColorBrush newBrush = new SolidColorBrush();
                newBrush.Color = Colors.Red;

                // Set the X positions
                double x1 = (i);
                double x2 = (i + xinc);

                // Set the Y positions based on the equation
                double y1 = A * Math.Exp(B * x1);
                double y2 = A * Math.Exp(B * x2);

                // Transform Coords
                CurveLine.X1 = TransformXCoord(x1);
                CurveLine.Y1 = TransformYCoord(y1);

                CurveLine.X2 = TransformXCoord(x2);

                CurveLine.Y2 = TransformYCoord(y2);

                CurveLine.Stroke = GetStroke();
                CurveLine.StrokeThickness = LineWeight;

                currentCanvas.Children.Add(CurveLine);

            }

            // Draw the correct current decay line
            switch (CurrentDecay)
            {
                case DecayLine.None:
                    break;
                case DecayLine.Halflife:
                    DrawDecayLines(HalfLife());
                    break;
                case DecayLine.Capacitor:
                    DrawDecayLines(RC());
                    break;
            }
        }

        private void DrawDecayLines(double DecayConstant)
        {

            double currentX = 0;
            for (int i = 1; i <= DecayLineNum; i++)
            {
                currentX = DecayConstant * i;

                Line LineY = new Line()
                {
                    //Set 1st coord on the curve
                    X1 = TransformXCoord(currentX),
                    Y1 = TransformYCoord(A * Math.Exp(B * (currentX))),

                    // Set 2nd coord at the bottom of the chart
                    X2 = TransformXCoord(currentX),
                    Y2 = TransformYCoord(0),

                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 1.8,
                    Opacity = 1,
                    StrokeDashArray = new DoubleCollection() { 2 }
                };

                Line LineX = new Line()
                {
                    //Set 1st coord on the curve
                    X1 = TransformXCoord(currentX),
                    Y1 = TransformYCoord(A * Math.Exp(B * (currentX))),

                    // Set 2nd coord at the side of the chart
                    X2 = 0,
                    Y2 = TransformYCoord(A * Math.Exp(B * (currentX))),

                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 1.8,
                    Opacity = 1,
                    StrokeDashArray = new DoubleCollection() { 2 }
                };


                currentCanvas.Children.Add(LineY);
                currentCanvas.Children.Add(LineX);

            }
        }



    }
}
