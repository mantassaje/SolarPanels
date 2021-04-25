using SolarPanels.Extensions;
using SolarPanels.Factories;
using SolarPanels.Models;
using SolarPanels.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SolarPanels.Services
{
    public class PanelGeneratorService
    {
        private readonly ComplexShape _buildZone;
        private readonly List<ComplexShape> _blockedZones;
        private List<Panel> _panels;

        private float _rowSpacing;
        private float _columnSpacing;

        public PanelGeneratorService(ShapeFactory shapeFactory)
        {
            _buildZone = shapeFactory.CreateBuildZone();

            _blockedZones = new List<ComplexShape>()
            {
                shapeFactory.CreateBlockedZone()
            };
        }

        /// <summary>
        /// Generates panels with provided parameters.
        /// Result will be save inside this service class.
        /// </summary>
        public void Generate(float width, float heigth, float tilt, float rowSpacing, float columnSpacing)
        {
            _panels = new List<Panel>();
            _rowSpacing = rowSpacing;
            _columnSpacing = columnSpacing;

            var minPoint = _buildZone.MinPoint();
            var maxPoint = _buildZone.MaxPoint();

            for (var x = minPoint.X; x < maxPoint.X; x = x + 1)
                for (var y = minPoint.Y; y < maxPoint.Y; y = y + 1)
                {
                    var panel = new Panel()
                    {
                        TopRightCorner = new FloatPoint(x, y),
                        Width = width,
                        Heigth = heigth,
                        Tilt = tilt
                    };

                    if (IsValid(panel))
                    {
                        _panels.Add(panel);
                    }
                }
        }

        private bool IsValid(Panel panel)
        {
            if (!panel.IsAllInside(_buildZone))
            {
                return false;
            }

            foreach(var blockedZone in _blockedZones)
            {
                if (blockedZone.IsAnyInside(panel)
                    || panel.IsAnyInside(blockedZone))
                {
                    return false;
                }
            }

            var paddedPanel = panel.GetPaddedShape(_rowSpacing, _columnSpacing);

            foreach (var otherPanel in _panels)
            {
                var otherLines = otherPanel.GetLines();

                if (otherPanel.IsAnyInside(paddedPanel))
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<IShape> GetShapes()
        {
            var shapes = new List<IShape>();

            shapes.Add(_buildZone);
            shapes.AddRange(_blockedZones);
            shapes.AddRange(_panels);
            shapes.AddRange(_panels.Select(panel => panel.GetPaddedShape(_rowSpacing, _columnSpacing)));

            return shapes;
        }
    }
}
