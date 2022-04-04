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
    public class ReteteIngredienteController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public ReteteIngredienteController(IDemoService demoService)
        {

            _demoService = demoService;
        }

        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetReteteIngredienteRepository().GetAll();
            return Ok(result);


        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetReteteIngredienteRepository().FindByIdAsync(id);
            return Ok(result);


        }

        [HttpGet("GetByReteta/{id}")]

        public ActionResult GetByReteta(Guid id)
        {
            var result =  _demoService.GetReteteIngredienteRepository().GetByReteta(id);
            return Ok(result);
        

        }
        //post =create 

        /* [HttpPost("add")]
         public async Task<IActionResult> Add(RetetaIngrediente RetIng)
         {
             RetIng.Id = Guid.NewGuid();
             var repo = _demoService.GetReteteIngredienteRepository();
             await repo.CreateAsync(RetIng);
             await repo.SaveAsync();
             return Ok();
         }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<RetetaIngrediente> ReteteIng)
        {
            foreach (var RetIng in ReteteIng)
            {
                RetIng.Id = Guid.NewGuid();
                var repo = _demoService.GetReteteIngredienteRepository();
                await repo.CreateAsync(RetIng);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<RetetaIngrediente> ReteteIng)
        {
            foreach (var RetIng in ReteteIng)
            {
                //RetIng.Id = Guid.NewGuid();
                var repo = _demoService.GetReteteIngredienteRepository();
                await repo.CreateAsync(RetIng);
                await repo.SaveAsync();
            }
            return Ok();
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(RetetaIngrediente RetIng)
        {

            var repo = _demoService.GetReteteIngredienteRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);
            repo.Update(RetIng);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetReteteIngredienteRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }
}
