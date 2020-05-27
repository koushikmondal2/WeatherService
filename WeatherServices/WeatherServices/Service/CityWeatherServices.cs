using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherServices.Model;

namespace WeatherServices.Service
{
    public class CityWeatherServices : IWeatherService
    {
        private readonly IOptions<AppConfiguration> _configuration;
        public CityWeatherServices(IOptions<AppConfiguration> configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// Process the weather details of mutiple City
        /// </summary>
        /// <param name="cities"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WeatherStatusModel>> GetWeatherDetails(string[] cities)
        {
            try
            {
                List<WeatherStatusModel> weatherStatus = new List<WeatherStatusModel>();

                foreach (var city in cities)
                {
                    var cityweather = await GetCityWeather(city).ConfigureAwait(false);
                    if (cityweather != null)
                    {
                        weatherStatus.Add(cityweather);
                    }
                }
                return weatherStatus.OrderBy(s => s.Temparature);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the City weather details
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<WeatherStatusModel> GetCityWeather(string city)
        {
            HttpClient client = new HttpClient();
            string url = string.Format(_configuration.Value.Weather, city);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response = await client.SendAsync(request);
            string data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            WeatherStatusModel payload = null;
            if (response.IsSuccessStatusCode)
            {
                payload = ExtractTemparedata(data);
            }
            return payload;
        }

        /// <summary>
        /// Generate Tempareture Payload
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static WeatherStatusModel ExtractTemparedata(string data)
        {
            WeatherStatusModel weatherStatus = null;
            if (data != null)
            {
                var cityData = JsonConvert.DeserializeObject<IDictionary<string, object>>(data);
                var temparatureData = JsonConvert.DeserializeObject<IDictionary<string, object>>(cityData["main"].ToString());
                weatherStatus = new WeatherStatusModel
                {
                    Index = Convert.ToInt32(cityData["id"]),
                    Name = cityData["name"].ToString(),
                    Temparature = Convert.ToDouble(temparatureData["temp"])
                };
                
            }
            return weatherStatus;
        }
    }
}
