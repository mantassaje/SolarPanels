
using SolarPanels.Models;
using SolarPanels.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SolarPanels.Extensions
{
    public static class FloatPointExtensions
    {
        public static IEnumerable<Models.LineSegment> ToLines(this IEnumerable<FloatPoint> points, SolidColorBrush color)
        {
            if (points == null
                || points.Count() == 0)
            {
                yield break;
            }

            FloatPoint? previousPoint = null;

            foreach (var point in points)
            {
                if (previousPoint == null)
                {
                    previousPoint = point;
                    continue;
                }

                yield return new Models.LineSegment()
                {
                    Point1 = previousPoint.Value,
                    Point2 = point,
                    Stroke = color
                };

                previousPoint = point;
            }

            yield return new Models.LineSegment()
            {
                Point1 = points.First(),
                Point2 = points.Last(),
                Stroke = color
            };
        }

        /// <summary>
        /// Check if this point is inside passed shape.
        /// </summary>
        public static bool IsInside(this FloatPoint point, IShape shape)
        {
            var lines = shape.GetLines();

            var minX = lines
                .SelectAllX()
                .Min();

            var rayCast = new Models.LineSegment()
            {
                Point1 = new FloatPoint(minX - 1, point.Y),
                Point2 = point
            };

            var intersectionCount = shape.FindIntersections(rayCast).Count();

            return intersectionCount % 2 == 1;
        }
    }
}
