using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.Models
{
    public class WeatherModel : INotifyPropertyChanged
    {
        private int _elementId;

        private string _date;

        private string _sky;

        private string _temperature;

        private string _windSpeed;

        private string _precipetation;

        private string _cityName;

        private string _hour;


        public int ElementId
        {
            get => _elementId;
            set
            {
                if (value == _elementId) return;
                _elementId = value;
                NotifyPropertyChanged();
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                if (value == _date) return;
                _date = value;
                NotifyPropertyChanged();
            }
        }

        public string Sky
        {
            get => _sky;
            set
            {
                if (value == _sky) return;
                _sky = value;
                NotifyPropertyChanged();
            }
        }

        public string Temperature
        {
            get => _temperature;
            set
            {
                if (value == _temperature) return;
                _temperature = value;
                NotifyPropertyChanged();
            }
        }

        public string WindSpeed
        {
            get => _windSpeed;
            set
            {
                if (value == _windSpeed) return;
                _windSpeed = value;
                NotifyPropertyChanged();
            }
        }

        public string Precipetation
        {
            get => _precipetation;
            set
            {
                if (value == _precipetation) return;
                _precipetation = value;
                NotifyPropertyChanged();
            }
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                if (value == _cityName) return;
                _cityName = value;
                NotifyPropertyChanged();
            }
        }

        public string Hour
        {
            get => _hour;
            set
            {
                if (value == _hour) return;
                _hour = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
