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
    public class UnitatiController : ControllerBase
    {

        private readonly IDemoService _demoService;

        public UnitatiController(IDemoService demoService)
        {

            _demoService = demoService;
        }




        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetUnitatiRepository().GetAll();
            return Ok(result);
         

        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetUnitatiRepository().FindByIdAsync(id);
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        //post =create 

        /*[HttpPost("add")]
        public async Task<IActionResult> Add(Unitati Unit)
        {
            Unit.Id = Guid.NewGuid();
            var repo = _demoService.GetUnitatiRepository();
            await repo.CreateAsync(Unit);
            await repo.SaveAsync();
            return Ok();
        }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<Unitati> Unitati)
        {
            foreach (var Unit in Unitati)
            {
                Unit.Id = Guid.NewGuid();
                var repo = _demoService.GetUnitatiRepository();
                await repo.CreateAsync(Unit);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<Unitati> Unitati)
        {
            foreach (var Unit in Unitati)
            {
                //Unit.Id = Guid.NewGuid();
                var repo = _demoService.GetUnitatiRepository();
                await repo.CreateAsync(Unit);
                await repo.SaveAsync();
            }
            return Ok();
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(Unitati Unit)
        {

            var repo = _demoService.GetUnitatiRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);

            repo.Update(Unit);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetUnitatiRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }


}

