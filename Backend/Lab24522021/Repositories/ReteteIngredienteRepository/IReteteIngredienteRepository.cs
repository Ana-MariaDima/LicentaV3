using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.ReteteRepository
{
   public interface IReteteIngredienteRepository : IGenericRepository<RetetaIngrediente>
    {
        RetetaIngrediente GetByIngredient(Guid id_ingredient);
        RetetaIngrediente GetByReteta(Guid id_retetea);
        RetetaIngrediente GetByUnitate(Guid id_unitate);

        //Retete GetByIdIncludingRetetaIngredient(int id);
      //  List<Retete> GetAllWithInclude();
        //List<Ingrediente> GetAllWithJoins();-- vezi lab4 pt mai multe detalii



    }
}
