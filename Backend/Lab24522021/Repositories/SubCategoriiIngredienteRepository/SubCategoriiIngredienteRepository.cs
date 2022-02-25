using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.DatabaseRepository;
using Licenta.Repositories.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.SubCategoriiIngredienteRepository
{
    public class SubCategoriiIngredienteRepository : GenericRepository<SubCategoriiIngrediente>, ISubCategoriiIngredienteRepository
    {
        public SubCategoriiIngredienteRepository(Context context) : base(context)
        {

        }
        public SubCategoriiIngrediente GetByNume(string name)
        {
            
                return _table.FirstOrDefault(x => x.Nume_Subcategoriie_ingredient.ToLower().Equals(name.ToLower()));
            

        }

        public IEnumerable<SubCategoriiIngrediente> GetByCategorieIngrediente(Guid id)
        {
            return _table.Where(x => x.IdCategorieIngredient == id);
        }

    }
}
