using Microsoft.AspNetCore.Mvc;
using SodukoAPI.Services;

namespace SodukoAPI.Controllers
{
    public class HomeController : Controller
    {
        private Generator generator;

        public HomeController(Services.Generator generator)
        {
            this.generator = generator;
        }

        public string Index(int? id)
        {
            Model.SodukoBoard board = generator.Generate();
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(board, options);
            return $"id is '{id}'";
        }
    }
}
