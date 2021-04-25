using SolarPanels.Models;
using SolarPanels.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarPanels.Services
{
    public class DisplayService
    {
        private List<LineSegment> _drawLines = new List<LineSegment>();

        public void AddShapes(params IShape[] shapes)
        {
            foreach (var shape in shapes)
            {
                _drawLines.AddRange(shape.GetLines());
            }
        }

        public void AddLine(LineSegment line)
        {
            _drawLines.Add(line);
        }

        public void Clear()
        {
            _drawLines = new List<LineSegment>();
        }

        public IEnumerable<LineSegment> GetDrawLines(float scale)
        {
            return _drawLines.Select(line => new LineSegment()
            {
                Point1 = new FloatPoint(line.Point1.X * scale, line.Point1.Y * scale),
                Point2 = new FloatPoint(line.Point2.X * scale, line.Point2.Y * scale),
                Stroke = line.Stroke
            });
        }
    }
}
