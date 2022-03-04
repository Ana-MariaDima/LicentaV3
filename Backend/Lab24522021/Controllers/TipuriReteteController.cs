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
    public class TipuriReteteController : ControllerBase
    {

        private readonly IDemoService _demoService;

        public TipuriReteteController(IDemoService demoService)
        {

            _demoService = demoService;
        }




        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetTipuriReteteRepository().GetAll();
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetTipuriReteteRepository().FindByIdAsync(id);
            return Ok(result);
            //return Ok(await _context.CategorieIngredient.ToListAsync());

        }
        //post =create 

        /* [HttpPost("add")]
         public async Task<IActionResult> Add(TipuriRetete CatRet)
         {
             CatRet.Id = Guid.NewGuid();
             var repo = _demoService.GetTipuriReteteRepository();
             await repo.CreateAsync(CatRet);
             await repo.SaveAsync();
             return Ok();
         }*/

        [HttpPost("add")]
        public async Task<IActionResult> Add(List<TipuriRetete> CategoriiRet)
        {
            foreach (var CatRet in CategoriiRet)
            {
                CatRet.Id = Guid.NewGuid();
                var repo = _demoService.GetTipuriReteteRepository();
                await repo.CreateAsync(CatRet);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<TipuriRetete> CategoriiRet)
        {
            foreach (var CatRet in CategoriiRet)
            {
                //CatRet.Id = Guid.NewGuid();
                var repo = _demoService.GetTipuriReteteRepository();
                await repo.CreateAsync(CatRet);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(TipuriRetete CatRet)
        {

            var repo = _demoService.GetTipuriReteteRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);

            repo.Update(CatRet);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetTipuriReteteRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }


}
