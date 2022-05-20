using Licenta.Data;
using Licenta.Models.Relations.Many_to_Many;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.AprecieriRepository;
using Licenta.Repositories.Generic_Repository;
using Licenta.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.AprecieriRepository
{
    public class AprecieriRepository : GenericRepository<Aprecieri>, IAprecieriRepository
    {
        public AprecieriRepository(Context context) : base(context)
        {

        }

    

        public List<Aprecieri> GetByUser(Guid id_user)
        {
            return _table.Include(x => x.IdUser).Where(x => x.IdUser.Equals(id_user)).ToList();
        }

        public List<Aprecieri> GetByReteta(Guid id_retetea)
        {
            return _table.Where(x => x.IdReteta.Equals(id_retetea)).ToList();
        }

        public List<Aprecieri> GetByCompositeKey(Guid id_user, Guid id_reteta)
        {
            return _table.Where(x => x.IdReteta.Equals(id_reteta))//.ToList();
                
                .Where(x=> x.IdUser.Equals(id_user)).ToList();
        }



    }
}
