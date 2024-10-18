using CoreEhsTest.Dtos;
using CoreEhsTest.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreEhsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesmanComissionController : ControllerBase
    {
        private readonly ISalesmanComissionService _salesmanCommissionService;

        public SalesmanComissionController(ISalesmanComissionService salesmanComissionService)
        {
            _salesmanCommissionService = salesmanComissionService;
        }

        [HttpGet("SalesmanCommissionReport")]
        public ActionResult<IEnumerable<SalesComissionDto>> GenerateReport()
        {
            return Ok(_salesmanCommissionService.GenerateReport());
        }
    }
}
