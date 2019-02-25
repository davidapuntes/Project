using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;

namespace WeatherApp.ViewModel
{
    public class WeatherVM
    {

        //5.- Creamos en el ViewModel una propiedad del modelo...Seguir el 6) en la vista
        public WeatherUnderground Weather { get; set; } //Inyectado en el paso 9) en la vista....

        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                GetCities();
            }
        }

        /* La ObservableCollection class se usa para que cuando está propiedad esté
        ** binded to una vista, la vista se actualice automáticamente cuando cualquier elemento de la
        ** lista cambie (se añade uno, se elimine otro...etc...) */
        public ObservableCollection<RESULT> Cities { get; set; }

        private RESULT selectedResult;

        public RESULT SelectedResult
        {
            get { return selectedResult; }
            set
            {
                selectedResult = value;
                GetWeather();
            }
        }

        public RefreshCommand RefreshCommand { get; set; }

        public WeatherVM()
        {
            Weather = new WeatherUnderground();
            Cities = new ObservableCollection<RESULT>();
            SelectedResult = new RESULT();
            RefreshCommand = new RefreshCommand(this);
        }

        private async void GetCities()
        {
            var cities = await WeatherAPI.GetAutocompleteAsync(Query);

            Cities.Clear();
            foreach(var city in cities)
            {
                Cities.Add(city);
            }
        }

        public async void GetWeather()
        {
            if (SelectedResult != null)
            {
                var weather = await WeatherAPI.GetWeatherInformationAsync(SelectedResult.L);
                Weather.Current_Observation.Weather = weather.Current_Observation.Weather;
                Weather.Current_Observation.UV = weather.Current_Observation.UV;
                Weather.Current_Observation.Wind_String = weather.Current_Observation.Wind_String;
                Weather.Current_Observation.Precip_Today_String = weather.Current_Observation.Precip_Today_String;
                Weather.Current_Observation.Temperature_String = weather.Current_Observation.Temperature_String;
                Weather.Current_Observation.Display_Location.City = weather.Current_Observation.Display_Location.City;
            }
        }
    }
}
