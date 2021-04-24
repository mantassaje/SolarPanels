using Caliburn.Micro;
using SolarPanels.Extensions;
using SolarPanels.Factories;
using SolarPanels.Models;
using SolarPanels.Services;
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
        private readonly DisplayService _displayService;

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

        public MainViewModel(DisplayService displayService)
        {
            _displayService = displayService;

            var shape1 = ShapeFactory.CreateBuildZone();
            var shape2 = ShapeFactory.CreateBlockedZone();

            _displayService.AddShape(shape1);
            _displayService.AddShape(shape2);

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

            _displayService.AddLine(inrLine1);
            _displayService.AddLine(inrLine2);

            var panel = new Panel()
            {
                TopRightCorner = new Point(160, 50),
                Heigth = 30,
                Width = 50
            };

            _displayService.AddShape(panel);

            Message = shape1.IsInside(panel).ToString();

            Lines = new ObservableCollection<LineSegment>(_displayService.DrawLines);
        }

        public void GenerateButton()
        {
        }
    }
}
