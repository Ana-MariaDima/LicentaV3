using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using System.Linq;

namespace Licenta.Repositories.PahareRepository
{
    public class PahareRepository : GenericRepository<Pahare>, IPahareRepository
    {
        public PahareRepository(Context context) : base(context)
        {

        }
        public Pahare GetByNume(string name)
        {

            return _table.FirstOrDefault(x => x.Nume_Pahar.ToLower().Equals(name.ToLower()));


        }
    }
}


