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
using DataFlow;

namespace DataFlow.GridProperties
{
    public partial class GridDialogBox : Window
    {

        public int MinorIntervalsOnX;
        public int MinorIntervalsOnY;

        public int MajorIntervalsOnX;
        public int MajorIntervalsOnY;

        public int DomainMin;
        public int DomainMax;

        public int RangeMin;
        public int RangeMax;


        public GridDialogBox()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            MinorIntervalsOnX = int.Parse(MinorIntervalsOnXBox.Text);
            MinorIntervalsOnY = int.Parse(MinorIntervalsOnYBox.Text);

            MajorIntervalsOnX = int.Parse(MajorIntervalsOnXBox.Text);
            MajorIntervalsOnY = int.Parse(MajorIntervalsOnYBox.Text);

            DomainMin = int.Parse(DomainMinBox.Text);
            DomainMax = int.Parse(DomainMaxBox.Text);

            RangeMax = int.Parse(RangeMaxBox.Text);
            RangeMin = int.Parse(RangeMinBox.Text);

            this.DialogResult = true;
        }


        private void MinorIntervalsOnXBox_Loaded(object sender, RoutedEventArgs e)
        {
            MinorIntervalsOnXBox.Text = Convert.ToString(MinorIntervalsOnX);
            MinorIntervalsOnYBox.Text = Convert.ToString(MinorIntervalsOnY);

            MajorIntervalsOnXBox.Text = Convert.ToString(MajorIntervalsOnX);
            MajorIntervalsOnYBox.Text = Convert.ToString(MajorIntervalsOnY);
        }

        private void DomainMinBox_Loaded(object sender, RoutedEventArgs e)
        {
            DomainMinBox.Text = Convert.ToString(DomainMin);
            DomainMaxBox.Text = Convert.ToString(DomainMax);

            RangeMinBox.Text = Convert.ToString(RangeMin);
            RangeMaxBox.Text = Convert.ToString(RangeMax);
        }
    }


}
