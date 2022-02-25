using Licenta.Models.Relations.One_to_Many;
using Licenta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReteteController : ControllerBase
    {

        private readonly IDemoService _demoService;

        public ReteteController(IDemoService demoService)
        {

            _demoService = demoService;
        }

        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetReteteRepository().GetAll();
            return Ok(result);


        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetReteteRepository().FindByIdAsync(id);
            return Ok(result);


        }
        //post =create 

        /*[HttpPost("add")]
        public async Task<IActionResult> Add(Retete Ret)
        {
            Ret.Id = Guid.NewGuid();
            var repo = _demoService.GetReteteRepository();
            await repo.CreateAsync(Ret);
            await repo.SaveAsync();
            return Ok();
        }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<Retete> Retete)
        {
            foreach (var Ret in Retete)
            {
                Ret.Id = Guid.NewGuid();
                var repo = _demoService.GetReteteRepository();
                await repo.CreateAsync(Ret);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Retete Ret)
        {

            var repo = _demoService.GetReteteRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);
            repo.Update(Ret);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetReteteRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }
}
