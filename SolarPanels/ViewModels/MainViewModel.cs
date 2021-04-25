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
        private readonly PanelGeneratorService _panelGeneratorService;

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

        public MainViewModel(DisplayService displayService, PanelGeneratorService panelGeneratorService)
        {
            _displayService = displayService;
            _panelGeneratorService = panelGeneratorService;

            /*var inrLine1 = new LineSegment()
            {
                Point1 = new FloatPoint(116, 25),
                Point2 = new FloatPoint(116, 35.8333333333333f),
                Stroke = System.Windows.Media.Brushes.Green
            };

            var inrLine2 = new LineSegment()
            {
                Point2 = new FloatPoint(78, 28),
                Point1 = new FloatPoint(119, 28),
                Stroke = System.Windows.Media.Brushes.Green
            };

            _displayService.AddLine(inrLine1);
            _displayService.AddLine(inrLine2);

            Message = inrLine1.FindIntersection(inrLine2)?.ToString();*/

            _panelGeneratorService.Generate(35, 15, 25, 3, 3);
            _displayService.AddShapes(_panelGeneratorService.GetShapes().ToArray());

            Lines = new ObservableCollection<LineSegment>(_displayService.DrawLines);
        }

        public void GenerateButton()
        {
        }
    }
}
