using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.TipuriReteteRepository
{
    public interface ITipuriReteteRepository : IGenericRepository<TipuriRetete>
    {

        TipuriRetete GetByNume(string name);

        TipuriRetete GetById(string idTip);

    }
}
