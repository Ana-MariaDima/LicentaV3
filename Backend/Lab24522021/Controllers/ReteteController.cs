﻿using Licenta.Models.Relations.One_to_Many;
using Licenta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Models.Relations.Many_to_Many;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Get(GetReteteDTO payload)
        {
            /*
             * id: "3f8758c1-b2b8-438b-8fe5-0ea7a0d46b34"
                instructiuni: "Se dizolvă zahărul în apă fierbinte și se răcește. Coaceți citricele și rupeți-le în pene. Se amestecă vinul, siropul de zahăr, fructele și Fresca într-un ulcior și se pune în frigider timp de câteva ore. Se servește în pahare înalte cu un pai."
                nume_Tip_Retete: "Alcoolic"
                nume_pahar: "Carafă"
                nume_reteta: "Sweet Sangria"
                poza_reteta: "http://www.thecocktaildb.com/images/media/drink/uqqvsp1468924228.jpg"
                rating: 0
                retetaIngredient: Array(7)
                0:
                cantitate: ""
                idIngredient: "41a3fc57-c4e6-4eac-9f09-2e9b2e023843"
                nume_ingredient: "Portocală"
                unitate: "0"
                [[Prototype]]: Object
                1: {nume_ingredient: 'Frișcă', idIngredient: '93969e42-420f-4884-97d0-4315ddb4ceea', unitate: '0', cantitate: ''}
                2: {nume_ingredient: 'Lime', idIngredient: 'ac1b9048-32ef-4539-945b-4b9fd2b5e308', unitate: '0', cantitate: ''}
                3: {nume_ingredient: 'Zahăr', idIngredient: '2295e315-0a72-417e-a42d-9c8ca1bedd70', unitate: 'ml', cantitate: '250'}
                4: {nume_ingredient: 'Vin Rosu', idIngredient: '73ec73d8-ed7c-4173-b84e-d6a9aae8eff9', unitate: 'sticle', cantitate: '2'}
                5: {nume_ingredient: 'Lămâie', idIngredient: 'ea9538d0-08f4-496d-b80f-e45c2934b50b', unitate: '0', cantitate: ''}
                6: {nume_ingredient: 'Măr', idIngredient: 'cbac33c8-7363-4328-adbf-ec4003deeeba', unitate: '0', cantitate: ''}
                length: 7
                [[Prototype]]: Array(0)
                tipReteta:
                nume_Tip_Retete: "Alcoolic"
             */
            List<Retete> retete;
            if (!payload.initialRequest)
            {
                retete = await _demoService.GetReteteRepository().GetAll();
                retete = retete.Skip(100).ToList();
            }
            else
            {
               retete = await _demoService.GetReteteRepository().GetAll();
                retete = retete.Take(100).ToList();

            }
            var ingrediente = await _demoService.GetIngredienteRepository().GetAll();
            var unitati = await _demoService.GetUnitatiRepository().GetAll();
            List<dynamic> rezultate = new List<dynamic>();

            retete.ForEach(reteta =>
            {   var tipR = _demoService.GetTipuriReteteRepository().GetById(reteta.IdTipReteta.ToString());
                var catR = _demoService.GetCategoriiReteteRepository().GetById(reteta.IdCategorieReteta.ToString());
                var pahar = _demoService.GetPahareRepository().GetById(reteta.IdPahar.ToString());
                var tabelaAsociativaIng = _demoService.GetReteteIngredienteRepository().GetByReteta(reteta.Id).ToList();
                List<dynamic> retetaIngrediente = new List<dynamic>();
                var ingredienteReteta = ingrediente.Where(x => tabelaAsociativaIng.Select(z => z.IdIngredient).Contains(x.Id)).ToList();

                ingredienteReteta.ForEach(ing =>
                {
                    var unitate = unitati.Where(z => z.Id.Equals(tabelaAsociativaIng.Where(x => x.IdIngredient.Equals(ing.Id)).FirstOrDefault().IdUnitate));
                    var nume_unitate = unitate.FirstOrDefault() == null ? "" : unitate.FirstOrDefault().Nume_unitate;
                    dynamic retetaIng = new
                    {
                        nume_ingredient = ing.Nume_ingredient,
                        idIngredient = ing.Id.ToString(),
                        unitate =nume_unitate,
                        cantitate = tabelaAsociativaIng.Where(x => x.IdIngredient.Equals(ing.Id)).FirstOrDefault().Cantitate_Ingredient
                    };

                    retetaIngrediente.Add(retetaIng);
                });

                dynamic rezultat = new
                {   id = reteta.Id.ToString(),
                    instructiuni = reteta.Instructiuni_reteta,
                    nume_Tip_Retete = tipR.Nume_Tip_Retete,
                    nume_Categorie_Retete = catR.Nume_Categorie_Retete,
                    nume_pahar = pahar.Nume_Pahar,
                    nume_reteta = reteta.Nume_reteta,
                    poza_reteta = reteta.Poza_reteta,
                    rating = reteta.Rating_retea,
                    retetaIngredient = retetaIngrediente
                  

                };
        
                rezultate.Add(rezultat);
            });

            return Ok(rezultate);


        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {  var retete = await _demoService.GetReteteRepository().GetAll();
            var rnd = new Random();
            var result = retete.ElementAt(rnd.Next(retete.Count-1));
            var reteta = _demoService.GetReteteRepository().GetByNumeJoined(result.Nume_reteta);
            return Ok(reteta);


        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllRet()
        {
            var retete = await _demoService.GetReteteRepository().GetAll();
           
            return Ok(retete);


        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetByIdSimple(Guid id)
        {
           var result = await _demoService.GetReteteRepository().FindByIdAsync(id);
           
            return Ok(result);


        }
        [HttpGet("liked/{id}")]

        public IActionResult GetById(Guid id)
        {
           //var result = await _demoService.GetReteteRepository().FindByIdAsync(id);
           var reteta =   _demoService.GetReteteRepository().GetByIdJoined(id.ToString());
            return Ok(reteta);


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
            
            var reteta = _demoService.GetReteteRepository().GetById(retetaJoined.Id);
            List<Aprecieri> ap = _demoService.GetAprecieriRepository().GetByCompositeKey(Guid.Parse(id), reteta.Id);
            if (ap.Count == 0)
            {
                var apreciere = new Aprecieri() { Reteta = reteta, IdUser = Guid.Parse(id) };
                _demoService.GetAprecieriRepository().Create(apreciere);
            }
            else
            {
                _demoService.GetAprecieriRepository().Delete(ap[0]);
                _demoService.GetAprecieriRepository().Save();
            }
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
    public bool initialRequest;
}


public class LikeDTO
{
    public string Name;
    public string Token;
}