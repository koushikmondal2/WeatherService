using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherServices.Model;

namespace WeatherServices.Biz
{
    public interface IWeatherBiz
    {
        Task<IEnumerable<WeatherStatusModel>> GetWeatherDetails(string city);
    }
}
