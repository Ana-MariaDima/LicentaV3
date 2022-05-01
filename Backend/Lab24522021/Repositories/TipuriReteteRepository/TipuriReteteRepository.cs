using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using System.Linq;

namespace Licenta.Repositories.TipuriReteteRepository
{
    public class TipuriReteteRepository : GenericRepository<TipuriRetete>, ITipuriReteteRepository
    {
        public TipuriReteteRepository(Context context) : base(context)
        {

        }
        public TipuriRetete GetByNume(string name)
        {

            return _table.FirstOrDefault(x => x.Nume_Tip_Retete.ToLower().Equals(name.ToLower()));


        }

        public TipuriRetete GetById(string idTip)
        {
            return _table.FirstOrDefault(x => x.Id.ToString().Equals(idTip));

        }
    }
}


