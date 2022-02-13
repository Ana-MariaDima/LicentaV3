using Licenta.Data;
using Licenta.Models.Relations.Many_to_Many;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Licenta.Repositories.UnitatiRepository
{
    public class UnitatiRepository : GenericRepository<Unitati>, IUnitatiRepository
    {
        public UnitatiRepository(Context context) : base(context)
        {

        }
        public Unitati GetByNume(string name)
        {
            return _table.FirstOrDefault(x => x.Nume_unitate.ToLower().Equals(name.ToLower()));
        }
    }
}
