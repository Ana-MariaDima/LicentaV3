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
    public class CategoriiIngredienteController : ControllerBase
    {

       // private readonly Context _context;
        private readonly IDemoService _demoService;

    public CategoriiIngredienteController(IDemoService demoService)
    {
        // _context = context;
        _demoService = demoService;
    }




    //get 
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _demoService.GetCategoriiIngredienteRepository().GetAll();
        return Ok(result);
        //return Ok(await _context.CategorieIngredient.ToListAsync());

    }
    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _demoService.GetCategoriiIngredienteRepository().FindByIdAsync(id);
        return Ok(result);
        //return Ok(await _context.CategorieIngredient.ToListAsync());

    }

       
        //post =create 
        /*
            [HttpPost("add")]
            public async Task<IActionResult> Add(CategoriiIngrediente CatIng)
            {
                CatIng.Id = Guid.NewGuid();
                var repo = _demoService.GetCategoriiIngredienteRepository();
                await repo.CreateAsync(CatIng);
                await repo.SaveAsync();
                return Ok();
            }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<CategoriiIngrediente> CategoriiIng)
        {
            foreach (var CatIng in CategoriiIng)
                {
                CatIng.Id = Guid.NewGuid();
                var repo = _demoService.GetCategoriiIngredienteRepository();
                await repo.CreateAsync(CatIng);
                await repo.SaveAsync(); 
            }
            return Ok();
        }
        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<CategoriiIngrediente> CategoriiIng)
        {
            foreach (var CatIng in CategoriiIng)
            {
                //CatIng.Id = Guid.NewGuid();
                var repo = _demoService.GetCategoriiIngredienteRepository();
                await repo.CreateAsync(CatIng);
                await repo.SaveAsync();
            }
            return Ok();
        }


        [HttpPost("update")]
    public async Task<IActionResult> Update(CategoriiIngrediente CatIng)
    {

        var repo = _demoService.GetCategoriiIngredienteRepository();
        // var result = await repo.FindByIdAsync(CatIng.Id);
        repo.Update(CatIng);
        await repo.SaveAsync();
        return Ok();
    }
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {

        var repo = _demoService.GetCategoriiIngredienteRepository();
        var result = await repo.FindByIdAsync(id);
        if (result == null)
            return Ok();
        repo.Delete(result);
        await repo.SaveAsync();
        return Ok();
    }

}
}
