using Common.DAL.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Client.Models
{
    public class ViewWeatherModel : INotifyPropertyChanged
    {
        private string _selectedHour;
        public ObservableCollection<string> Hours => new() { "0:00", "3:00", "6:00", "9:00", "12:00", "15:00", "18:00", "21:00" };

        private WeatherModel _forecastWithInHour { get; set; }

        public List<WeatherModel> Forecast { get; set; }

        private string _selectedCity;

        public ObservableCollection<string> Cities { get; set; }


        private static readonly HttpClient client = new HttpClient();

        public ViewWeatherModel()
        {
            GetAllCities();
        }

        public WeatherModel ForecastWithInHour
        {
            get { return _forecastWithInHour; }
            set
            {
                _forecastWithInHour = value;
                OnPropertyChanged("ForecastWithInHour");
            }
        }

        public string SelectedHour
        {
            get { return _selectedHour; }
            set
            {
                if (Forecast == null)
                {
                    return;
                }
                _selectedHour = value;
                OnPropertyChanged("SelectedHour");
                GetWeatherWithinHour(value);
            }
        }

        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged("SelectedCity");
                TommorowForecast(value);
            }
        }


        internal void GetAllCities()
        {
            var reponse = client.GetStringAsync("https://localhost:44388/api/WeatherForecast/GetAllCities/GetAllCities").Result;
            Cities = JsonConvert.DeserializeObject<ObservableCollection<string>>(reponse);
        }

        private void TommorowForecast(string cityName)
        {
            var reponse = client.GetStringAsync($"https://localhost:44388/api/WeatherForecast/GetForecastForTomorow/GetForecastForTomorow/{cityName}").Result;
            var result = JsonConvert.DeserializeObject<List<Weather>>(reponse);

            if (!result.Any())
            {
                Forecast = null;
                ShowMessage();
                return;
            }

            Forecast = new List<WeatherModel>();

            foreach (var i in result)
            {
                var forecast = new WeatherModel
                {
                    ElementId = i.ElementId,
                    Date = i.Date.ToShortDateString(),
                    Sky = i.Sky,
                    Temperature = i.Temperature,
                    WindSpeed = i.WindSpeed,
                    Precipetation = i.Precipetation,
                    CityName = i.CityName,
                    Hour = i.Hour
                };
                Forecast.Add(forecast);
            }
        }

        private void GetWeatherWithinHour(string hour)
        {
            ForecastWithInHour = Forecast.SingleOrDefault(i => i.Hour == hour);
        }

        private void ShowMessage()
        {
            MessageBox.Show("Загрузите актуальный прогноз через консольное приложение и нажмите кнопку Обновить");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
