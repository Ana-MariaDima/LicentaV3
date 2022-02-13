using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.RetetegRepository
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

        public Retete GetById(int id)
        {
            return _table.FirstOrDefault(x => x.Id.Equals(id));
        }

        public Retete GetByIdIncludingRetetaIngredient(Guid id)
        {
            return _table.Include(x => x.RetetaIngredient).FirstOrDefault(x => x.Id.Equals(id));
        }

        public Retete GetByNume(string nume_reteta)
        {
            return _table.FirstOrDefault(x => x.Nume_reteta.ToLower().Equals(nume_reteta.ToLower()));
        }

    }
}
