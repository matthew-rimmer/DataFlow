using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using DataFlow.ChartClasses;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace DataFlow
{
    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        #region Declarations
        Chart MainChart;

        Regex PositiveRegex = new Regex(@"\d+.?\d+");
        Regex PostitiveNegativeRegex = new Regex(@"[+-]?\d+.?\d+");

        #endregion

        // MainWindow Constructor 
        public MainWindow()
        {
            InitializeComponent();

            MainChart = new Chart(ChartCanvas, BorderCanvas);

            // Sets the binding for the datagrid
            this.DataContext = MainChart.coordinates;
        }

        #region Annotations/Drawing on canvas

        // Called when the canvas is resized (and on startup)
        private void ChartCanvas_RedrawChart()
        {
            // Initialises all the objects for startup
            MainChart.DrawChart();

            // Resizes the canvas so that the border/chart canvas are the same size and
            // so that all the labels + textboxes fit onto the window
            ChartCanvas.Height = (ChartWindow.Height - 150);
            ChartCanvas.Width = (ChartWindow.Width - 250);

            BorderCanvas.Height = (ChartWindow.Height - 150);
            BorderCanvas.Width = (ChartWindow.Width - 250);

            MainChart.RedrawChart();

            // Parses the textbox for the value of C for the ExpInc curve
            try
            {
                switch (MainChart.CurrentLine)
                {
                    case Chart.RegressionType.ExpInc:
                        MainChart.ExpIncAsymtope = double.Parse(IncC_Textbox.Text);
                        break;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("There was an error drawing the line - there is no/an incorrect value for C");
                MainChart.CurrentLine = Chart.RegressionType.None;
                ChartCanvas_RedrawChart();
            }

        }


        private void DrawRegressionLine()
        {
            // Changes the current line to a straight regression line and adds the values of it to the labels in the window. 
            // If error, switch to no line
            try
            {
                MainChart.CurrentLine = Chart.RegressionType.Straight;

                MainChart.RedrawChart();

                GradientLabel.Content = Math.Round(MainChart.SFit.Gradient, 2);

                YInterceptLabel.Content = Math.Round(MainChart.SFit.YIntercept, 2);

                XInterceptLabel.Content = Math.Round(MainChart.SFit.GetXValue(0), 2);
            }
            catch (Exception)
            {
                MessageBox.Show("Error drawing Straight regression");
                MainChart.CurrentLine = Chart.RegressionType.None;
                ChartCanvas_RedrawChart();
            }



        }

        private void DrawExpCurve()
        {
            // Changes the current line to an exponential regression curve and adds the values of it to the labels in the window. 
            // If error, switch to no line
            try
            {
                MainChart.CurrentLine = Chart.RegressionType.Exp;

                MainChart.RedrawChart();

                ExpALabel.Content = Math.Round(MainChart.ExpFit.A, 2);

                ExpBLabel.Content = Math.Round(MainChart.ExpFit.B, 2);
            }
            catch (Exception)
            {
                MessageBox.Show("Error drawing Exp Curve");
                MainChart.CurrentLine = Chart.RegressionType.None;
                ChartCanvas_RedrawChart();
            }
        }

        private void DrawExpIncCurve()
        {
            // Changes the current line to an exponential increase regression curve and adds the values of it to the labels in the window.
            // If error, switch to no line
            try
            {
                MainChart.CurrentLine = Chart.RegressionType.ExpInc;
                MainChart.ExpIncAsymtope = double.Parse(IncC_Textbox.Text);

                MainChart.RedrawChart();
                ExpIncALabel.Content = Math.Round(MainChart.ExpIncFit.A, 2);

                ExpIncBLabel.Content = Math.Round(MainChart.ExpIncFit.B, 2);
            }
            catch (Exception)
            {
                MessageBox.Show("Error drawing Exp Increase Curve");
                MainChart.CurrentLine = Chart.RegressionType.None;
                ChartCanvas_RedrawChart();
            }
        }

        #endregion

        #region Event/Button Handlers

        // When the user finishes editing a cell, this redraws the canvas 
        private void XYDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ChartCanvas_RedrawChart();
        }

        private void RegressionButton_Click(object sender, RoutedEventArgs e)
        {
            DrawRegressionLine();
        }

        private void ExpRegressionButton_Click(object sender, RoutedEventArgs e)
        {
            DrawExpCurve();
        }

        private void ExpIncButton_Click(object sender, RoutedEventArgs e)
        {
            DrawExpIncCurve();
        }

        private void ChartWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChartCanvas_RedrawChart();
        }

        private void DomainClick(object sender, RoutedEventArgs e)
        {
            // Checks the textbox value against the regex 
            // If valid, pass to MainChart, if not, display error
            if (PostitiveNegativeRegex.IsMatch(DomainMinBox.Text))
            {
                MainChart.MinBoundsX = double.Parse(DomainMinBox.Text);
            }
            else
            {
                MessageBox.Show("Invalid Domain");
            }

            if (PostitiveNegativeRegex.IsMatch(DomainMaxBox.Text))
            {
                MainChart.MaxBoundsX = double.Parse(DomainMaxBox.Text);
            }
            else
            {
                MessageBox.Show("Invalid Domain");
            }

            ChartCanvas_RedrawChart();


        }

        private void RangeClick(object sender, RoutedEventArgs e)
        {
            // Checks the textbox value against the regex 
            // If valid, pass to MainChart, if not, display error
            if (PostitiveNegativeRegex.IsMatch(RangeMinBox.Text))
            {
                MainChart.MinBoundsY = double.Parse(RangeMinBox.Text);
            }
            else
            {
                MessageBox.Show("Invalid Range");
            }

            if (PostitiveNegativeRegex.IsMatch(RangeMaxBox.Text))
            {
                MainChart.MaxBoundsY = double.Parse(RangeMaxBox.Text);
            }
            else
            {
                MessageBox.Show("Invalid Range");
            }

            ChartCanvas_RedrawChart();


        }

        private void HalfLifeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CapacitorCheckBox.IsChecked = false;
            MainChart.CurrentDecayLine = ChartClasses.Regression.DecayLine.Halflife;
            ChartCanvas_RedrawChart();
        }

        private void HalfLifeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainChart.CurrentDecayLine = ChartClasses.Regression.DecayLine.None;
            ChartCanvas_RedrawChart();
        }

        private void CapacitorCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HalfLifeCheckBox.IsChecked = false;
            MainChart.CurrentDecayLine = ChartClasses.Regression.DecayLine.Capacitor;
            ChartCanvas_RedrawChart();
        }

        private void CapacitorCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainChart.CurrentDecayLine = ChartClasses.Regression.DecayLine.None;
            ChartCanvas_RedrawChart();
        }

        private void MenuExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "untitled"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG Image (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();


            // Process save file dialog box results
            if (result == true)
            {
                // Save image
                string fileName = dlg.FileName;

                XYDataGrid.Focus();

                Rect bounds = VisualTreeHelper.GetDescendantBounds(FullCanvas);
                double dpi = 96d;


                RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);


                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(FullCanvas);
                    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }

                rtb.Render(dv);
                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    pngEncoder.Save(ms);
                    ms.Close();

                    System.IO.File.WriteAllBytes(fileName, ms.ToArray());
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }



        }


        #endregion

        #region Dialogs

        private void ShowGridBox()
        {
            // Instantiate the dialog box
            GridProperties.GridDialogBox dlg = new GridProperties.GridDialogBox();

            // Pass values from chart to dialog
            dlg.MinorIntervalsOnX = MainChart.MinorIntervalsX;
            dlg.MinorIntervalsOnY = MainChart.MinorIntervalsY;

            dlg.MajorIntervalsOnX = MainChart.MajorIntervalsX;
            dlg.MajorIntervalsOnY = MainChart.MajorIntervalsY;

            dlg.DomainMax = (int)MainChart.MaxBoundsX;
            dlg.DomainMin = (int)MainChart.MinBoundsX;

            dlg.RangeMax = (int)MainChart.MaxBoundsY;
            dlg.RangeMin = (int)MainChart.MinBoundsY;

            // Configure the dialog box
            dlg.Owner = this;



            // Open the dialog box  
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {
                // Update values in chart
                MainChart.MajorIntervalsX = (dlg.MajorIntervalsOnX);
                MainChart.MajorIntervalsY = (dlg.MajorIntervalsOnY);

                MainChart.MinorIntervalsX = (dlg.MinorIntervalsOnX);
                MainChart.MinorIntervalsY = (dlg.MinorIntervalsOnY);

                MainChart.MaxBoundsX = dlg.DomainMax;
                MainChart.MinBoundsX = dlg.DomainMin;

                MainChart.MaxBoundsY = dlg.RangeMax;
                MainChart.MinBoundsY = dlg.RangeMin;


                ChartCanvas_RedrawChart();
            }
        }

        private void Properties_Click_Grid(object sender, RoutedEventArgs e)
        {
            ShowGridBox();
        }

        private void ShowLineBox()
        {
            // Instantiate the dialog box
            LineProperties.LineDialogBox dlg = new LineProperties.LineDialogBox();

            dlg.CurrentColour = MainChart.CurrentColour;
            dlg.LineWeight = MainChart.LineWeight;


            // Configure the dialog box
            dlg.Owner = this;



            // Open the dialog box  
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {
                // Update values in chart
                MainChart.CurrentColour = dlg.CurrentColour;
                MainChart.LineWeight = dlg.LineWeight;

                ChartCanvas_RedrawChart();
            }
        }

        private void Properties_Click_Line(object sender, RoutedEventArgs e)
        {
            ShowLineBox();
        }

        private void Properties_Click_Unc(object sender, RoutedEventArgs e)
        {
            // Instantiate the dialog box
            Uncertainties.UncertaintyDialogBox dlg = new Uncertainties.UncertaintyDialogBox();

            dlg.CurrentUncertainty = MainChart.CurrentUncertainty;


            // Configure the dialog box
            dlg.Owner = this;



            // Open the dialog box  
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {
                // Update values in chart
                // Switches the window

                MainChart.CurrentUncertainty = dlg.CurrentUncertainty;

                switch (MainChart.CurrentUncertainty)
                {
                    // Opens the window to the corresponding type, then hides the other windows
                    case Uncertainties.UncertaintyType.None:
                        UncertaintyDatagridWindow.Visibility = Visibility.Hidden;
                        UncertaintyIndivWindow.Visibility = Visibility.Hidden;
                        UncertaintyPercWindow.Visibility = Visibility.Hidden;
                        MainChart.SingleUncXValue = 0;
                        MainChart.SingleUncYValue = 0;
                        break;
                    case Uncertainties.UncertaintyType.SingleAbs:
                        UncertaintyDatagridWindow.Visibility = Visibility.Hidden;
                        UncertaintyIndivWindow.Visibility = Visibility.Visible;
                        UncertaintyPercWindow.Visibility = Visibility.Hidden;

                        MainChart.SingleUncXValue = 0;
                        MainChart.SingleUncYValue = 0;
                       
                        // Sets the uncertainty to the exsiting value if it's already in the box
                        if (PositiveRegex.IsMatch(UncXTextBox.Text))
                        {
                            MainChart.SingleUncXValue = double.Parse(UncXTextBox.Text);
                        }
                        if (PositiveRegex.IsMatch(UncYTextBox.Text))
                        {
                            MainChart.SingleUncYValue = double.Parse(UncYTextBox.Text);
                        }
                        break;
                    case Uncertainties.UncertaintyType.SinglePerc:
                        UncertaintyDatagridWindow.Visibility = Visibility.Hidden;
                        UncertaintyIndivWindow.Visibility = Visibility.Hidden;
                        UncertaintyPercWindow.Visibility = Visibility.Visible;

                        MainChart.SingleUncXValue = 0;
                        MainChart.SingleUncYValue = 0;

                        // Sets the uncertainty to the exsiting value if it's already in the box
                        if (PositiveRegex.IsMatch(UncPerXTextBox.Text))
                        {
                            MainChart.SingleUncXValue = double.Parse(UncPerXTextBox.Text);
                        }
                        if (PositiveRegex.IsMatch(UncPerYTextBox.Text))
                        {
                            MainChart.SingleUncYValue = double.Parse(UncPerYTextBox.Text);
                        }
                        break;
                    case Uncertainties.UncertaintyType.Indiv:
                        UncertaintyDatagridWindow.Visibility = Visibility.Visible;
                        UncertaintyIndivWindow.Visibility = Visibility.Hidden;
                        UncertaintyPercWindow.Visibility = Visibility.Hidden;
                        MainChart.SingleUncXValue = 0;
                        MainChart.SingleUncYValue = 0;
                        break;
                }
                ChartCanvas_RedrawChart();
            }
        }

        private void UncSingleButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositiveRegex.IsMatch(UncXTextBox.Text))
            {
                MainChart.SingleUncXValue = double.Parse(UncXTextBox.Text);
            }
            if (PositiveRegex.IsMatch(UncYTextBox.Text))
            {
                MainChart.SingleUncYValue = double.Parse(UncYTextBox.Text);
            }
            ChartCanvas_RedrawChart();
        }

        private void UncPercButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositiveRegex.IsMatch(UncPerXTextBox.Text))
            {
                MainChart.SingleUncXValue = double.Parse(UncPerXTextBox.Text);
            }
            if (PositiveRegex.IsMatch(UncPerYTextBox.Text))
            {
                MainChart.SingleUncYValue = double.Parse(UncPerYTextBox.Text);
            }
            ChartCanvas_RedrawChart();
        }

        private void DecayLinesClick(object sender, RoutedEventArgs e)
        {
            MainChart.DecayLineNum = int.Parse(DecayLinesTextbox.Text);
            ChartCanvas_RedrawChart();
        }

        private void DecayLinesClickInc(object sender, RoutedEventArgs e)
        {
            MainChart.DecayLineNum = int.Parse(DecayLinesTextboxInc.Text);
            ChartCanvas_RedrawChart();
        }





        #endregion

    }



}
