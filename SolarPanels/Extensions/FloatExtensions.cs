using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanels.Extensions
{
    public static class FloatExtensions
    {
        public static double DegreesToRadians(this float degrees)
        {
            var radians = degrees * Math.PI / 180.0;
            return radians;
        }
    }
}
