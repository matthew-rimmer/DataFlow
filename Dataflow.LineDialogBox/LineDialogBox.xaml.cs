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

namespace DataFlow.LineProperties
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LineDialogBox : Window
    {
        public LineColour CurrentColour;
        public int LineWeight;


        public LineDialogBox()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Black_Selected(object sender, RoutedEventArgs e) => CurrentColour = LineColour.Black;
        private void Red_Selected(object sender, RoutedEventArgs e) => CurrentColour = LineColour.Red;
        private void Blue_Selected(object sender, RoutedEventArgs e) => CurrentColour = LineColour.Blue;


        private void one_Selected(object sender, RoutedEventArgs e) => LineWeight = 1;
        private void two_Selected(object sender, RoutedEventArgs e) => LineWeight = 2;
        private void three_Selected(object sender, RoutedEventArgs e) => LineWeight = 3;
        private void four_Selected(object sender, RoutedEventArgs e) => LineWeight = 4;

        private void WeightComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            switch (CurrentColour)
            {
                case LineColour.Red:
                    ColourComboBox.SelectedItem = "Red";
                    break;
                case LineColour.Black:
                    ColourComboBox.SelectedItem = "Black";
                    break;
                case LineColour.Blue:
                    ColourComboBox.SelectedItem = "Blue ";
                    break;
                default:
                    break;
            }
        }
    }

    public enum LineColour
    {
        Red, Black, Blue
    }
}
