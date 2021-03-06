using Licenta.Models.Relations.Many_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.DatabaseRepository
{
    public interface IIngredienteRepository : IGenericRepository<Ingrediente>
    {
        Ingrediente GetByNume(string nume_ingredient);
     
        IEnumerable<Ingrediente> GetBySubCategorieIngrediente(Guid id);
    
        //List<Ingrediente> GetAllWithJoins();-- vezi lab4 pt mai multe detalii



    }
}
