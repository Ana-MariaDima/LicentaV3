using Licenta.Models.Relations.Many_to_Many;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.UnitatiRepository
{
    public interface IUnitatiRepository : IGenericRepository<Unitati>
    {
        Unitati GetByNume(string name);
    }
}
