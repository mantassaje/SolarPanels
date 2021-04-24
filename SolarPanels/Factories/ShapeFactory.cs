using SolarPanels.Extensions;
using SolarPanels.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SolarPanels.Factories
{
    /// <summary>
    /// Service that builds hardcoded zones.
    /// </summary>
    public static class ShapeFactory
    {
        private static List<Point> _buildZone = new List<Point>()
        {
            new Point(83, 136),
            new Point(124, 186),
            new Point(252, 155),
            new Point(227, 47),
            new Point(183, 82),
            new Point(163, 4),
            new Point(80, 25),
            new Point(83, 136)
        };

        private static List<Point> _blockedZone = new List<Point>()
        {
            new Point(173, 123),
            new Point(182, 143),
            new Point(145, 147),
            new Point(134, 121)
        };

        public static ComplexShape CreateBuildZone()
        {
            return new ComplexShape()
            {
                Lines = _buildZone
                    .ToLines(System.Windows.Media.Brushes.SandyBrown)
                    .ToList()
            };
        }

        public static ComplexShape CreateBlockedZone()
        {
            return new ComplexShape()
            {
                Lines = _blockedZone
                    .ToLines(System.Windows.Media.Brushes.Red)
                    .ToList()
            };
        }
    }
}
