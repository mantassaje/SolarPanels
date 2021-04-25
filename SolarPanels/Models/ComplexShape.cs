using SolarPanels.Extensions;
using SolarPanels.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SolarPanels.Models
{
    /// <summary>
    /// Any shape from list of lines.
    /// </summary>
    public class ComplexShape: IShape
    {
        public IEnumerable<LineSegment> Lines { get; set; }

        public IEnumerable<LineSegment> GetLines()
        {
            return Lines;
        }
    }
}
