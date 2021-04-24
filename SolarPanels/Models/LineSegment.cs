using System.Windows;
using System.Windows.Media;

namespace SolarPanels.Models
{
    public struct LineSegment
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public SolidColorBrush Stroke { get; set; }
    }
}
