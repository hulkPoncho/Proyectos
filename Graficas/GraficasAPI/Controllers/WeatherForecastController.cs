using Microsoft.AspNetCore.Mvc;

namespace GraficasAPI.Controllers
{
    public class Informacion
    {
        public List<string> xValues { get; set; }
        public List<int> yValues { get; set; }
        public List<string> barColors { get; set; }
    }

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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("getInformacion")]
        public Informacion getInformacion()
        {
            var xValues = new List<String> { "Italy", "France", "Spain", "USA", "Mexico" };
            var yValues = new List<int> { 55, 49, 44, 24, 15 };
            var barColors = new List<string> {   "#b91d47", "#00aba9", "#2b5797","#e8c3b9", "#1e7145" };
            var informacionGrafica = new Informacion
            {
                xValues = xValues,
                yValues = yValues,
                barColors = barColors
            };
            return informacionGrafica;
        }
        
    }
}