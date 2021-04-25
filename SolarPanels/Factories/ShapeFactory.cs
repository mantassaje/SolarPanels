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
    public class ShapeFactory
    {
        private List<FloatPoint> _buildZone = new List<FloatPoint>()
        {
            new FloatPoint(83, 136),
            new FloatPoint(124, 186),
            new FloatPoint(252, 155),
            new FloatPoint(227, 47),
            new FloatPoint(183, 82),
            new FloatPoint(163, 4),
            new FloatPoint(80, 25),
            new FloatPoint(83, 136)
        };

        private List<FloatPoint> _blockedZone = new List<FloatPoint>()
        {
            new FloatPoint(173, 123),
            new FloatPoint(182, 143),
            new FloatPoint(145, 147),
            new FloatPoint(134, 121)
        };

        public ComplexShape CreateBuildZone()
        {
            return new ComplexShape()
            {
                Lines = _buildZone
                    .ToLines(System.Windows.Media.Brushes.SandyBrown)
                    .ToList()
            };
        }

        public ComplexShape CreateBlockedZone()
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
