using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dashboard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var temp = Enumerable.Range(1, 6).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55)
            }).ToList();
            foreach (var item in temp)
            {
                item.Summary = GetSummary(item.TemperatureC);
            }
            return temp;
        }

        private string GetSummary(int temperature)
        {
            if(temperature < 1)
            {
                return "Freezing";
            }
            if (temperature < 10)
            {
                return "Chilly";
            }
            if (temperature < 15)
            {
                return "Cool";
            }
            if (temperature < 19)
            {
                return "Mild";
            }
            if (temperature < 26)
            {
                return "Warm";
            }
            if (temperature < 30)
            {
                return "Hot";
            }
            if (temperature > 30)
            {
                return "Scorching";
            }
            return ":)";
        }
    }
}
