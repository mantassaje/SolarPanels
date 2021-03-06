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
        /// <summary>
        /// Return point of intersection.
        /// Return null if no intersection.
        /// </summary>
        public static FloatPoint? FindIntersection(this LineSegment line1, LineSegment line2)
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

            var infinateIntersecationPoint = new FloatPoint(
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
        /// Assume line object defines two separate corners of rectangular.
        /// Check if point is inside of the rectangular.
        /// </summary>
        private static bool IsInside(this LineSegment rectangelar, FloatPoint point)
        {
            //Rounding is a workaround. This sometimes gives wrong results with infinate fraction numbers.
            var corner1 = new FloatPoint(
                (float)Math.Round(Math.Min(rectangelar.Point1.X, rectangelar.Point2.X)),
                (float)Math.Round(Math.Min(rectangelar.Point1.Y, rectangelar.Point2.Y))
            );

            var corner2 = new FloatPoint(
                (float)Math.Round(Math.Max(rectangelar.Point1.X, rectangelar.Point2.X)),
                (float)Math.Round(Math.Max(rectangelar.Point1.Y, rectangelar.Point2.Y))
            );

            var roundedPoint = new FloatPoint()
            {
                X = (float)Math.Round(point.X),
                Y = (float)Math.Round(point.Y)
            };

            return roundedPoint.X >= corner1.X
                && roundedPoint.X <= corner2.X
                && roundedPoint.Y >= corner1.Y
                && roundedPoint.Y <= corner2.Y;
        }


        public static IEnumerable<float> SelectAllX(this IEnumerable<LineSegment> lines)
        {
            return lines
                .SelectMany(line =>
                    new float[] {
                        line.Point1.X,
                        line.Point2.X
                    }
                );
        }

        public static IEnumerable<float> SelectAllY(this IEnumerable<LineSegment> lines)
        {
            return lines
                .SelectMany(line =>
                    new float[] {
                        line.Point1.Y,
                        line.Point2.Y
                    }
                );
        }
    }
}
