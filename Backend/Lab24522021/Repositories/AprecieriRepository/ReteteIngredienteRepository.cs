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

    

        public Aprecieri GetByUser(Guid id_user)
        {
            return _table.Include(x => x.IdUser).FirstOrDefault(x => x.IdUser.Equals(id_user));
        }

        public Aprecieri GetByReteta(Guid id_retetea)
        {
            return _table.Include(x => x.IdReteta).FirstOrDefault(x => x.IdReteta.Equals(id_retetea));
        }

        

    }
}
