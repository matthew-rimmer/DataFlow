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

namespace DataFlow.Uncertainties
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UncertaintyDialogBox : Window
    {
        
        public UncertaintyType CurrentUncertainty;

        public UncertaintyDialogBox()
        {
            InitializeComponent();
        }

        private void SingleAbsolute_Radio_Checked(object sender, RoutedEventArgs e)
        {
            CurrentUncertainty = UncertaintyType.SingleAbs;
        }

        private void SinglePerc_Radio_Checked(object sender, RoutedEventArgs e)
        {
            CurrentUncertainty = UncertaintyType.SinglePerc;
        }

        private void Individ_Radio_Checked(object sender, RoutedEventArgs e)
        {
            CurrentUncertainty = UncertaintyType.Indiv;
        }

        private void None_Radio_Checked(object sender, RoutedEventArgs e)
        {
            CurrentUncertainty = UncertaintyType.None;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void None_Radio_Loaded(object sender, RoutedEventArgs e)
        {
            switch (CurrentUncertainty)
            {
                case UncertaintyType.None:
                    None_Radio.IsChecked = true;
                    break;
                case UncertaintyType.SingleAbs:
                    SingleAbsolute_Radio.IsChecked = true;
                    break;
                case UncertaintyType.SinglePerc:
                    SinglePerc_Radio.IsChecked = true;
                    break;
                case UncertaintyType.Indiv:
                    Individ_Radio.IsChecked = true;
                    break;
            }
        }
    }

    public enum UncertaintyType
    {
        None, SingleAbs, SinglePerc, Indiv
    };
}

