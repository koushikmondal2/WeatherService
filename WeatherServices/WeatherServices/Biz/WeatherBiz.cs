using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherServices.Model;
using WeatherServices.Service;

namespace WeatherServices.Biz
{
    public class WeatherBiz : IWeatherBiz
    {
        public readonly IWeatherService weatherService;
        public WeatherBiz(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        /// <summary>
        /// Getting Weather Details from Service class Test 
        /// </summary>
        /// <param name="cities">Cities Details</param>
        /// <returns></returns>
        public async Task<IEnumerable<WeatherStatusModel>> GetWeatherDetails(string cities)
        {
            try
            {
                var cityDetails = cities.Split(',');
                return await weatherService.GetWeatherDetails(cityDetails).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
                //exception message
            }
        }
    }
}
