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
    public class CategoriiReteteController : ControllerBase
    {

        private readonly IDemoService _demoService;

        public CategoriiReteteController(IDemoService demoService)
        {
          
            _demoService = demoService;
        }




        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetCategoriiReteteRepository().GetAll();
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetCategoriiReteteRepository().FindByIdAsync(id);
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        //post =create 

        /* [HttpPost("add")]
         public async Task<IActionResult> Add(CategoriiRetete CatRet)
         {
             CatRet.I d = Guid.NewGuid();
             var repo = _demoService.GetCategoriiReteteRepository();
             await repo.CreateAsync(CatRet);
             await repo.SaveAsync();
             return Ok();
         }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<CategoriiRetete> CategotiiRet)
        {
            foreach (var CatRet in CategotiiRet)
            {
                CatRet.Id = Guid.NewGuid();
                var repo = _demoService.GetCategoriiReteteRepository();
                await repo.CreateAsync(CatRet);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<CategoriiRetete> CategotiiRet)
        {
            foreach (var CatRet in CategotiiRet)
            {
                // CatRet.Id = Guid.NewGuid();
                var repo = _demoService.GetCategoriiReteteRepository();
                await repo.CreateAsync(CatRet);
                await repo.SaveAsync();
            }
            return Ok();
        }


        [HttpPost("update")]
        public async Task<IActionResult> Update(CategoriiRetete CatRet)
        {

            var repo = _demoService.GetCategoriiReteteRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);

            repo.Update(CatRet);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetCategoriiReteteRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }


}
