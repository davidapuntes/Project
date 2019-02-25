using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class WeatherAPI
    {
        public const string API_KEY = "ea31f202bd6daddc";
        public const string BASE_URL = "http://api.wunderground.com/api/{0}/conditions/{1}.json";
        public const string BASE_URL_AUTOCOMPLETE = "http://autocomplete.wunderground.com/aq?query={0}";

        //Todos los métodos async (asincronos) devuelven una tarea
        //Se marca async porque dentro está esperando (await) a otros métodos async ....
        //El mismo, al tener el modificador async, ya puede ser awaited también...


        // ## Ver --> https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/ ## <--
        public static async Task<WeatherUnderground> GetWeatherInformationAsync(string link)
        {
            WeatherUnderground result = new WeatherUnderground();

            string url = string.Format(BASE_URL, API_KEY, link);

            using (HttpClient client = new HttpClient())
            {
                //Si no tuviese el await, devolvería un Task<TipoQuesea>...
                //Al ser await, ya devuelve el <TipoQueSea>
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<WeatherUnderground>(json);
            }

            return result;
        }

        public static async Task<List<RESULT>> GetAutocompleteAsync(string query)
        {
            List<RESULT> cities = new List<RESULT>();

            string url = string.Format(BASE_URL_AUTOCOMPLETE, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                var city = JsonConvert.DeserializeObject<City>(json);
                cities = city.RESULTS;
            }

            return cities;
        }
    }
}
