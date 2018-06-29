using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFlow
{
    class GridIntervals
    {
        public int MajorIntervalsX { get; } 
        public int MinorIntervalsX { get; } 

        public int MajorIntervalsY { get; }
        public int MinorIntervalsY { get; } 

        public GridIntervals(int MajorIntervalsX, int MinorIntervalsX, int MajorIntervalsY, int MinorIntervalsY)
        {
            this.MinorIntervalsX = MinorIntervalsX;
            this.MajorIntervalsX = MajorIntervalsX;
            this.MinorIntervalsY = MinorIntervalsY;
            this.MajorIntervalsY = MajorIntervalsY;
        }
    }
}
