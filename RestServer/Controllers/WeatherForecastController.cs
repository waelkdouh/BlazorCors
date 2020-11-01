using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[EnableCors("AllowEveryone")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static IEnumerable<WeatherForecast> WeatherForecast = GenerateWeatherForecast();

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public static IEnumerable<WeatherForecast> GenerateWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                ID = rng.Next(1,1000),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
            
        }

       
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return WeatherForecast.ToArray();
        }

      
        [HttpPut]
        //[Route("api/Employee/Edit")]
        public void Edit([FromBody] WeatherForecast model)
        {
            if (ModelState.IsValid)
            {
                var oldValue = WeatherForecast.ToList().FirstOrDefault(w => w.ID == model.ID);
                WeatherForecast.ToList().Select(x => x.Equals(oldValue) ? model : x);
            }
        }

    }
}
