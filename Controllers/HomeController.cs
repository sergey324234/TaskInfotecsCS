using Microsoft.AspNetCore.Mvc;
using TaskInfotecsCS.Models.FileProcessors;

namespace App.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly FactoryFileProcessor _factory;

        public HomeController(FactoryFileProcessor factory)
        {
            _factory = factory;
        }


        [HttpPost("upload-csv")] // Метод 1: POST на адрес api/home/upload-csv
        public IActionResult UploadCsv(IFormFile file)
        {
            var tmp = _factory.GetProcessor(file);
            
            return Ok(tmp.SupportConctentTypeFile);
        }

        [HttpGet("results")] // Метод 2: GET на адрес api/home/results
        public IActionResult GetResults()
        {
            return Ok("Метод для получения результатов");
        }

        [HttpGet("latest-values")] // Метод 3: GET на адрес api/home/latest-values
        public IActionResult GetLatestValues()
        {
            return Ok("Метод для получения 10 последних значений");
        }
        /*
        public IActionResult Index()
        {
            return View();
        }

        public string Privet()
        {
            return "hello world howare!";
        }

        [ActionName("well")]
        public string Test1()
        {
            return "ActionName";
        }
        public async Task Index2()
        {
            Response.ContentType = "text/html;charset=utf-8";
            System.Text.StringBuilder table = new("<h1>HEHHEHE </h1><table>");

            foreach(var header in Request.Headers)
            {
                table.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
            }

            table.Append("</table>");

            await Response.WriteAsync(table.ToString());
        }

        public string Index3() 
        {
            string age = Request.Query["age"];
            return $"name, {age}";
        }

        [HttpGet]
        public async Task Index4()
        {
            string content = @"<form method='post' action='/Home/PersonData'>
                <label>Name:</label><br />
                <input name='name' /><br />
                <label>Age:</label><br />
                <input type='number' name='age' /><br />
                <input type='submit' value='Send' />
            </form>";

            Response.ContentType = "text/html;charset=utf-8";
            await Response.WriteAsync(content);
        }

        [HttpPost]
        public string PersonData()
        {
            var name = Request.Form["name"];
            var age = Request.Form["age"];

            return $"{name}, {age}";
            
        }
        /*
        public IActionResult Index6()
        {
            return new HtmlResult("<h1>HELLO</h1>");
        }

        public IActionResult GetVoid1()
        {

            return new UnauthorizedResult();
        }*/
    }
}