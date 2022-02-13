namespace Workshop.Web.Controllers
{
    using System.IO;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [ApiController]
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IConfiguration configuration;

        public ApiController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [Route("weather")]
        public async Task<object> Weather(string lat, string lon)
        {
            var apiKey = this.configuration.GetValue<string>("WeatherApiKey");

            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string jsonResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonResponse = await reader.ReadToEndAsync();
            }

            var jsonObject = JsonSerializer.Deserialize<object>(jsonResponse);

            return jsonObject;
        }

        [Route("github-repos")]
        public async Task<object> Github()
        {
            var url = $"https://api.github.com/users/danieljekov/repos";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.UserAgent = "myTestApp";

            string jsonResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonResponse = await reader.ReadToEndAsync();
            }

            var jsonObject = JsonSerializer.Deserialize<object>(jsonResponse);

            return jsonObject;
        }
    }
}
