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
            return _table.Where(x => x.IdUser.Equals(id_user)).ToList();
        }

        private class GenerateSugestionModel{
            Guid user;
        }

        public List<Guid> GetUsersWhichLiked(List<string> retete, Guid idUserExcepted)
        {
            var inputParam1 = string.Join(",", retete);
            //_context.Database.Query<MyStoredProcResultType>()
                     
            //DbSet <GenerateSugestionModel>
            DbSet<SugestionResult> sugestionResult;
            sugestionResult = _context.Set<SugestionResult>();
            
            return sugestionResult.FromSqlRaw("exec GenerateSugestions {0}, {1}", inputParam1, idUserExcepted).AsEnumerable().Select(x=>x.Id).ToList();
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
