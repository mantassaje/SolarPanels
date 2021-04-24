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
        public static Point MinPoint(this IShape shape)
        {
            var shapeLine = shape.GetLines();

            var x = shapeLine
                .SelectMany(line =>
                    new double[] {
                        line.Point1.X,
                        line.Point2.X
                    })
                .Min();

            var y = shapeLine
                .SelectMany(line =>
                    new double[] {
                        line.Point1.Y,
                        line.Point2.Y
                    })
                .Min();

            return new Point(x, y);
        }

        public static Point MaxPoint(this IShape shape)
        {
            var shapeLine = shape.GetLines();

            var x = shapeLine
                .SelectMany(line =>
                    new double[] {
                        line.Point1.X,
                        line.Point2.X
                    })
                .Max();

            var y = shapeLine
                .SelectMany(line =>
                    new double[] {
                        line.Point1.Y,
                        line.Point2.Y
                    })
                .Max();

            return new Point(x, y);
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

        public static bool IsAllInside(this IShape shapeInside, IShape shapeZone)
        {
            var insideShapeLines = shapeInside.GetLines();

            if (insideShapeLines.Count() == 0)
            {
                return false;
            }

            var insideFirstPoint = insideShapeLines.First().Point1;

            if (!insideFirstPoint.IsInside(shapeZone))
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

        public static bool IsAnyInside(this IShape shapeInside, IShape shapeZone)
        {
            var insideShapeLines = shapeInside.GetLines();

            if (insideShapeLines.Count() == 0)
            {
                return false;
            }

            foreach (var insideLine in insideShapeLines)
            {
                if (shapeZone.DoesIntersects(insideLine))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
