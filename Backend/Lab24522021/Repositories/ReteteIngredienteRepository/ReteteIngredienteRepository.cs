using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using Licenta.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.ReteteRepository
{
    public class ReteteIngredienteRepository : GenericRepository<RetetaIngrediente>, IReteteIngredienteRepository
    {
        public ReteteIngredienteRepository(Context context) : base(context)
        {

        }

    

        public RetetaIngrediente GetByIngredient(Guid id_ingredient)
        {
            return _table.Include(x => x.IdIngredient).FirstOrDefault(x => x.IdIngredient.Equals(id_ingredient));
        }

        public RetetaIngrediente GetByReteta(Guid id_retetea)
        {
            return _table.Include(x => x.IdReteta).FirstOrDefault(x => x.IdReteta.Equals(id_retetea));
        }

        public RetetaIngrediente GetByUnitate(Guid id_unitate)
        {
            return _table.Include(x => x.IdUnitate).FirstOrDefault(x => x.IdUnitate.Equals(id_unitate));

        }

    }
}
