using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdvancedScada.WPF.HMIControls
{
    public enum ShapesE
    {
        Chevron, Diamond, Hexagon, Label, RoundedSidesRectangle, SpeechBubble, Triangle, Rectangle
    }
    public class HMIShapes : Shape
    {
        public HMIShapes()
        {
            this.Stretch = Stretch.Fill;
            this.StrokeLineJoin = PenLineJoin.Round;

        }
        public enum Orientation
        {
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }
        [Category("HMI")]
        public Orientation TriangleOrientation
        {
            get { return (Orientation)GetValue(TriangleOrientationProperty); }
            set { SetValue(TriangleOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TriangleOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TriangleOrientationProperty =
            DependencyProperty.Register("TriangleOrientation", typeof(Orientation), typeof(HMIShapes), new UIPropertyMetadata(Orientation.N, OnOrientationChanged));

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs ek)
        {
            HMIShapes t = (HMIShapes)d;
            t.InvalidateVisual();
        }

        [Category("HMI")]
        public ShapesE ShapesMullt
        {
            get { return (ShapesE)GetValue(ShapesMulltProperty); }
            set { SetValue(ShapesMulltProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShapesMullt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShapesMulltProperty =
            DependencyProperty.Register("ShapesMullt", typeof(ShapesE), typeof(HMIShapes), new FrameworkPropertyMetadata(ShapesE.Chevron, FrameworkPropertyMetadataOptions.AffectsRender));

        protected override Geometry DefiningGeometry
        {
            get
            {
                switch (ShapesMullt)
                {
                    case ShapesE.Chevron:

                        return GetGeometryChevron();
                    case ShapesE.Diamond:
                        return GetGeometryDiamond();

                    case ShapesE.Hexagon: return GetGeometryHexagon();
                    case ShapesE.Label:
                        break;
                    case ShapesE.RoundedSidesRectangle: return GetGeometryRoundedSidesRectangle();

                    case ShapesE.SpeechBubble: return GetGeometrySpeechBubble();

                    case ShapesE.Triangle: return GetGeometryTriangle();
                    case ShapesE.Rectangle:

                        break;
                    default:
                        break;
                }
                return GetGeometryChevron();
            }
        }


        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);
        //    double width = this.ActualWidth;
        //    double height = this.ActualHeight;
        //    double bevel = height * 0.1;
        //    switch (ShapesMullt)
        //    {
        //        case ShapesE.Chevron:
        //            // Create a rectangle and draw it in the DrawingContext.

        //            drawingContext.DrawGeometry(System.Windows.Media.Brushes.LightBlue, (System.Windows.Media.Pen)null, GetGeometryChevron());

        //            break;
        //        case ShapesE.Diamond:
        //            break;
        //        case ShapesE.Hexagon:
        //            break;
        //        case ShapesE.Label:
        //            break;
        //        case ShapesE.RoundedSidesRectangle:
        //            break;
        //        case ShapesE.SpeechBubble:
        //            break;
        //        case ShapesE.Triangle:
        //            break;
        //        default:
        //            break;
        //    }

        //}

        private Geometry GetGeometryChevron()
        {
            return Geometry.Parse("M 0, 40 l 20, -40 l 20, 40 h -10 l -10, -20 l -10, 20 Z");
        }
        private Geometry GetGeometryDiamond()
        {
            return Geometry.Parse("M 100, 0 l 100, 100 l -100, 100 l -100, -100 Z");
        }
        private Geometry GetGeometryHexagon()
        {
            double sideLength = 100;
            double x = Math.Sqrt(sideLength * sideLength / 2);
            return Geometry.Parse(String.Format("M {0},0 h {1} l {0},{0} l -{0},{0} h -{1} l -{0},-{0} Z", x, sideLength));
        }
        private Geometry GetGeometryRoundedSidesRectangle()
        {
            return Geometry.Parse("M 20,10 h 100 a 100,100,45,0,1,0,100 h -100 a 100,100,-45,0,1,0,-100 Z");
        }
        private Geometry GetGeometrySpeechBubble()
        {
            double cornerRadius = 10;
            double speechOffset = 30;
            double speechDepth = 20;
            double speechWidth = 20;
            double width = ActualWidth - StrokeThickness;
            double height = ActualHeight - StrokeThickness;
            var g = new StreamGeometry();
            using (var context = g.Open())
            {
                double x0 = StrokeThickness / 2;
                double x1 = x0 + cornerRadius;
                double x2 = width - cornerRadius - x0;
                double x3 = x2 + cornerRadius;

                double x4 = x0 + speechOffset;
                double x5 = x4 + speechWidth;

                double y0 = StrokeThickness / 2;
                double y1 = y0 + cornerRadius;
                double y2 = height - speechDepth - (cornerRadius * 2);
                double y3 = y2 + cornerRadius;
                double y4 = y3 + speechDepth;
                context.BeginFigure(new Point(x1, y0), true, true);
                context.LineTo(new Point(x2, y0), true, true);
                context.ArcTo(new Point(x3, y1), new Size(cornerRadius, cornerRadius), 90, false, SweepDirection.Clockwise, true, true);
                context.LineTo(new Point(x3, y2), true, true);
                context.ArcTo(new Point(x2, y3), new Size(cornerRadius, cornerRadius), 90, false, SweepDirection.Clockwise, true, true);
                context.LineTo(new Point(x5, y3), true, true);
                context.LineTo(new Point(x4, y4), true, true);
                context.LineTo(new Point(x4, y3), true, true);
                context.LineTo(new Point(x1, y3), true, true);
                context.ArcTo(new Point(x0, y2), new Size(cornerRadius, cornerRadius), 90, false, SweepDirection.Clockwise, true, true);
                context.LineTo(new Point(x0, y1), true, true);
                context.ArcTo(new Point(x1, y0), new Size(cornerRadius, cornerRadius), 90, false, SweepDirection.Clockwise, true, true);
            }
            g.Freeze();
            return g;
        }
        private Geometry GetGeometryTriangle()
        {
            string data;
            if (TriangleOrientation == Orientation.N)
                data = "M 0,1 l 1,1 h -2 Z";
            else if (TriangleOrientation == Orientation.NE)
                data = "M 0,0 h 1 v 1 Z";
            else if (TriangleOrientation == Orientation.E)
                data = "M 0,0 l 1,1 l -1,1 Z";
            else if (TriangleOrientation == Orientation.SE)
                data = "M 1,0 v 1 h -1 Z";
            else if (TriangleOrientation == Orientation.S)
                data = "M 0,0 h 2 l -1,1 Z";
            else if (TriangleOrientation == Orientation.SW)
                data = "M 0,0 v 1 h 1 Z";
            else if (TriangleOrientation == Orientation.W)
                data = "M 0,1 l 1,1 v -2 Z";
            else
                data = "M 0,0 h 1 l -1,1 Z";
            return Geometry.Parse(data);
        }
    }
}
