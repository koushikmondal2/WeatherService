using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherServices.Biz;
using WeatherServices.Model;

namespace WeatherServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherBiz _weatherBiz; 

        public WeatherController(IWeatherBiz weatherBiz)
        {
            this._weatherBiz = weatherBiz;
        }


        /// <summary>
        /// Get Weather Details by City
        /// </summary>
        /// <param name="cities"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<WeatherStatusModel>> Get(string cities)
        {
            if (string.IsNullOrEmpty(cities))
            {
                throw new ArgumentException("Invalid Input");
            }
            var result = await _weatherBiz.GetWeatherDetails(cities).ConfigureAwait(false);
            return result;
        }
    }
}