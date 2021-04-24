
using SolarPanels.Models;
using SolarPanels.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SolarPanels.Extensions
{
    public static class PointExtensions
    {
        public static IEnumerable<Models.LineSegment> ToLines(this IEnumerable<Point> points, SolidColorBrush color)
        {
            if (points == null
                || points.Count() == 0)
            {
                yield break;
            }

            Point? previousPoint = null;

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

        public static bool IsInside(this Point point, IShape shape)
        {
            var lines = shape.GetLines();

            var minX = lines
                .SelectMany(line =>
                    new double[] {
                        line.Point1.X,
                        line.Point2.X
                    })
                .Min();

            var rayCast = new Models.LineSegment()
            {
                Point1 = new Point(minX - 1, point.Y),
                Point2 = point
            };

            var intersectionCount = shape.FindIntersections(rayCast).Count();

            return intersectionCount % 2 == 1;
        }
    }
}
