using Microsoft.AspNetCore.Mvc;
using TaskInfotecsCS.Models.FileProcessors;
using TaskInfotecsCS.DbData;
using TaskInfotecsCS.DbTables;
using TaskInfotecsCS.ResultProcessors;
using TaskInfotecsCS.FilterDbProcessors;

namespace App.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;


        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        /*[HttpPost("upload")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            BaseFileProcessor processor = new CSVFileProcessor(file, _context);

            // 1. Записываем файл в БД
            await processor.WriteFileBD();

            // 2. Достаем записанные данные обратно из БД
            List<Values> dbValues = await processor.GetValuesFromDb();

            // 3. Считаем результат
            var calculator = new ResultCalculator();
            Result resultObject = calculator.Calculate(dbValues, file.FileName);

            // 4. Записываем Result в БД
            await processor.SaveResultBD(resultObject);

            return Ok();
        }*/

        [HttpGet]
        public async Task<ActionResult<List<Result>>> GetFilteredResults([FromQuery] ResultFilterDto filter)
        {
            var filterBuilder = new FilterResultTableDb(_context);

            var results = await filterBuilder
                .FilterByFileName(filter.FileName)
                .FilterByFirstOperationTime(filter.FirstOpTimeFrom, filter.FirstOpTimeTo)
                .FilterByAvgValue(filter.MinAvgValue, filter.MaxAvgValue)
                .FilterByAvgExecutionTime(filter.MinAvgExecTime, filter.MaxAvgExecTime)
                .ExecuteAsync();

            return Ok(results);
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