using SolarPanels.Extensions;
using SolarPanels.Factories;
using SolarPanels.Models;
using SolarPanels.Models.Interfaces;
using System.Collections.Generic;

namespace SolarPanels.Services
{
    public class PanelGeneratorService
    {
        private readonly ComplexShape _buildZone;
        private readonly List<ComplexShape> _blockedZones;
        private List<Panel> _panel;

        public PanelGeneratorService(ShapeFactory shapeFactory)
        {
            _buildZone = shapeFactory.CreateBuildZone();

            _blockedZones = new List<ComplexShape>()
            {
                shapeFactory.CreateBlockedZone()
            };
        }

        public void Generate(double width, double heigth, double rowSpacing, double heigthSpacing)
        {
            _panel = new List<Panel>();

            var minPoint = _buildZone.MinPoint();
            var maxPoint = _buildZone.MaxPoint();

            for(var x = minPoint.X; x < maxPoint.X; x = x + width + rowSpacing)
                for (var y = minPoint.Y; y < maxPoint.Y; y = y + heigth + heigthSpacing)
                {
                    var panel = new Panel()
                    {
                        TopRightCorner = new System.Windows.Point(x, y),
                        Width = width,
                        Heigth = heigth
                    };

                    if (IsValidShape(panel))
                    {
                        _panel.Add(panel);
                    }
                }
        }

        private bool IsValidShape(IShape shape)
        {
            if (!shape.IsAllInside(_buildZone))
            {
                return false;
            }

            foreach(var blockedZone in _blockedZones)
            {
                if (blockedZone.IsAnyInside(shape)
                    || shape.IsAnyInside(blockedZone))
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
            shapes.AddRange(_panel);


            return shapes;
        }
    }
}
