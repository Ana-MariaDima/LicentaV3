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
    public class AprecieriController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public AprecieriController(IDemoService demoService)
        {

            _demoService = demoService;
        }

        //get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _demoService.GetAprecieriRepository().GetAll();
            return Ok(result);


        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetAprecieriRepository().FindByIdAsync(id);
            return Ok(result);


        }
        //post =create 

        [HttpPost("add")]
        public async Task<IActionResult> Add(Aprecieri Apre)
        {
            Apre.Id = Guid.NewGuid();
            var repo = _demoService.GetAprecieriRepository();
            await repo.CreateAsync(Apre);
            await repo.SaveAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Aprecieri Apre)
        {

            var repo = _demoService.GetAprecieriRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);
            repo.Update(Apre);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetAprecieriRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }
}
