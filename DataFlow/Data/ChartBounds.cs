using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFlow
{
    class ChartBounds
    {
        public double MinBoundsX { get; }
        public double MaxBoundsX { get; }
        public double MinBoundsY { get; }
        public double MaxBoundsY { get; }

        public ChartBounds(double MinBoundsX, double MaxBoundsX, double MinBoundsY, double MaxBoundsY)
        {
            this.MinBoundsX = MinBoundsX;
            this.MaxBoundsX = MaxBoundsX;
            this.MinBoundsY = MinBoundsY;
            this.MaxBoundsY = MaxBoundsY;
        }

    }
}
