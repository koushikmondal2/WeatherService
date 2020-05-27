using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherServices.Model
{
    /// <summary>
    /// Weather status
    /// </summary>
    public class WeatherStatusModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public double Temparature { get; set; }
    }
}
