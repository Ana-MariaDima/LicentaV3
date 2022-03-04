using Licenta.Models.Relations.Many_to_Many;
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
    public class IngredienteController : ControllerBase
    {

        private readonly IDemoService _demoService;

        public IngredienteController(IDemoService demoService)
        {

            _demoService = demoService;
        }
      
        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetIngredienteRepository().GetAll();
            return Ok(result);
    

        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetIngredienteRepository().FindByIdAsync(id);
            return Ok(result);
         

        }
        //post =create 

        /*[HttpPost("add")]
        public async Task<IActionResult> Add(Ingrediente Ing)
        {
            Ing.Id = Guid.NewGuid();
            var repo = _demoService.GetIngredienteRepository();
            await repo.CreateAsync(Ing);
            await repo.SaveAsync();
            return Ok();
        }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<Ingrediente> Ingrediente)
        {
            foreach (var Ing in Ingrediente)
            {
                Ing.Id = Guid.NewGuid();
                var repo = _demoService.GetIngredienteRepository();
                await repo.CreateAsync(Ing);
                await repo.SaveAsync();
            }
            return Ok();
        }
        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<Ingrediente> Ingrediente)
        {
            foreach (var Ing in Ingrediente)
            {
                //Ing.Id = Guid.NewGuid();
                var repo = _demoService.GetIngredienteRepository();
                await repo.CreateAsync(Ing);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Ingrediente Ing)
        {

            var repo = _demoService.GetIngredienteRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);
            repo.Update(Ing);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetIngredienteRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }
}
