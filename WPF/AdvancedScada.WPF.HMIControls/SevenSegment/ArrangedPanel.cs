using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace AdvancedScada.WPF.HMIControls.SevenSegment
{
    /// <summary>
    /// A panel children of which fill maximum space
    /// </summary>
    [DesignTimeVisible(false)]
    internal class ArrangedPanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0;
            double y = 0;
            double w = finalSize.Width / InternalChildren.Count;
            double h = finalSize.Height;

            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(x, y), new Size(w, h)));
                x += w;
            }
            return finalSize;
        }
    }
}
