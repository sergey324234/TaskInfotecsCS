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

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            BaseValuesTableFileProcessor processorValues = new CSVFileProcessor(file, _context);

            await processorValues.WriteFileBD();

            List<Values> dbValues = await processorValues.LoadDataTable();

            var calculator = new ResultCalculator();
            Result resultObject = calculator.Calculate(dbValues, file.FileName);

            BaseResultTableFileProcessor processorResult = new BaseResultTableFileProcessor(file, _context);

            await processorResult.SaveDataTable(resultObject);

            return Ok();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<Result>>> GetFilteredResults([FromQuery] FilterResultTableParam filter)
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

        [HttpGet("query")]
        public async Task<IActionResult> GetLatestValues([FromQuery] string fileName)
        {
            BaseQueryTable<Values> filter = new QueryValuesTables(_context);
            var data = await filter.GetLatestAsync(fileName);
            return Ok(data);
        }

    }
}