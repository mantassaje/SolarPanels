using Caliburn.Micro;
using SolarPanels.Extensions;
using SolarPanels.Factories;
using SolarPanels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SolarPanels.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        private int _pressCount;

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        private string _testText;
        public string TestText 
        {
            get { return _testText; }
            set
            {
                _testText = value;
                NotifyOfPropertyChange(() => TestText);
            }
        }

        public ObservableCollection<LineSegment> _lines;
        public ObservableCollection<LineSegment> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                NotifyOfPropertyChange(() => Lines);
            }
        }

        public MainViewModel()
        {
            var shape1 = ShapeFactory.CreateBuildZone();
            var shape2 = ShapeFactory.CreateBlockedZone();

            var lines = shape1.Lines.Union(shape2.Lines).ToList();

            var inrLine1 = new LineSegment()
            {
                Point1 = new Point(20, 20),
                Point2 = new Point(20, 50),
                Stroke = System.Windows.Media.Brushes.Green
            };

            var inrLine2 = new LineSegment()
            {
                Point2 = new Point(40, 40),
                Point1 = new Point(21, 17),
                Stroke = System.Windows.Media.Brushes.Green
            };

            lines.Add(inrLine1);
            lines.Add(inrLine2);

            Message = inrLine1.FindIntersection(inrLine2)?.ToString();

            Lines = new ObservableCollection<LineSegment>(lines);
        }

        public void GenerateButton()
        {
            _pressCount++;

            Message = "Presses = " + _pressCount;

            Lines = new ObservableCollection<LineSegment>()
            {
                new LineSegment()
                {
                    Point1 = new Point(_pressCount, _pressCount),
                    Point2 = new Point(_pressCount + 20, _pressCount + 30),
                    Stroke = System.Windows.Media.Brushes.Green
                }
            };
        }
    }
}
