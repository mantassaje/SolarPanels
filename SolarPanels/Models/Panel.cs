using SolarPanels.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SolarPanels.Models
{
    public class Panel: IShape
    {
        public FloatPoint TopRightCorner { get; set; }
        public float Width { get; set; }
        public float Heigth { get; set; }
        public float Tilt { get; set; }


        /*
        90 should be 0
        0 shuld be 1
        45 should be 0.5
        */
        public float GetTiltedHeight()
        {
            var reversedTilt = (float)Math.Abs(90d - Tilt);
            var ratio = reversedTilt / 90f;
            return Heigth * ratio;
        }

        public IEnumerable<LineSegment> GetLines()
        {
            var color = System.Windows.Media.Brushes.Blue;
            var tiltedHeight = GetTiltedHeight();

            var line1 = new LineSegment()
            {
                Point1 = TopRightCorner,
                Point2 = new FloatPoint(TopRightCorner.X + Width, TopRightCorner.Y),
                Stroke = color
            };

            yield return line1;

            var line2 = new LineSegment()
            {
                Point1 = line1.Point2,
                Point2 = new FloatPoint(TopRightCorner.X + Width, TopRightCorner.Y + tiltedHeight),
                Stroke = color
            };

            yield return line2;

            var line3 = new LineSegment()
            {
                Point1 = line2.Point2,
                Point2 = new FloatPoint(TopRightCorner.X, TopRightCorner.Y + tiltedHeight),
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

        public IEnumerable<LineSegment> GetPaddedLines(float rowSpacing, float columnSpacing)
        {
            var color = System.Windows.Media.Brushes.LightSkyBlue;
            var lines = GetLines().GetEnumerator();

            lines.MoveNext();
            var line = lines.Current;

            var paddedLine1 = new LineSegment()
            {
                Point1 = new FloatPoint(line.Point1.X - columnSpacing, line.Point1.Y - rowSpacing),
                Point2 = new FloatPoint(line.Point2.X + columnSpacing, line.Point2.Y - rowSpacing),
                Stroke = color
            };

            yield return paddedLine1;

            lines.MoveNext();
            line = lines.Current;

            var paddedLine2 = new LineSegment()
            {
                Point1 = paddedLine1.Point2,
                Point2 = new FloatPoint(line.Point2.X + columnSpacing, line.Point2.Y + rowSpacing),
                Stroke = color
            };

            yield return paddedLine2;

            lines.MoveNext();
            line = lines.Current;

            var paddedLine3 = new LineSegment()
            {
                Point1 = paddedLine2.Point2,
                Point2 = new FloatPoint(line.Point2.X - columnSpacing, line.Point2.Y + rowSpacing),
                Stroke = color
            };

            yield return paddedLine3;

            yield return new LineSegment()
            {
                Point1 = paddedLine3.Point2,
                Point2 = paddedLine1.Point1,
                Stroke = color
            };
        }
    }
}
