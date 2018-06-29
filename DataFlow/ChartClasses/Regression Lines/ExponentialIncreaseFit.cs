using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DataFlow.ChartClasses
{
    class ExponentialIncreaseFit : ExponentialFit
    {
        public double C { get; set; }


        public ExponentialIncreaseFit(ObservableCollection<CoordPoint> coordinates, Canvas currentCanvas, ChartBounds CurrentBounds, double C)
        {
            this.currentCanvas = currentCanvas;
            this.maxBoundsX = CurrentBounds.MaxBoundsX;
            this.minBoundsX = CurrentBounds.MinBoundsX;
            this.maxBoundsY = CurrentBounds.MaxBoundsY;
            this.minBoundsY = CurrentBounds.MinBoundsY;
            this.coordinates = coordinates;
            this.C = C;

            // Puts the X and Y values into their on list so they can be calculated on
            for (int i = 0; i < coordinates.Count; i++)
            {
                XValues.Add(coordinates[i].X);
            }

            for (int i = 0; i < coordinates.Count; i++)
            {
                YValues.Add((((coordinates[i].Y / C) - 1) *-1));
            }

            // Sets the curve values
            A = GetA();

            B = GetB();

        }

        public override void Draw(DecayLine CurrentDecay, int DecayLineNum)
        {
            // Calculate the increment on the x for a smooth curve
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
                double y1 = ((A * Math.Exp(B * x1) * -1) + 1) *C;
                double y2 = ((A * Math.Exp(B * x2) * -1) + 1) *C;


                // Transform Coords
                CurveLine.X1 = TransformXCoord(x1);
                CurveLine.Y1 = TransformYCoord(y1);

                CurveLine.X2 = TransformXCoord(x2);
                CurveLine.Y2 = TransformYCoord(y2);

                CurveLine.Stroke = GetStroke();
                CurveLine.StrokeThickness = LineWeight;

                currentCanvas.Children.Add(CurveLine);

            }


            if (CurrentDecay == DecayLine.Capacitor)
            {
                double currentX = 0;

                // Calculate the interval constant
                double xInc = RC();
                for (int i = 0; i < DecayLineNum; i++)
                {
                    // Calculate the current X position
                    currentX = xInc * i;

                    Line LineY = new Line()
                    {
                        // Set 1st coord at the bottom of the chart
                        X1 = TransformXCoord(currentX),
                        Y1 = TransformYCoord(C * (1 - (A * Math.Exp(B * (0) )))) ,

                        // Set 2nd coord on the curve
                        X2 = TransformXCoord(currentX),
                        Y2 = TransformYCoord(C * (1 -(A * Math.Exp(B * (currentX))))),

                        Stroke = new SolidColorBrush(Colors.Blue),
                        StrokeThickness = 1.8,
                        Opacity = 1,
                        StrokeDashArray = new DoubleCollection() { 2 }
                    };

                    Line LineX = new Line()
                    {
                        // Set 1st coord on the curve
                        X1 = TransformXCoord(currentX),
                        Y1 = TransformYCoord(C *(1 - (A * Math.Exp(B * (currentX))))),

                        // Set 2nd coord to side of the chart
                        X2 = TransformXCoord(0),
                        Y2 = TransformYCoord(C *(1 - (A * Math.Exp(B * (currentX))))),

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
}
