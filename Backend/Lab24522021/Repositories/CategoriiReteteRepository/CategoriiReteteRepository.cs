using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using System.Linq;

namespace Licenta.Repositories.CategoriiReteteRepository
{
    public class CategoriiReteteRepository : GenericRepository<CategoriiRetete>, ICategoriiReteteRepository
    {
        public CategoriiReteteRepository(Context context) : base(context)
        {

        }
        public CategoriiRetete GetByNume(string name)
        {

            return _table.FirstOrDefault(x => x.Nume_Categorie_Retete.ToLower().Equals(name.ToLower()));


        }
    }
}


 