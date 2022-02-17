using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.CategoriiIngredienteRepository
{
    public class CategoriiIngredienteRepository : GenericRepository<CategoriiIngrediente>, ICategoriiIngredienteRepository
    {
        public CategoriiIngredienteRepository(Context context) : base(context)
        {

        }
        public CategoriiIngrediente GetByNume(string name)
        {

            return _table.FirstOrDefault(x => x.Nume_categoriie_ingredient.ToLower().Equals(name.ToLower()));


        }
    }
}
