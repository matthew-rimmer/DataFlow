using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using DataFlow.ChartClasses;

namespace DataFlow
{
    class Chart
    {
        //Private Variables
        private Canvas currentCanvas;
        private Canvas currentBorderCanvas;

        public ObservableCollection<CoordPoint> coordinates = new ObservableCollection<CoordPoint>();

        //Public Variables
        public double MaxBoundsX { get; set; } = 100;
        public double MinBoundsX { get; set; } = 0;

        public double MaxBoundsY { get; set; } = 100;
        public double MinBoundsY { get; set; } = 0;

        public int MajorIntervalsX { get; set; } = 10;
        public int MajorIntervalsY { get; set; } = 10;

        public int MinorIntervalsX { get; set; } = 5;
        public int MinorIntervalsY { get; set; } = 5;

        public RegressionType CurrentLine;

        public Uncertainties.UncertaintyType CurrentUncertainty;

        public ChartClasses.Grid ChartGrid;

        public ExponentialFit ExpFit; 
        
        public StraightFit SFit;

        public ExponentialIncreaseFit ExpIncFit;

        public double SingleUncXValue { get; set; }
        public double SingleUncYValue { get; set; }

        public LineProperties.LineColour CurrentColour; 
        public int LineWeight = 2;

        public ChartClasses.Regression.DecayLine CurrentDecayLine;
        public int DecayLineNum = 5;

        public double ExpIncAsymtope;

        // Construcutor initialises canvases
        public Chart(Canvas currentCanvas, Canvas currentBorderCanvas)
        {
            this.currentCanvas = currentCanvas;
            this.currentBorderCanvas = currentBorderCanvas;
        }

        public void DrawChart()
        {
            // Draws each part of the graph 

            DrawGrid();            

            DrawUncertainties();

            DrawPlots();

            DrawRegression();
        }

        public void ClearChart()
        {
            // Clears the labels on the border canvas, then clears the main canvas
            ChartGrid.ClearLabels();
            currentCanvas.Children.Clear();
        }

        public void RedrawChart()
        {
            ClearChart();
            DrawChart();
        }


        private void DrawPlots()
        {
            // Draws all the plots in the ObservableCollection
            for (int i = 0; i < coordinates.Count; i++)
            {
                Plot plot = new Plot(currentCanvas, GetBounds(), coordinates[i].X, coordinates[i].Y);
                plot.Draw();
            }
        }

        private void DrawGrid()
        {
            // Instantiates + draws an new grid 
            GridIntervals currentIntervals = new GridIntervals(MajorIntervalsX, MinorIntervalsX, MajorIntervalsY, MinorIntervalsY);
            ChartGrid = new ChartClasses.Grid(GetBounds(), currentCanvas, currentBorderCanvas, currentIntervals);
            ChartGrid.Draw();
        }

        private void DrawRegression()
        {
            // Selects the current regression line based on the current line and draws it
            switch (CurrentLine)
            {
                case RegressionType.None:
                    break;
                case RegressionType.Straight:
                    DrawStraightRegression();
                    break;
                case RegressionType.Exp:
                    DrawExpRegression();
                    break;
                case RegressionType.ExpInc:
                    DrawExpIncRegression(ExpIncAsymtope);
                    break;
            }
        }

        private void DrawStraightRegression()
        {
            // Reinstantiates a new line
            SFit = new StraightFit(coordinates, GetBounds(), currentCanvas);


            SFit.CurrentColour = CurrentColour;
            SFit.LineWeight = LineWeight;

            SFit.Draw();

            // Sets the current regression type to Straight
            CurrentLine = RegressionType.Straight;
        }

        private void DrawExpRegression()
        {
            // Reinstantiates a new curve
            ExpFit = new ExponentialFit(coordinates, currentCanvas, GetBounds());

            ExpFit.CurrentColour = CurrentColour;
            ExpFit.LineWeight = LineWeight;

            ExpFit.Draw(CurrentDecayLine, DecayLineNum);

            // Sets the current regression type to Exponential
            CurrentLine = RegressionType.Exp;
        }

        private void DrawExpIncRegression(double C)
        {
            // Reinstantiates a new curve
            ExpIncFit = new ExponentialIncreaseFit(coordinates, currentCanvas, GetBounds(), C);

            ExpIncFit.CurrentColour = CurrentColour;
            ExpIncFit.LineWeight = LineWeight;

            ExpIncFit.Draw(CurrentDecayLine, DecayLineNum);

            // Sets the current regression type to Exponential Increase
            CurrentLine = RegressionType.ExpInc;
        }

        private void DrawUncertainties()
        {
            // Selects the current uncertainty type and draws it for each 
            // Each type does it's respective drawing for each value in the ObservableCollection
            switch (CurrentUncertainty)
            {
                case Uncertainties.UncertaintyType.Indiv:
                    // Draws the uncertainty for all the coordinates
                    for (int i = 0; i < coordinates.Count; i++)
                    {
                        IndivUncValues currentUncValues = new IndivUncValues(coordinates[i].XPlus, coordinates[i].XMinus, coordinates[i].YPlus, coordinates[i].YMinus);
                        Uncertainty Unc = new Uncertainty(coordinates[i], currentUncValues, currentCanvas, GetBounds());
                    }
                    break;
                case Uncertainties.UncertaintyType.SingleAbs:
                    // Draws the uncertainty for all the coordinates
                    for (int i = 0; i < coordinates.Count; i++)
                    {
                        Uncertainty Unc = new Uncertainty(coordinates[i], SingleUncXValue, SingleUncYValue, false, currentCanvas, GetBounds());
                    }
                    break; 
                case Uncertainties.UncertaintyType.SinglePerc:
                    // Draws the uncertainty for all the coordinates
                    for (int i = 0; i < coordinates.Count; i++)
                    {
                        Uncertainty Unc = new Uncertainty(coordinates[i], SingleUncXValue, SingleUncYValue, true, currentCanvas, GetBounds());
                    }
                    break;             
            }

            
        }

        private ChartBounds GetBounds()
        {
            // Returns the current bounds of the chart
            return new ChartBounds(MinBoundsX, MaxBoundsX, MinBoundsY, MaxBoundsY);
        }

        public enum RegressionType
        {
            None, Straight, Exp, ExpInc
        };

    }


}
