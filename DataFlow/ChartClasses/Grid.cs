using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;

namespace DataFlow.ChartClasses
{
    class Grid : ChartElement
    {
        public int MajorIntervalsX { get; set; } = 10;
        public int MajorIntervalsY { get; set; } = 10;

        public int MinorIntervalsX { get; set; } = 5;
        public int MinorIntervalsY { get; set; } = 5;

        private Canvas currentBorderCanvas;

        public Grid(ChartBounds currentBounds, Canvas currentCanvas, Canvas currentBorderCanvas, GridIntervals currentIntervals)
        {
            this.currentCanvas = currentCanvas;
            this.currentBorderCanvas = currentBorderCanvas;
            this.maxBoundsX = currentBounds.MaxBoundsX;
            this.minBoundsX = currentBounds.MinBoundsX;
            this.maxBoundsY = currentBounds.MaxBoundsY;
            this.minBoundsY = currentBounds.MinBoundsY;
            this.MajorIntervalsX = currentIntervals.MajorIntervalsX;
            this.MajorIntervalsY = currentIntervals.MajorIntervalsY;
            this.MinorIntervalsX = currentIntervals.MinorIntervalsX;
            this.MinorIntervalsY = currentIntervals.MinorIntervalsY;

        }

        public void Draw()
        {
            double lineX = 0;
            double LabelintervalY = ((double)maxBoundsY - minBoundsY) / MajorIntervalsY;
            double LabelintervalX = ((double)maxBoundsX - minBoundsX) / MajorIntervalsX;
            int LineCounter = 0;

            // Create a Black Brush
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            //Create a Grey Brush
            SolidColorBrush grayBrush = new SolidColorBrush();
            grayBrush.Color = Colors.Gray;

            int OriginLineVert = 0;

            bool OriginLineVertThere = false;

            List<Line> gridLinesX = new List<Line>();

            List<Line> gridLinesY = new List<Line>();

            // Creating Vertical Grid lines
            for (int i = 0; lineX <= (currentCanvas.Width); ++i)
            { 
                gridLinesX.Add(new Line
                {
                    X1 = lineX,
                    Y1 = 0,

                    X2 = lineX,
                    Y2 = currentCanvas.Height,

                    Stroke = blackBrush
                });

                bool OriginLine = false;

                // If the line is at the origin, make the liner thicker/black
                if (minBoundsX + (LabelintervalX * ((double)i / MinorIntervalsX)) == 0.0)
                {
                    gridLinesX[i].StrokeThickness = 2;
                    gridLinesX[i].Stroke = blackBrush;
                    OriginLineVert = i;
                    OriginLineVertThere = true;
                    OriginLine = true;
                }
 
                //If the line is major, make the line black
                if ((LineCounter % MinorIntervalsX == 0) && OriginLine == false)
                {
                    gridLinesX[i].Stroke = blackBrush;
                    // Add line to main canvas
                    currentCanvas.Children.Add(gridLinesX[i]);
                }
                // If the line is minor, make the line grey
                else if (OriginLine == false)
                {
                    gridLinesX[i].Stroke = grayBrush;
                    // Add line to main canvas
                    currentCanvas.Children.Add(gridLinesX[i]);
                }



                lineX += ((currentCanvas.Width) / MajorIntervalsX) / MinorIntervalsX;
                LineCounter++;
            }

            double lineY = 0;

            LineCounter = 0;

            int OriginLineHoriz = 0;
            bool OriginLineHorizThere = false;

            // Creating Horizontal Grid lines
            for (int i = 0; lineY <= currentCanvas.Height; ++i)
            {
                gridLinesY.Add(new Line
                {
                    X1 = 0,
                    Y1 = lineY,

                    X2 = currentCanvas.Width,
                    Y2 = lineY,

                    Stroke = blackBrush,
                }
                );


                bool OriginLine = false;

                // If the line is at the origin, make the liner thicker/black
                if (minBoundsY + (LabelintervalY * ((double)i / MinorIntervalsY)) == 0)
                {
                    gridLinesY[i].StrokeThickness = 2;
                    gridLinesY[i].Stroke = blackBrush;
                    OriginLineHoriz = i;
                    OriginLineHorizThere = true;
                    OriginLine = true;
                }

                //If the line is major, make the line black
                if ((LineCounter % MinorIntervalsY == 0) && OriginLine == false)
                {
                    gridLinesY[i].Stroke = blackBrush;
                    // Add line to main canvas
                    currentCanvas.Children.Add(gridLinesY[i]);
                }
                // If the line is minor, make the line grey
                else if (OriginLine == false)
                {
                    gridLinesY[i].Stroke = grayBrush;
                    // Add line to main canvas
                    currentCanvas.Children.Add(gridLinesY[i]);
                }



                lineY += ((currentCanvas.Height) / MajorIntervalsY) / MinorIntervalsY;
                LineCounter++;
            }


            // Draw Origin lines if found 

            if (OriginLineHorizThere)
            {
                currentCanvas.Children.Add(gridLinesY[OriginLineHoriz]);
            }

            if (OriginLineVertThere)
            {
                currentCanvas.Children.Add(gridLinesX[OriginLineVert]);
            }




            // Create Border 

            Rectangle BorderRectangle = new Rectangle()
            {
                Height = currentCanvas.Height,
                Width = currentCanvas.Width,
                Stroke = blackBrush,
                StrokeThickness = 1
            };


            // Add Border to the canvas
            currentCanvas.Children.Add(BorderRectangle);


            double interval = (currentCanvas.Width) / MajorIntervalsX;

            

            // Create a flip transformation so that the labels draw upright
            ScaleTransform flipTrans = new ScaleTransform();

            flipTrans.ScaleY = -1;

            // Create list labels
            List<Label> gridLabelsX = new List<Label>();

            List<Label> gridLabelsY = new List<Label>();


            // Starts the inveral at a negative to draw the labels in the correct position, as they are normally displaced incorrectly left
            double Currentinterval = -10;

            // Create Horizontal Labels 
            for (int i = 0; Currentinterval < (currentCanvas.Width); i++)
            {
                // Add/set the label
                gridLabelsX.Add(new Label
                {
                    FontSize = 16,
                    Content = Math.Round(minBoundsX + (LabelintervalX * i), 2),
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    Margin = new Thickness(Currentinterval, -35, 0, 0),
                    RenderTransform = flipTrans,
                    Uid = "XLabel"
                });


                // Add label to border canvas
                currentBorderCanvas.Children.Add(gridLabelsX[i]);

                // Increases the X value currently being drawn to
                Currentinterval += interval;

            }


            interval = (currentCanvas.Height) / MajorIntervalsY;


            // Starts the inveral at a negative to draw the labels in the correct position, as they are normally displaced incorrectly upwards
            Currentinterval = 0;

            // Create Vertical Labels 
            for (int i = 0; Currentinterval < (currentCanvas.Height); i++)
            {
                // Add/set the label
                gridLabelsY.Add(new Label
                {
                    FontSize = 16,
                    Content = Math.Round(minBoundsY + (LabelintervalY) * i,2),
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    Margin = new Thickness(-40, Currentinterval, 0, 0),
                    RenderTransform = flipTrans,
                    Uid = "YLabel"
                });

                // Add label to border canvas
                currentBorderCanvas.Children.Add(gridLabelsY[i]);

                // Increases the Y value currently being drawn to
                Currentinterval += interval;

            }

        }

        public void ClearLabels()
        {
            List<UIElement> itemstoremove = new List<UIElement>();

            // Adds each label in the border canvas to the list
            foreach (UIElement ui in currentBorderCanvas.Children)
            {
                if (ui.Uid == "XLabel" || ui.Uid == "YLabel")
                {
                    itemstoremove.Add(ui);
                }
            }

            // Removes all the labels/items in the list
            foreach (UIElement ui in itemstoremove)
            {
                currentBorderCanvas.Children.Remove(ui);
            }

        }
    }
}
