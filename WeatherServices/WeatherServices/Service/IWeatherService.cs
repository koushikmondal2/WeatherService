using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherServices.Model;

namespace WeatherServices.Service
{
    /// <summary>
    /// Interface to get the Weather Data
    /// </summary>
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherStatusModel>> GetWeatherDetails(string[] city);
    }
}
