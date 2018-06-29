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
    class Regression : ChartElement
    {
        public LineProperties.LineColour CurrentColour = LineProperties.LineColour.Red;
        public int LineWeight { get; set; } = 2;

        //Lists for X and Y values
        public List<double> XValues = new List<double>();
        public List<double> YValues = new List<double>();

        public ObservableCollection<CoordPoint> coordinates = new ObservableCollection<CoordPoint>();

        //Returns the current colour
        protected SolidColorBrush GetStroke()
        {

            SolidColorBrush NewBrush = new SolidColorBrush();

            switch (CurrentColour)
            {
                case LineProperties.LineColour.Black:
                    NewBrush.Color = Colors.Black;
                    break;
                case LineProperties.LineColour.Red:
                    NewBrush.Color = Colors.Red;
                    break;
                case LineProperties.LineColour.Blue:
                    NewBrush.Color = Colors.Blue;
                    break;
            }

            return NewBrush;

        }

        // SumOf if there's 1 set of values
        // The bool in front of the list indicates if natural logs will be taken of it
        protected double SumOf(bool ln1, List<double> listIn1)
        {
            double total = 0; 

            for (int i = 0; i < listIn1.Count; i++)
            {
                if (ln1)
                {
                    total = total + Math.Log(listIn1[i]);
                }
                else 
                {
                    total = total + listIn1[i];
                }
                
            }

            return total;

        }

        // SumOf if there's 2 sets of values
        // The bool in front of each list indicates if natural logs will be taken of it
        protected double SumOf(bool ln1, List<double> listIn1, bool ln2, List<double> listIn2)
        {
            double total = 0;


            for (int i = 0; i < listIn1.Count; i++)
            {
                double temp1 = 0;
                double temp2 = 0;

                if (ln1)
                {
                    temp1 = Math.Log(listIn1[i]);
                }
                else
                {
                    temp1 = listIn1[i];
                }

                if (ln2)
                {
                    temp2 = Math.Log(listIn2[i]);
                }
                else
                {
                    temp2 = listIn2[i];
                }

                total = total + (temp1 * temp2);
            }

            return total;
        }

        // SumOf if there's 3 sets of values
        // The bool in front of each list indicates if natural logs will be taken of it
        protected double SumOf(bool ln1, List<double> listIn1, bool ln2, List<double> listIn2, bool ln3, List<double> listIn3)
        {
            double total = 0;


            for (int i = 0; i < listIn1.Count; i++)
            {
                double temp1 = 0;
                double temp2 = 0;
                double temp3 = 0;

                if (ln1)
                {
                    temp1 = Math.Log(listIn1[i]);
                }
                else
                {
                    temp1 = listIn1[i];
                }

                if (ln2)
                {
                    temp2 = Math.Log(listIn2[i]);
                }
                else
                {
                    temp2 = listIn2[i];
                }

                if (ln3)
                {
                    temp3 = Math.Log(listIn3[i]);
                }
                else
                {
                    temp3 = listIn3[i];
                }

                total = total + (temp1 * temp2 * temp3);
            }

            return total; 
        }

        public enum DecayLine
        {
            None, Halflife, Capacitor
        }
    }
}
