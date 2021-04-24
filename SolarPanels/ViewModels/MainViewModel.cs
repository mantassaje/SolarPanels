using Caliburn.Micro;
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

        public ObservableCollection<Line> _lines;
        public ObservableCollection<Line> Lines
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
            Message = "Hello World";
            var shape1 = ShapeFactory.CreateBuildZone();
            var shape2 = ShapeFactory.CreateBlockedZone();

            var lines = shape1.Lines.Union(shape2.Lines);

            Lines = new ObservableCollection<Line>(lines);
        }

        public void GenerateButton()
        {
            _pressCount++;

            Message = "Presses = " + _pressCount;

            Lines = new ObservableCollection<Line>()
            {
                new Line()
                {
                    Point1 = new Point(_pressCount, _pressCount),
                    Point2 = new Point(_pressCount + 20, _pressCount + 30),
                    Stroke = System.Windows.Media.Brushes.Green
                }
            };
        }
    }
}
