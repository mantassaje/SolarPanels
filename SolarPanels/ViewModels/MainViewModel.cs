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

        private float _length = 25;
        public float Length
        {
            get { return _length; }
            set
            {
                _length = value;
                NotifyOfPropertyChange(() => Length);
            }
        }

        private float _width = 15;
        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }
        private float _tilt = 25;
        public float Tilt
        {
            get { return _tilt; }
            set
            {
                if (_tilt > 60) _tilt = 60;
                else if (_tilt < 0) _tilt = 0;
                else _tilt = value;
                NotifyOfPropertyChange(() => Tilt);
            }
        }

        private float _rowSpacing = 8;
        public float RowSpacing
        {
            get { return _rowSpacing; }
            set
            {
                _rowSpacing = value;
                NotifyOfPropertyChange(() => RowSpacing);
            }
        }

        private float _columnSpacing = 2;
        public float ColumnSpacing
        {
            get { return _columnSpacing; }
            set
            {
                _columnSpacing = value;
                NotifyOfPropertyChange(() => ColumnSpacing);
            }
        }

        public ObservableCollection<LineSegment> _lines = new ObservableCollection<LineSegment>();
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
        }

        public void GenerateButton()
        {
            _panelGeneratorService.Generate(Length, Width, Tilt, RowSpacing, ColumnSpacing);

            _displayService.Clear();
            _displayService.AddShapes(_panelGeneratorService.GetShapes().ToArray());

            Lines = new ObservableCollection<LineSegment>(_displayService.GetDrawLines(2f));
        }
    }
}
