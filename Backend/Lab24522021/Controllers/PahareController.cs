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
    public class PahareController : ControllerBase
    {

        private readonly IDemoService _demoService;

        public PahareController(IDemoService demoService)
        {

            _demoService = demoService;
        }




        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetPahareRepository().GetAll();
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetPahareRepository().FindByIdAsync(id);
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        //post =create 

        [HttpPost("add")]
        public async Task<IActionResult> Add(List< Pahare> pahare)
        {
            foreach (var pahar in pahare)
            {
                pahar.Id = Guid.NewGuid();
                var repo = _demoService.GetPahareRepository();
                await repo.CreateAsync(pahar);
                await repo.SaveAsync();
                
            }
            return Ok();
        }

        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<Pahare> pahare)
        {
            foreach (var pahar in pahare)
            {
                //pahar.Id = Guid.NewGuid();
                var repo = _demoService.GetPahareRepository();
                await repo.CreateAsync(pahar);
                await repo.SaveAsync();

            }
            return Ok();
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(Pahare pahar)
        {

            var repo = _demoService.GetPahareRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);

            repo.Update(pahar);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetPahareRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }


}
