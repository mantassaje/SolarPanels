﻿using SolarPanels.Models;
using SolarPanels.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarPanels.Services
{
    public class DisplayService
    {
        public List<LineSegment> DrawLines { get; private set; } = new List<LineSegment>();

        public void AddShapes(params IShape[] shapes)
        {
            foreach (var shape in shapes)
            {
                DrawLines.AddRange(shape.GetLines());
            }
        }

        public void AddLine(LineSegment line)
        {
            DrawLines.Add(line);
        }
    }
}