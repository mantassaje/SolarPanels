using SolarPanels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SolarPanels.Extensions
{
    public static class LineSegmentExtensions
    {
        public static Point? FindIntersection(this LineSegment line1, LineSegment line2)
        {
            var a1 = line1.Point2.Y - line1.Point1.Y;
            var b1 = line1.Point1.X - line1.Point2.X;
            var c1 = a1 * line1.Point1.X + b1 * line1.Point1.Y;

            var a2 = line2.Point2.Y - line2.Point1.Y;
            var b2 = line2.Point1.X - line2.Point2.X;
            var c2 = a2 * line2.Point1.X + b2 * line2.Point1.Y;

            var delta = a1 * b2 - a2 * b1;
            
            //If delta is 0 then lines are parallel.
            if (delta == 0)
            {
                return null;
            }

            var infinateIntersecationPoint = new Point(
                (b2 * c1 - b1 * c2) / delta, 
                (a1 * c2 - a2 * c1) / delta
            );

            if (line1.IsInside(infinateIntersecationPoint)
                && line2.IsInside(infinateIntersecationPoint))
            {
                return infinateIntersecationPoint;
            }

            return null;
        }

        /// <summary>
        /// Assume line object defines two separate corners of rectangelar.
        /// Check if point is inside of the rectangelar.
        /// </summary>
        /// <returns></returns>
        public static bool IsInside(this LineSegment rectangelar, Point point)
        {
            var corner1 = new Point(
                Math.Min(rectangelar.Point1.X, rectangelar.Point2.X),
                Math.Min(rectangelar.Point1.Y, rectangelar.Point2.Y)
            );

            var corner2 = new Point(
                Math.Max(rectangelar.Point1.X, rectangelar.Point2.X), 
                Math.Max(rectangelar.Point1.Y, rectangelar.Point2.Y)
            );

            return point.X >= corner1.X
                && point.X <= corner2.X
                && point.Y >= corner1.Y
                && point.Y <= corner2.Y;
        }
    }
}
