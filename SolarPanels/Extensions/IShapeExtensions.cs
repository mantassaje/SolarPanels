using SolarPanels.Models;
using SolarPanels.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SolarPanels.Extensions
{
    public static class IShapeExtensions
    {
        public static bool IsInside(this IShape shape, Point point)
        {
            var lines = shape.GetLines();

            var minX = lines
                .SelectMany(line =>
                    new double[] {
                        line.Point1.X,
                        line.Point2.X
                    })
                .Min();

            var rayCast = new LineSegment()
            {
                Point1 = new Point(minX - 1, point.Y),
                Point2 = point
            };

            var intersectionCount = shape.FindIntersections(rayCast).Count();

            return intersectionCount % 2 == 1;
        }

        public static IEnumerable<Point> FindIntersections(this IShape shape, LineSegment line)
        {
            return shape
                .GetLines()
                .Select(shapeLine => shapeLine.FindIntersection(line))
                .Where(point => point.HasValue)
                .Select(point => point.Value);
        }

        public static bool DoesIntersects(this IShape shape, LineSegment line)
        {
            return shape
                .GetLines()
                .Any(shapeLine => shapeLine.FindIntersection(line) != null);
        }

        public static bool IsInside(this IShape shapeZone, IShape shapeInside)
        {
            var insideShapeLines = shapeInside.GetLines();

            if (insideShapeLines.Count() == 0)
            {
                return false;
            }

            var insideFirstPoint = insideShapeLines.First().Point1;

            if (!shapeZone.IsInside(insideFirstPoint))
            {
                return false;
            }

            foreach(var insideLine in insideShapeLines)
            {
                if (shapeZone.DoesIntersects(insideLine))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
