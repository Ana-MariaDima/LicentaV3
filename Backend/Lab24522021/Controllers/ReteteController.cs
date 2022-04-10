using Licenta.Models.Relations.One_to_Many;
using Licenta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Models.Relations.Many_to_Many;

namespace Licenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReteteController : ControllerBase
    {

        private readonly IDemoService _demoService;

        public ReteteController(IDemoService demoService)
        {

            _demoService = demoService;
        }

        [HttpPost]
        public IActionResult Get(GetReteteDTO payload)
        {
         
            var result =  _demoService.GetReteteRepository().GetAllJoined(payload.Page, payload.RecordsPerPage);
            return Ok(result);


        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {  var retete = await _demoService.GetReteteRepository().GetAll();
            var rnd = new Random();
            var result = retete.ElementAt(rnd.Next(retete.Count-1));
            var reteta = _demoService.GetReteteRepository().GetByNume(result.Nume_reteta);
            return Ok(reteta);


        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _demoService.GetReteteRepository().FindByIdAsync(id);
            return Ok(result);


        }
        //post =create 

        /*[HttpPost("add")]
        public async Task<IActionResult> Add(Retete Ret)
        {
            Ret.Id = Guid.NewGuid();
            var repo = _demoService.GetReteteRepository();
            await repo.CreateAsync(Ret);
            await repo.SaveAsync();
            return Ok();
        }*/
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<Retete> Retete)
        {
            foreach (var Ret in Retete)
            {
                Ret.Id = Guid.NewGuid();
                var repo = _demoService.GetReteteRepository();
                await repo.CreateAsync(Ret);
                await repo.SaveAsync();
            }
            return Ok();
        }
        //
        //aici cred ca e problema 
        [HttpPost("like")]
        public IActionResult Like(LikeDTO payload)
        {
           
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(payload.Token);
            var id = jwtSecurityToken.Claims.Where(z => z.Type == "id").FirstOrDefault().Value;
            var retetaJoined =  _demoService.GetReteteRepository().GetByNume(payload.Name);
            //var reteta = _demoService.GetReteteRepository().GetById(retetaJoined.Id);
            var apreciere = new Aprecieri() { IdReteta = retetaJoined.Id, IdUser = Guid.Parse(id) };
            _demoService.GetAprecieriRepository().Create(apreciere);

            return Ok();
        }

        [HttpPost("addWithGuid")]
        public async Task<IActionResult> AddWithGuid(List<Retete> Retete)
        {
            foreach (var Ret in Retete)
            {
                //Ret.Id = Guid.NewGuid();
                var repo = _demoService.GetReteteRepository();
                await repo.CreateAsync(Ret);
                await repo.SaveAsync();
            }
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Retete Ret)
        {

            var repo = _demoService.GetReteteRepository();
            // var result = await repo.FindByIdAsync(CatIng.Id);
            repo.Update(Ret);
            await repo.SaveAsync();
            return Ok();
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var repo = _demoService.GetReteteRepository();
            var result = await repo.FindByIdAsync(id);
            if (result == null)
                return Ok();
            repo.Delete(result);
            await repo.SaveAsync();
            return Ok();
        }
    }
}


public class GetReteteDTO
{
    public int RecordsPerPage;
    public int Page;
}


public class LikeDTO
{
    public string Name;
    public string Token;
}