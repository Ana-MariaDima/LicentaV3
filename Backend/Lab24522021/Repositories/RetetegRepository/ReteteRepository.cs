using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.ReteteRepository
{
    public class ReteteRepository : GenericRepository<Retete>, IReteteRepository
    {
        public ReteteRepository(Context context) : base(context)
        {

        }
        public void OrderbyName()
        {
            //Linq
            var IngredienteAscName = _table.OrderBy(x => x.Nume_reteta);
            var IngredienteDescName = _table.OrderByDescending(x => x.Nume_reteta);

            //Linq Querry Syntax 
            var IngredienteAscName1 = from s in _table
                                      orderby s.Nume_reteta
                                      select s;
            var IngredienteDescName1 = from s in _table
                                       orderby s.Nume_reteta descending
                                       select s;
        }


        public void OrderbyCategorieIngredient()
        {

            var IngredienteAscCategorieIngredient = _table.OrderBy(x => x.IdCategorieReteta);
            var IngredienteDescCategorieIngredient = _table.OrderByDescending(x => x.IdCategorieReteta);

        }

        public void OrderbyCategorieAndName()
        {

            var IngredienteAscCategorieAndName = _table.OrderBy(x => x.Nume_reteta).ThenBy(x => x.IdCategorieReteta);
            var IngredienteDescCategorieAndName = _table.OrderByDescending(x => x.Nume_reteta).ThenBy(x => x.IdCategorieReteta);

        }

        public void GroupBy()
        {
            //Linq
            var groupRetete = _table.GroupBy(s => s.IdCategorieReteta);

            //creez o lista cu toate id-urile de categorii ingrediente si apoi pt fiecare categgorie de ingrediente parcurg toate ingredientele
            foreach (var reteteGroupByCateg in groupRetete)
            {
                Console.WriteLine("Ingredients gouped by category:" + reteteGroupByCateg.Key);

                foreach (Retete i in reteteGroupByCateg)
                {
                    Console.WriteLine("Reteta Name" + i.Nume_reteta);
                }
            }

          
        }


        public List<Retete> GetAllWithInclude()
        {
            //intorce toate ingredientele +toate intratile aferente din tabela ReteteIngrediente 
            return _table.Include(x => x.RetetaIngredient).ToList();
            //Ingrediente ingredient1{ RetetaIngrediente id1;
            // RetetaIngrediente id2;
            //....}
            //acest lucru este posibil datorita lui Include 
        }
        public async Task<List<Retete>> GetAllWithIncludeAsync()
        {
            return await _table.Include(x => x.RetetaIngredient).ToListAsync();

            // Model1 model1-a
            //  Model2 model2-a
            // Model1 model1-b
            //  Model2 model2-b

            // {... model1-a, { ...model2 a}}, {... model1-b, {..model2-b}}

            // model1-a.model2-a.Id
        }

        public Retete GetByCategorie(Guid categorie_reteta)
        {
            return _table.Include(x => x.IdCategorieReteta).FirstOrDefault(x => x.IdCategorieReteta.Equals(categorie_reteta));
        }

       

        public Retete GetByIdIncludingRetetaIngredient(Guid id)
        {
            return _table.Include(x => x.RetetaIngredient).FirstOrDefault(x => x.Id.Equals(id));
        }

        /* select reteta.Nume_reteta, tipReteta.Nume_Tip_Retete, ingredient.Nume_ingredient from dbo.Reteta reteta 
	join dbo.TipReteta tipReteta on tipReteta.Id = reteta.IdTipReteta
	join dbo.RetetaIngredient retetaIngredient on retetaIngredient.IdReteta = reteta.Id
	join dbo.Ingredient ingredient on ingredient.Id = retetaIngredient.IdIngredient
        */

        public Retete GetById(Guid id)
        {
            return _table.FirstOrDefault(x => x.Id.Equals(id));
        }
        public dynamic GetByNume(string nume_reteta)
        {
            return _table.Join(_context.TipReteta, r => r.IdTipReteta, tr => tr.Id,
                                 (r, tr) => new { r.Id, r.Poza_reteta, r.IdTipReteta, r.IdPahar, idReteta = r.Id, r.Nume_reteta, r.Instructiuni_reteta, r.Rating_retea, tr.Nume_Tip_Retete })
                          .Join(_context.RetetaIngredient, o => o.idReteta, ri => ri.IdReteta,
                                 (o, ri) => new { o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta, o.IdPahar, o.Nume_reteta, o.Nume_Tip_Retete, o.Instructiuni_reteta, o.Rating_retea, ri.IdIngredient, ri.IdUnitate, ri.Cantitate_Ingredient })
                          .Join(_context.Ingredient, o => o.IdIngredient, i => i.Id,
                                 (o, ing) => new { o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta, o.Nume_reteta, o.IdPahar, o.Nume_Tip_Retete, o.Instructiuni_reteta, o.IdIngredient, o.Rating_retea, o.IdUnitate, o.Cantitate_Ingredient, ing.Nume_ingredient })
                          .Join(_context.Pahar, o => o.IdPahar, p => p.Id,
                                (o, pah) => new { o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta, o.Nume_reteta, o.IdPahar, o.Nume_Tip_Retete, o.IdIngredient, o.Instructiuni_reteta, o.IdUnitate, o.Cantitate_Ingredient, o.Rating_retea, o.Nume_ingredient, pah.Nume_Pahar })
                          .Join(_context.Unitate, o => o.IdUnitate, u => u.Id,
                                (o, unit) => new { o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta, o.Nume_reteta, o.IdPahar, o.Nume_Tip_Retete, o.IdIngredient, o.Instructiuni_reteta, o.IdUnitate, o.Cantitate_Ingredient, o.Nume_ingredient, o.Rating_retea, o.Nume_Pahar, unit.Nume_unitate })
                          
                          .ToList()

                                .Where(x => x.Nume_reteta.ToLower().Equals(nume_reteta.ToLower())).FirstOrDefault();
        }



        
        public IEnumerable<object> GetAllJoined(int page, int recordsPerPage)
        {
            return _table.Join(_context.TipReteta, r => r.IdTipReteta, tr => tr.Id,
                                 (r, tr) => new {r.Id, r.Poza_reteta, r.IdTipReteta, r.IdPahar,idReteta = r.Id, r.Nume_reteta, r.Instructiuni_reteta,r.Rating_retea,tr.Nume_Tip_Retete })
                          .Join(_context.RetetaIngredient, o => o.idReteta, ri => ri.IdReteta,
                                 (o, ri) => new {o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta,o.IdPahar, o.Nume_reteta, o.Nume_Tip_Retete, o.Instructiuni_reteta,o.Rating_retea,ri.IdIngredient, ri.IdUnitate, ri.Cantitate_Ingredient})
                          .Join(_context.Ingredient, o => o.IdIngredient, i => i.Id,
                                 (o, ing) => new {o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta, o.Nume_reteta, o.IdPahar, o.Nume_Tip_Retete,o.Instructiuni_reteta, o.IdIngredient, o.Rating_retea, o.IdUnitate, o.Cantitate_Ingredient, ing.Nume_ingredient })
                          .Join(_context.Pahar, o=> o.IdPahar, p=>p.Id,
                                (o, pah)=> new { o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta, o.Nume_reteta, o.IdPahar, o.Nume_Tip_Retete, o.IdIngredient, o.Instructiuni_reteta, o.IdUnitate, o.Cantitate_Ingredient, o.Rating_retea, o.Nume_ingredient, pah.Nume_Pahar })
                          .Join (_context.Unitate, o=>o.IdUnitate, u=>u.Id,
                                (o, unit) => new { o.Id, o.Poza_reteta, o.IdTipReteta, o.idReteta, o.Nume_reteta, o.IdPahar, o.Nume_Tip_Retete, o.IdIngredient, o.Instructiuni_reteta, o.IdUnitate, o.Cantitate_Ingredient, o.Nume_ingredient, o.Rating_retea, o.Nume_Pahar, unit.Nume_unitate })
                          .Skip(page*recordsPerPage)
                          .Take(recordsPerPage)
                           .AsEnumerable<object>();
        }


    }
}
