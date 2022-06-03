using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.SugestionRepository
{
    public class SugestionRepository: GenericRepository<SugestionResult>
    {
        public SugestionRepository(Context context) : base(context)
        {

        }
    }

}
