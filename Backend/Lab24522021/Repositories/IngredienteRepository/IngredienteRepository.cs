using Licenta.Data;
using Licenta.Models.Relations.Many_to_Many;
using Licenta.Repositories.Generic_Repository;
using Licenta.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.DatabaseRepository
{
    public class IngredienteRepository :GenericRepository<Ingrediente>, IIngredienteRepository
    {
        public IngredienteRepository (Context  context): base(context)
        {

        }

        public new Ingrediente GetById(string id)
        {
           return  _context.Ingredient.FromSqlRaw("select * from Ingredient where Id = '" + id + "'").FirstOrDefault();
        }
        public void OrderbyName()
        {
            //Linq
            var IngredienteAscName = _table.OrderBy(x => x.Nume_ingredient);
            var IngredienteDescName = _table.OrderByDescending(x => x.Nume_ingredient);

            //Linq Querry Syntax 
            var IngredienteAscName1 = from s in _table
                                      orderby s.Nume_ingredient
                                      select s;
            var IngredienteDescName1 = from s in _table
                                      orderby s.Nume_ingredient descending
                                      select s;
        }


        public void OrderbyCategorieIngredient()
        {
            
            var IngredienteAscCategorieIngredient = _table.OrderBy(x => x.IdSubCategorieIngredient);
            var IngredienteDescCategorieIngredient = _table.OrderByDescending(x => x.IdSubCategorieIngredient);

        }

        public void OrderbyCategorieAndName()
        {
            
            var IngredienteAscCategorieAndName = _table.OrderBy(x => x.Nume_ingredient).ThenBy(x => x.IdSubCategorieIngredient);
            var IngredienteDescCategorieAndName = _table.OrderByDescending(x => x.Nume_ingredient).ThenBy(x => x.IdSubCategorieIngredient);

        }

        public void GroupBy()
        {
            //Linq
            var groupIngredients = _table.GroupBy(s => s.IdSubCategorieIngredient);

            //creez o lista cu toate id-urile de categorii ingrediente si apoi pt fiecare categgorie de ingrediente parcurg toate ingredientele
            foreach( var ingredGroupByCateg in groupIngredients)
            {
                Console.WriteLine("Ingredients gouped by category:" + ingredGroupByCateg.Key);

                foreach(Ingrediente i in ingredGroupByCateg)
                {
                    Console.WriteLine("Ingredient Name" + i.Nume_ingredient);
                }
            }

            //2: mere, pere, banane
            //3: vin rosu, vin alb, ...
        }

        
        public List<Ingrediente> GetAllWithInclude()
        {
            //intorce toate ingredientele +toate intratile aferente din tabela ReteteIngrediente 
            return _table.Where(x=>true).ToList();
            //Ingrediente ingredient1{ RetetaIngrediente id1;
                                        // RetetaIngrediente id2;
                                        //....}
           //acest lucru este posibil datorita lui Include 
        }
        public async Task <List<Ingrediente> >GetAllWithIncludeAsync()
        {
            return await _table.Where(x=>true).ToListAsync();

            // Model1 model1-a
            //  Model2 model2-a
            // Model1 model1-b
            //  Model2 model2-b

            // {... model1-a, { ...model2 a}}, {... model1-b, {..model2-b}}

            // model1-a.model2-a.Id
        }

        public IEnumerable<Ingrediente> GetBySubCategorieIngrediente(Guid id)
        {
            return _table.Where(x => x.IdSubCategorieIngredient == id);
        }
        public Ingrediente GetById(int id)
        {
            return _table.FirstOrDefault(x => x.Id.Equals(id));
        }

       /* public Ingrediente GetByIdIncludingRetetaIngredient(Guid id)
        {
            return _table.Include(x => x.RetetaIngredient).FirstOrDefault(x => x.Id.Equals(id));
        }*/

        public Ingrediente GetByNume(string nume_ingredient)
        {
            return _table.FirstOrDefault(x => x.Nume_ingredient.ToLower().Equals(nume_ingredient.ToLower()));
        }

        
    }

    
}
