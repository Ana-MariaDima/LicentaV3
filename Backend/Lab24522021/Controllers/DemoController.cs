using Licenta.Services;
using Microsoft.AspNetCore.Mvc;


//in controler apelam serviciile 
namespace Lab44522021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        [HttpGet]
        public IActionResult GetByTitle(string nume_ingredient)
        {
            var result = _demoService.GetDataMappedByNume(nume_ingredient);
            return Ok(result);
        }
    }
}
