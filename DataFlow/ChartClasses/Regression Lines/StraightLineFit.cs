using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace DataFlow.ChartClasses
{
    class StraightFit : Regression
    {

        //Values for the equation y = mx + c
        public double Gradient { get; set; }
        public double YIntercept  { get; set; }

        //Values for variance and covariance 
        private double Sxx;
        private double Sxy;

        //Values for means
        private double yBar;
        private double xBar;

        //Constructor generates the values for all the values in the class, including the equation
        public StraightFit(ObservableCollection<CoordPoint> coordinates, ChartBounds CurrentBounds, Canvas currentCanvas)
        {
            this.currentCanvas = currentCanvas;
            this.maxBoundsX = CurrentBounds.MaxBoundsX;
            this.minBoundsX = CurrentBounds.MinBoundsX;
            this.maxBoundsY = CurrentBounds.MaxBoundsY;
            this.minBoundsY = CurrentBounds.MinBoundsY;

            this.coordinates = coordinates;
            for (int i = 0; i < coordinates.Count; i++)
            {
                XValues.Add(coordinates[i].X);
            }

            for (int i = 0; i < coordinates.Count; i++)
            {
                YValues.Add(coordinates[i].Y);
            }

            xBar = Mean(XValues);
            yBar = Mean(YValues);

            Sxy = Variance(XValues, YValues);

            Sxx = Variance(XValues, XValues);

            Gradient = Sxy / Sxx;

            YIntercept = yBar - (Gradient * xBar);

        }

        //Calculcates the Variance/Covariance of lists of co-ords
        private double Variance(List<double> list1, List<double> list2)
        {
            double listLength = list1.Count;
            double sumOf1 = list1.Sum();
            double sumOf2 = list2.Sum();
            double squaredNumTotal = 0;


            for (int i = 0; i < list1.Count; i++)
            {
                squaredNumTotal = squaredNumTotal + (list1[i] * list2[i]);
            }

            return squaredNumTotal - ((sumOf1 * sumOf2) / list1.Count);

        }

        //Calculates the mean
        private double Mean(List<double> ListIn)
        {
            double SumOfList = ListIn.Sum();

            return SumOfList / ListIn.Count;
        }

        public double GetYValue(double xValueIn)
        {
            double yValue = (Gradient * xValueIn) + YIntercept;


            return yValue;
        }

        public double GetXValue(double yValueIn)
        {
            double xValue = (yValueIn - YIntercept) / Gradient;


            return xValue;
        }

        public void Draw()
        {

            List<Double> CoordsX = new List<Double>();
            List<Double> CoordsY = new List<Double>();

            for (int i = 0; i < coordinates.Count; i++)
            {
                CoordsX.Add(coordinates[i].X);
            }

            for (int i = 0; i < coordinates.Count; i++)
            {
                CoordsY.Add(coordinates[i].Y);
            }

            Line RegressionLine = new Line()
            {
                X1 = TransformXCoord(minBoundsX),
                Y1 = TransformYCoord(GetYValue(minBoundsX)),

                X2 = TransformXCoord(maxBoundsX),

                Y2 = TransformYCoord(GetYValue(maxBoundsX)),

                Stroke = GetStroke(),

                StrokeThickness = LineWeight
            };

            currentCanvas.Children.Add(RegressionLine);
        }


    }
}
