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
        /// <summary>
        /// Get coordinates of top left corner.
        /// </summary>
        public static FloatPoint MinPoint(this IShape shape)
        {
            var shapeLine = shape.GetLines();

            var x = shapeLine
                .SelectAllX()
                .Min();

            var y = shapeLine
                .SelectAllY()
                .Min();

            return new FloatPoint(x, y);
        }

        /// <summary>
        /// Get coordinates of bottom right corner.
        /// </summary>
        public static FloatPoint MaxPoint(this IShape shape)
        {
            var shapeLine = shape.GetLines();

            var x = shapeLine
                .SelectAllX()
                .Max();

            var y = shapeLine
                .SelectAllY()
                .Max();

            return new FloatPoint(x, y);
        }

        public static IEnumerable<FloatPoint> FindIntersections(this IShape shape, LineSegment line)
        {
            return shape
                .GetLines()
                .Select(shapeLine => shapeLine.FindIntersection(line))
                .Where(point => point.HasValue)
                .Select(point => point.Value)
                .Distinct();
        }

        public static bool DoesIntersects(this IShape shape, LineSegment line)
        {
            return shape
                .GetLines()
                .Any(shapeLine => shapeLine.FindIntersection(line) != null);
        }

        /// <summary>
        /// Is this shape is fully inside passed shapeZone.
        /// </summary>
        public static bool IsAllInside(this IShape shapeInside, IShape shapeZone)
        {
            var insideShapeLines = shapeInside.GetLines();

            if (insideShapeLines.Count() == 0)
            {
                return false;
            }

            var insideFirstPoint = insideShapeLines.First().Point1;
            var insideLowerPoint = insideShapeLines.Last().Point1;

            // Checking lower point is a workaround. If raycast hits the corner it will count it as collision and asume it is inside.
            if (!insideFirstPoint.IsInside(shapeZone)
                || !insideLowerPoint.IsInside(shapeZone))
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

        /// <summary>
        /// Is part of this shape is inside passed shapeZone.
        /// </summary>
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

            var insideFirstPoint = insideShapeLines.First().Point1;

            return insideFirstPoint.IsInside(shapeZone);
        }
    }
}
