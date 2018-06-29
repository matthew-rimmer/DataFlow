using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFlow
{
    class IndivUncValues
    {
        public double XPlus { get; }
        public double XMinus { get; }
        public double YPlus { get; }
        public double YMinus { get; }

        public IndivUncValues(double XPlus, double XMinus, double YPlus, double YMinus)
        {
            this.XPlus = XPlus;
            this.XMinus = XMinus;
            this.YPlus = YPlus;
            this.YMinus = YMinus;
        }
    }
}
