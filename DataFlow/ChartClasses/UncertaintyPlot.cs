using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataFlow.ChartClasses
{
    class Uncertainty : ChartElement
    {
        private double xUncertaintyPlus;
        private double yUncertaintyPlus;
        private double xUncertaintyMinus;
        private double yUncertaintyMinus;

        private CoordPoint coordinate;
        //Single Uncertainty
        public Uncertainty(CoordPoint coordinate, double xUncertainty, double yUncertainty, bool Percentage, Canvas currentCanvas, ChartBounds currentBounds)
        {
            this.coordinate = coordinate;
            this.currentCanvas = currentCanvas;
            this.minBoundsX = currentBounds.MinBoundsX;
            this.minBoundsY = currentBounds.MinBoundsY;
            this.maxBoundsX = currentBounds.MaxBoundsX;
            this.maxBoundsY = currentBounds.MaxBoundsY;

            if (Percentage)
            {
                this.xUncertaintyPlus = coordinate.X *(xUncertainty / 100);
                this.xUncertaintyMinus = coordinate.X * (xUncertainty / 100);
                this.yUncertaintyPlus = coordinate.Y * (yUncertainty / 100);
                this.yUncertaintyMinus = coordinate.Y * (yUncertainty / 100);
            }
            else
            {
                this.xUncertaintyPlus = xUncertainty;
                this.xUncertaintyMinus = xUncertainty;
                this.yUncertaintyPlus = yUncertainty;
                this.yUncertaintyMinus = yUncertainty;
            }



            PlotXLineMinus();
            PlotYLineMinus();
            PlotYLinePlus();
            PlotXLinePlus();
        }

        //Indiv Uncertainty
        public Uncertainty(CoordPoint coordinate, IndivUncValues indivUncValues, Canvas currentCanvas, ChartBounds currentBounds)
        {
            this.coordinate = coordinate;
            this.xUncertaintyPlus = indivUncValues.XPlus;
            this.xUncertaintyMinus = indivUncValues.XMinus;
            this.yUncertaintyPlus = indivUncValues.YPlus;
            this.yUncertaintyMinus = indivUncValues.YMinus;
            this.currentCanvas = currentCanvas;
            this.minBoundsX = currentBounds.MinBoundsX;
            this.minBoundsY = currentBounds.MinBoundsY;
            this.maxBoundsX = currentBounds.MaxBoundsX;
            this.maxBoundsY = currentBounds.MaxBoundsY;

            PlotXLineMinus();
            PlotYLineMinus();
            PlotYLinePlus();
            PlotXLinePlus();

        }


        private void PlotXLinePlus()
        {

            Line LineX = new Line
            {
                X1 = TransformXCoord((coordinate.X + xUncertaintyPlus)),
                Y1 = TransformYCoord(coordinate.Y) ,

                X2 = TransformXCoord(coordinate.X),
                Y2 = TransformYCoord(coordinate.Y),

                Stroke = new SolidColorBrush { Color = Colors.Red },

            };

            currentCanvas.Children.Add(LineX);

        }

        private void PlotXLineMinus()
        {

            Line LineX = new Line
            {
                X1 = TransformXCoord(coordinate.X - xUncertaintyMinus),
                Y1 = TransformYCoord(coordinate.Y),

                X2 = TransformXCoord((coordinate.X)),
                Y2 = TransformYCoord(coordinate.Y),

                Stroke = new SolidColorBrush { Color = Colors.Red }

            };

            currentCanvas.Children.Add(LineX);

        }

        private void PlotYLinePlus()
        {
            Line LineY = new Line
            {
                Y1 = TransformYCoord(coordinate.Y),
                X1 = TransformXCoord(coordinate.X),

                Y2 = TransformYCoord(coordinate.Y + yUncertaintyPlus) ,
                X2 = TransformXCoord(coordinate.X),

                Stroke = new SolidColorBrush { Color = Colors.Red }

            };

            currentCanvas.Children.Add(LineY);
        }

        private void PlotYLineMinus()
        {
            Line LineY = new Line
            {
                Y1 = TransformYCoord(coordinate.Y - yUncertaintyMinus),
                X1 = TransformXCoord(coordinate.X),

                Y2 = TransformYCoord(coordinate.Y),
                X2 = TransformXCoord(coordinate.X),

                Stroke = new SolidColorBrush { Color = Colors.Red }

            };

            currentCanvas.Children.Add(LineY);
        }

    }
}
