using SolarPanels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanels.Extensions
{
    public static class PanelExtensions
    {
        public static ComplexShape GetPaddedShape(this Panel panel, float rowSpacing, float columnSpacing)
        {
            return new ComplexShape()
            {
                Lines = panel.GetPaddedLines(rowSpacing, columnSpacing)
            };
        }
    }
}
