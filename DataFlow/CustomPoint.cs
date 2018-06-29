using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.IO;
using DataFlow.ChartClasses;

namespace DataFlow
{
    // This class acts as a data class for storing the X and Y values and the uncertainties
    public class CoordPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double XPlus { get; set; }
        public double XMinus { get; set; }

        public double YPlus { get; set; }
        public double YMinus { get; set; }

    }
}
