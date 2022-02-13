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
        Ingrediente GetByCategorie(Guid categorie_ingredient);
        Ingrediente GetByIdIncludingRetetaIngredient(Guid id);
        List<Ingrediente> GetAllWithInclude();
        //List<Ingrediente> GetAllWithJoins();-- vezi lab4 pt mai multe detalii
     


    }
}
