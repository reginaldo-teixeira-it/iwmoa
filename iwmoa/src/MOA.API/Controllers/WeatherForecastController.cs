using Microsoft.AspNetCore.Mvc;

namespace MOA.API.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing_RAT", "Bracing_RAT", "Chilly_RAT", "Cool_RAT", "Mild_RAT", "Warm_RAT", "Balmy_RAT", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController( ILogger<WeatherForecastController> logger )
        {
            _logger = logger;
        }

        [HttpGet( Name = "GetWeatherForecast" )]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range( 1, 5 ).Select( index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays( index ),
                TemperatureC = Random.Shared.Next( -20, 55 ),
                Summary = Summaries[ Random.Shared.Next( Summaries.Length ) ]
            } )
            .ToArray();
        }
    }
}