using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;

namespace Test
{
    class Grid
    {
        public double totalLinesX { get; set; } = 10;
        public double totalLinesY { get; set; } = 10;

        public int minorLinesX { get; set; } = 5;
        public int minorLinesY { get; set; } = 5;

        private Canvas currentCanvas;
        private Canvas currentBorderCanvas;

        private double maxBoundsX;
        private double minBoundsX;

        private double maxBoundsY;
        private double minBoundsY;

        private Line[] gridLinesVert = new Line[1000];

        private Line[] gridLinesHoriz = new Line[1000];

        private Label[] gridLabelVert = new Label[1000];

        private Label[] gridLabelHoriz = new Label[1000];

        public Grid(Canvas currentCanvas, Canvas currentBorderCanvas, double maxBoundsX, double minBoundsX, double maxBoundsY, double minBoundsY)
        {
            this.currentCanvas = currentCanvas;
            this.currentBorderCanvas = currentBorderCanvas;
            this.maxBoundsX = maxBoundsX;
            this.minBoundsX = minBoundsX;
            this.maxBoundsY = maxBoundsY;
            this.minBoundsY = minBoundsY;

            Draw();
        }

        public void Draw()
        {
            double lineX = 0;
            double LabelintervalY = ((double)maxBoundsY - minBoundsY) / totalLinesY;
            double LabelintervalX = ((double)maxBoundsX - minBoundsX) / totalLinesX;
            int LineCounter = 0;

            // Create a Black Brush
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            SolidColorBrush grayBrush = new SolidColorBrush();
            grayBrush.Color = Colors.Gray;

            SolidColorBrush lightGrayBrush = new SolidColorBrush();
            lightGrayBrush.Color = Colors.Black;
            int OriginLineVert = 0;

            bool OriginLineVertThere = false;

            // Creating Vertical Grid lines
            for (int i = 0; lineX <= (currentCanvas.Width); ++i)
            {

                gridLinesVert[i] = new Line();
                gridLinesVert[i].X1 = lineX;
                gridLinesVert[i].Y1 = 0;
                gridLinesVert[i].X2 = lineX;
                gridLinesVert[i].Y2 = currentCanvas.Height;



                // Set Line's width, color
                gridLinesVert[i].Stroke = blackBrush;
                bool OriginLine = false;


                if (minBoundsX + (LabelintervalX * ((double)i / minorLinesX)) == 0.0)
                {
                    gridLinesVert[i].StrokeThickness = 2;
                    gridLinesVert[i].Stroke = blackBrush;
                    OriginLineVert = i;
                    OriginLineVertThere = true;
                    OriginLine = true;
                }

                if ((LineCounter % minorLinesX == 0) && OriginLine == false)
                {
                    gridLinesVert[i].Stroke = lightGrayBrush;
                    currentCanvas.Children.Add(gridLinesVert[i]);
                }
                else if (OriginLine == false)
                {
                    gridLinesVert[i].StrokeThickness = 1;
                    gridLinesVert[i].Stroke = grayBrush;
                    currentCanvas.Children.Add(gridLinesVert[i]);
                }




                // Add line to the Grid.
                lineX += ((currentCanvas.Width) / totalLinesX) / minorLinesX;
                LineCounter++;
            }

            double lineY = 0;

            LineCounter = 0;

            int OriginLineHoriz = 0;
            bool OriginLineHorizThere = false;

            // Creating Horizontal Grid lines
            for (int i = 0; lineY <= currentCanvas.Height; ++i)
            {
                gridLinesHoriz[i] = new Line();
                gridLinesHoriz[i].X1 = 0;
                gridLinesHoriz[i].Y1 = lineY;
                gridLinesHoriz[i].X2 = currentCanvas.Width;
                gridLinesHoriz[i].Y2 = lineY;

                // Set Line's width, color
                gridLinesHoriz[i].Stroke = blackBrush;

                bool OriginLine = false;

                if (minBoundsY + (LabelintervalY * ((double)i / minorLinesY)) == 0)
                {
                    gridLinesHoriz[i].StrokeThickness = 2;
                    gridLinesHoriz[i].Stroke = blackBrush;
                    OriginLineHoriz = i;
                    OriginLineHorizThere = true;
                    OriginLine = true;
                }

                if ((LineCounter % minorLinesY == 0) && OriginLine == false)
                {
                    gridLinesHoriz[i].Stroke = lightGrayBrush;
                    currentCanvas.Children.Add(gridLinesHoriz[i]);
                }
                else if (OriginLine == false)
                {
                    gridLinesHoriz[i].StrokeThickness = 1;
                    gridLinesHoriz[i].Stroke = grayBrush;
                    currentCanvas.Children.Add(gridLinesHoriz[i]);
                }



                // Add line to the Grid.

                lineY += ((currentCanvas.Height) / totalLinesY) / minorLinesY;
                LineCounter++;
            }

            if (OriginLineHorizThere)
            {
                currentCanvas.Children.Add(gridLinesHoriz[OriginLineHoriz]);
            }

            if (OriginLineVertThere)
            {
                currentCanvas.Children.Add(gridLinesVert[OriginLineVert]);
            }




            // Add Border lines

            Rectangle BorderRectangle = new Rectangle()
            {
                Height = currentCanvas.Height,
                Width = currentCanvas.Width,
                Stroke = blackBrush,
                StrokeThickness = 1
            };


            // Add lines to the Grid
            currentCanvas.Children.Add(BorderRectangle);


            double interval = (currentCanvas.Width) / totalLinesX;

            double Currentinterval = -10;

            ScaleTransform flipTrans = new ScaleTransform();

            flipTrans.ScaleY = -1;



            // Create Vertical Labels 
            for (int i = 0; Currentinterval < (currentCanvas.Width); i++)
            {

                gridLabelVert[i] = new Label();

                gridLabelVert[i].FontSize = 16;

                gridLabelVert[i].Content = minBoundsX + (LabelintervalX * i);


                gridLabelVert[i].RenderTransformOrigin = new Point(0.5, 0.5);

                gridLabelVert[i].Margin = new Thickness(Currentinterval, -35, 0, 0);


                gridLabelVert[i].RenderTransform = flipTrans;


                currentBorderCanvas.Children.Add(gridLabelVert[i]);

                Currentinterval += interval;

            }

            interval = (currentCanvas.Height) / totalLinesY;

            Currentinterval = -15;



            // Create Horizontal Labels 
            for (int i = 0; Currentinterval < (currentCanvas.Height); i++)
            {

                gridLabelHoriz[i] = new Label();

                gridLabelHoriz[i].FontSize = 16;

                gridLabelHoriz[i].Content = minBoundsY + (LabelintervalY) * i;

                gridLabelHoriz[i].RenderTransformOrigin = new Point(0.5, 0.5);

                gridLabelHoriz[i].Margin = new Thickness(-40, Currentinterval, 0, 0);


                gridLabelHoriz[i].RenderTransform = flipTrans;


                currentBorderCanvas.Children.Add(gridLabelHoriz[i]);

                Currentinterval += interval;

            }

        }

        public void Clear()
        {
            for (int i = 0; i < 12; i++)
            {
                currentBorderCanvas.Children.Remove(gridLabelHoriz[i]);
            }

            for (int i = 0; i < 12; i++)
            {
                currentBorderCanvas.Children.Remove(gridLabelVert[i]);
            }

            currentCanvas.Children.Clear();
        }
    }
}
