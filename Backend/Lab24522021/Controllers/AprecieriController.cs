using Licenta.Models.Relations.Many_to_Many;
using Licenta.Models.Relations.One_to_Many;
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


        [HttpPost("GetByIdLikes")]

        public async Task<IActionResult> GetByIdLikes(GetByIdDTO payload)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(payload.Token);
            var id = jwtSecurityToken.Claims.Where(z => z.Type == "id").FirstOrDefault().Value;
            var result = await _demoService.GetAprecieriRepository().GetAll();
            var toReturn = result.Where(z => { return z.IdUser == Guid.Parse(id) && z.Star == false; }).ToList();
            return Ok(toReturn);

        }

        [HttpPost("GetByIdReviews")]

        public async Task<IActionResult> GetByIdReviews(GetByIdDTO payload)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(payload.Token);
            var id = jwtSecurityToken.Claims.Where(z => z.Type == "id").FirstOrDefault().Value;
            var result = await _demoService.GetAprecieriRepository().GetAll();
            var toReturn = result.Where(z => { return z.IdUser == Guid.Parse(id) && z.Star == true; }).ToList();
            return Ok(toReturn);

        }


        [HttpPost("SubmitReview")]

        public IActionResult SubmitReview(ReviewDTO payload)
        {
            var aprecieriRepo = _demoService.GetAprecieriRepository();
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(payload.Token);
            var id = jwtSecurityToken.Claims.Where(z => z.Type == "id").FirstOrDefault().Value;



            var retetaJoined = _demoService.GetReteteRepository().GetByNume(payload.Name);

            var reteta = _demoService.GetReteteRepository().GetById(retetaJoined.Id);
            Aprecieri ap = _demoService.GetAprecieriRepository().GetByCompositeKey(Guid.Parse(id), reteta.Id).Where(x => x.Star == true).ToList().FirstOrDefault();
            if (ap == null)
            {
                var apreciere = new Aprecieri() { Reteta = reteta, IdUser = Guid.Parse(id), Star = true,
                    Review = payload.Review > 5 ? 5 : payload.Review < 1 ? 1 : payload.Review };
                aprecieriRepo.Create(apreciere);
            }
            else
            {
                ap.Review = payload.Review > 5 ? 5 : payload.Review < 1 ? 1 : payload.Review;

                aprecieriRepo.Update(ap);
                aprecieriRepo.Save();

            }

            var avg = aprecieriRepo.GetByReteta(reteta.Id).Where(x => x.Star).Average(x => x.Review);

            reteta.Rating_retea = (float)avg;
            _demoService.GetReteteRepository().Update(reteta);
            _demoService.GetReteteRepository().Save();

            return Ok();

        }
        [HttpGet("NrLikes")]
        public IActionResult NrLikes(Guid idReteta)
        {
            var aprecieriRepo = _demoService.GetAprecieriRepository();
            var nrLikes = aprecieriRepo.GetByReteta(idReteta).Where(x => !x.Star).Count();

            return Ok(nrLikes);
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

public class ReviewDTO
{
    public string Token;
    public int Review;
    public string Name;
}
public class GetByIdDTO
{
    public string Token;
}