using Licenta.Models.Relations.Many_to_Many;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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



        private static bool NextCombination(IList<int> num, int n, int k)
        {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--)
            {
                if (num[i] < n - 1 - (k - 1) + i)
                {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }

        private static List<IEnumerable<string>> Combinations(IEnumerable<string> elements, int k)
        {
            var grupari = new List<IEnumerable<string>>();
            var elem = elements.ToArray();
            var size = elem.Length;

            if (k > size) throw new Exception("K > size");

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do
            {
                var listToAdd = numbers.Select(n => elem[n]).ToList();
                var clonedList = new List<string>(listToAdd.Count());
                listToAdd.ForEach(el =>
                {
                    clonedList.Add(el.Clone().ToString());
                });
                //(ICloneable)item.Clone()
               grupari.Add(clonedList);
            } while (NextCombination(numbers, size, k));

            return grupari;
        }

        /*
             *  -- matching mediu
             *  toti userii care au apreciat (1,2) (diff 1 = 3 - 2)
             *  toti userii care au apreciat (1,3)
             *  toti userii care au apreciat (2,3)
             */
        static List<Guid> GetUsersByAprecieri(List<IEnumerable<string>> grupariDeAprecieri, IDemoService _demoService, Guid idUserExcepted)
        {
            var usersToReturn = new List<Guid>();
                
            var aprecieriRepo = _demoService.GetAprecieriRepository();
            grupariDeAprecieri.ForEach(grupare =>
            {

                //usersi care au apreciat gruparea in cazua (in totalitate)
                var users = aprecieriRepo.GetUsersWhichLiked(grupare.ToList(), idUserExcepted).Take(10);
                usersToReturn.AddRange(users);
            });

      
            return usersToReturn;
        }


        public struct rezFinal
        {
            public int scor_matching;
            public List<Guid> retete_recomandate;
        };

        [HttpPost("GeneratePersonalSugestions")]

        public async Task<IActionResult> GeneratePersonalSugestions(GetByIdDTO payload)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(payload.Token);
            var id = jwtSecurityToken.Claims.Where(z => z.Type == "id").FirstOrDefault().Value;

            var aprecieriUserTarget = _demoService.GetAprecieriRepository().GetByUser(Guid.Parse(id)).Where(x=>x.Star==false).Select(x=>x.IdReteta.ToString()).ToList(); // o lista cu id-uri de RETETE
            var colectieAprecieri = await _demoService.GetAprecieriRepository().GetAll(); // o lista cu toate aprecierile din baza

            /*
             * userTarget : (1,2,3)
             * (1) (2) (3)
             * (1,2) (1,3) (2,3)
             * (1,2,3)
             * 
             * (2^n) - 1
             * 
             * cautam useri in db
             *  -- cele mai slabe matchinguri
             *  toti userii care au apreciat (1) (diff 2 = 3 - 1)
             *  toti userii care au apreciat (2)
             *  toti userii care au apreciat (3)
             *  
             *  -- matching mediu
             *  toti userii care au apreciat (1,2) (diff 1 = 3 - 2)
             *  toti userii care au apreciat (1,3)
             *  toti userii care au apreciat (2,3)
             *  
             *  -- perfect matching 
             *  toti userii care au apreciat (1,2,3) (diff 0 = 3 - 3)
             *  
             * */


            var rezultateFinale = new List<Guid>();

            var threshold = 20;
            var thresholdNumarRetete = 100;
            var added = 0;
            threshold = threshold > aprecieriUserTarget.Count? aprecieriUserTarget.Count: threshold;
            for (int diff = 0; diff <= threshold - 1; diff++)
            {
                if (added == thresholdNumarRetete) break;

                int k = aprecieriUserTarget.Count - diff;

                List<IEnumerable<string>> rezultatComb = Combinations(aprecieriUserTarget, k);

                var relatedUsers = GetUsersByAprecieri(rezultatComb, _demoService, Guid.Parse(id));

                var otherLiked = new List<Guid>();
                relatedUsers.ForEach(user =>
                {
                    otherLiked.AddRange(colectieAprecieri.Where(x => relatedUsers.Contains(x.IdUser) && x.Star == false).Where(apreciere => !aprecieriUserTarget.Contains(apreciere.IdReteta.ToString()))
                                .Select(x=>x.IdReteta).Take(15));
                });

                if (otherLiked.Count > 0)
                {
                    var tmp = new List<Guid>();
                    if(otherLiked.Count + added > thresholdNumarRetete){
                        tmp.AddRange(otherLiked.Take(thresholdNumarRetete - added));
                    }
                    else{
                        tmp = otherLiked;
                    }

                    //tmp = tmp.Where(x => !rezultateFinale.Select(x=>x.ToString()).Contains(x.ToString())).ToList();
                    rezultateFinale.AddRange(tmp);
                    rezultateFinale = rezultateFinale.Distinct().ToList();

                    added += tmp.Count;
                }
                
            }

            return Ok(rezultateFinale);

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

            return Ok(new { medie = reteta.Rating_retea });

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