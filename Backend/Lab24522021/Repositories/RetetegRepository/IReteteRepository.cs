using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.ReteteRepository
{
    public interface IReteteRepository : IGenericRepository<Retete>
    {
        Retete GetByNume(string nume_ingredient);
        dynamic GetByNumeJoined(string nume_ingredient);

        Retete GetByCategorie(Guid categorie_reteta);
        Retete GetByIdIncludingRetetaIngredient(Guid id_reteta);
        List<Retete> GetAllWithInclude();



        Retete GetById(Guid id);

        IEnumerable<object> GetAllJoined(int page, int recordsPerPage);
    }
}
