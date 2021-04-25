using System.Windows;
using System.Windows.Media;

namespace SolarPanels.Models
{
    /// <summary>
    /// Single line from two points.
    /// </summary>
    public struct LineSegment
    {
        public FloatPoint Point1 { get; set; }
        public FloatPoint Point2 { get; set; }
        public SolidColorBrush Stroke { get; set; }

        public override string ToString()
        {
            return $"{Point1} - {Point2}";
        }
    }
}
