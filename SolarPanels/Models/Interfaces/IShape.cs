using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanels.Models.Interfaces
{
    public interface IShape
    {
        /// <summary>
        /// Get lines that form this shape.
        /// </summary>
        IEnumerable<LineSegment> GetLines();
    }
}
