using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;


namespace DataFlow.ChartClasses
{
    class ChartElement
    {
        protected Canvas currentCanvas;

        protected double maxBoundsX;
        protected double minBoundsX;

        protected double maxBoundsY;
        protected double minBoundsY;

        protected double MultiplierX()
        {
            return (currentCanvas.Width / (maxBoundsX - minBoundsX));
        }
        protected double MultiplierY()
        {
            return (currentCanvas.Height / (maxBoundsY - minBoundsY));
        }

        protected double TransformXCoord(double XCoord)
        {
            return (XCoord -(minBoundsX)) * MultiplierX();
        }

        protected double TransformYCoord(double YCoord)
        {
            return (YCoord - (minBoundsY)) * MultiplierY();
        }

    }
}
