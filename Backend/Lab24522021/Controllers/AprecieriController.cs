using Licenta.Models.Relations.Many_to_Many;
using Licenta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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


        [HttpPost("GetById")]

        public async Task<IActionResult> GetById(GetByIdDTO payload)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(payload.Token);
            var id = jwtSecurityToken.Claims.Where(z => z.Type == "id").FirstOrDefault().Value;
            var result = await _demoService.GetAprecieriRepository().GetAll();
            var toReturn = result.Where(z => { return z.IdUser == Guid.Parse(id); }).ToList();
            return Ok(toReturn);

        }


        //post =create 

        /*[HttpPost("add")]
        public async Task<IActionResult> Add(Aprecieri Apre)
        {
            Apre.Id = Guid.NewGuid();
            var repo = _demoService.GetAprecieriRepository();
            await repo.CreateAsync(Apre);
            await repo.SaveAsync();
            return Ok();
        }
*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<Aprecieri> Aprecieri)
        {
            foreach (var Apre in Aprecieri)
            {
                Apre.Id = Guid.NewGuid();
                var repo = _demoService.GetAprecieriRepository();
                await repo.CreateAsync(Apre);
                await repo.SaveAsync();
            }
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

public class GetByIdDTO
{
    public string Token;
}