using Licenta.Models.Relations.Many_to_Many;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.AprecieriRepository
{
   public interface IAprecieriRepository : IGenericRepository<Aprecieri>
    {
        List<Aprecieri> GetByUser (Guid id_user);
        List<Aprecieri> GetByReteta(Guid id_retetea);

        List<Aprecieri> GetByCompositeKey(Guid id_user, Guid id_reteta);

        List<Guid> GetUsersWhichLiked(List<string> retete, Guid idUserExcepted);

    }
}
