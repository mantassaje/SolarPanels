using SolarPanels.Models.Interfaces;
using System.Collections.Generic;
using System.Windows;

namespace SolarPanels.Models
{
    public class Panel: IShape
    {
        public Point TopRightCorner { get; set; }
        public double Width { get; set; }
        public double Heigth { get; set; }
        public double Tilt { get; set; }

        //TODO include tilt
        public IEnumerable<LineSegment> GetLines()
        {
            var color = System.Windows.Media.Brushes.Blue;

            var line1 = new LineSegment()
            {
                Point1 = TopRightCorner,
                Point2 = new System.Windows.Point(TopRightCorner.X + Width, TopRightCorner.Y),
                Stroke = color
            };

            yield return line1;

            var line2 = new LineSegment()
            {
                Point1 = line1.Point2,
                Point2 = new System.Windows.Point(TopRightCorner.X + Width, TopRightCorner.Y + Heigth),
                Stroke = color
            };

            yield return line2;

            var line3 = new LineSegment()
            {
                Point1 = line2.Point2,
                Point2 = new System.Windows.Point(TopRightCorner.X, TopRightCorner.Y + Heigth),
                Stroke = color
            };

            yield return line3;

            yield return new LineSegment()
            {
                Point1 = line3.Point2,
                Point2 = TopRightCorner,
                Stroke = color
            };
        }
    }
}
