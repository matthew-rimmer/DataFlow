using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace DataFlow.ChartClasses
{
    class Plot : ChartElement
    {
        double X;
        double Y;

        public Plot(Canvas currentCanvas, ChartBounds currentBounds,double X, double Y)
        {
            this.X = X;
            this.Y = Y;
            this.currentCanvas = currentCanvas;
            this.maxBoundsX = currentBounds.MaxBoundsX;
            this.minBoundsX = currentBounds.MinBoundsX;
            this.maxBoundsY = currentBounds.MaxBoundsY;
            this.minBoundsY = currentBounds.MinBoundsY;
        }

        public void Draw()
        {
            Line PlotLine1 = new Line();
            Line PlotLine2 = new Line();

            PlotLine1.Stroke = new SolidColorBrush(Colors.Black);
            PlotLine2.Stroke = new SolidColorBrush(Colors.Black);

            double XCordOnChart = TransformXCoord(X);

            double YCordOnChart = TransformYCoord(Y);

            // Sets the 1st line diagnoally slanting right
            PlotLine1.X1 = XCordOnChart + 5;
            PlotLine1.Y1 = YCordOnChart + 5;

            PlotLine1.X2 = XCordOnChart - 5;
            PlotLine1.Y2 = YCordOnChart - 5;

            // Sets the 2nd line diagnoally slanting left
            PlotLine2.X1 = XCordOnChart - 5;
            PlotLine2.Y1 = YCordOnChart + 5;

            PlotLine2.X2 = XCordOnChart + 5;
            PlotLine2.Y2 = YCordOnChart - 5;

            // Adds the lines to the canvas
            currentCanvas.Children.Add(PlotLine1);
            currentCanvas.Children.Add(PlotLine2);
        }
    }
}
