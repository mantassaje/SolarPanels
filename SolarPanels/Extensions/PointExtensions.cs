
using SolarPanels.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SolarPanels.Extensions
{
    public static class PointExtensions
    {
        public static IEnumerable<Line> ToLines(this IEnumerable<Point> points, SolidColorBrush color)
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

                yield return new Line()
                {
                    Point1 = previousPoint.Value,
                    Point2 = point,
                    Stroke = color
                };

                previousPoint = point;
            }

            yield return new Line()
            {
                Point1 = points.First(),
                Point2 = points.Last(),
                Stroke = color
            };
        }
    }
}
