using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriiIngredienteController : ControllerBase
    {

       // private readonly Context _context;
        private readonly IDemoService _demoService;

        public SubCategoriiIngredienteController( IDemoService demoService)
        {
           // _context = context;
            _demoService = demoService;
        }
      
       


        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             var result=await _demoService.GetSubCategoriiIngredienteRepository().GetAll();
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }

        

        [HttpGet("GetByCategorieIngrediente/{id}")]
        public ActionResult GetByCategorieIngrediente(Guid id)
        {
            var result =  _demoService.GetSubCategoriiIngredienteRepository().GetByCategorieIngrediente(id);
            return Ok(result);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetSubCategoriiIngredienteRepository().FindByIdAsync(id);
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        //post =create 

        /* [HttpPost("add")]
          public async Task<IActionResult> Add(SubCategoriiIngrediente CatIng)
          {
              CatIng.Id= Guid.NewGuid();
              var repo =  _demoService.GetSubCategoriiIngredienteRepository();
              await repo.CreateAsync(CatIng);
              await repo.SaveAsync();
              return Ok();
          }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<SubCategoriiIngrediente> CategoriiIng)
        {
            foreach (var CatIng in CategoriiIng)
            {
                CatIng.Id = Guid.NewGuid();
                var repo = _demoService.GetSubCategoriiIngredienteRepository();
                await repo.CreateAsync(CatIng);
                await repo.SaveAsync();
            }
            return Ok();
        }
        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<SubCategoriiIngrediente> CategoriiIng)
        {
            foreach (var CatIng in CategoriiIng)
            {
               // CatIng.Id = Guid.NewGuid();
                var repo = _demoService.GetSubCategoriiIngredienteRepository();
                await repo.CreateAsync(CatIng);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(SubCategoriiIngrediente CatIng)
        {
          
            var repo = _demoService.GetSubCategoriiIngredienteRepository();
           // var result = await repo.FindByIdAsync(CatIng.Id);
            repo.Update(CatIng);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetSubCategoriiIngredienteRepository();
             var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }

    }
}
