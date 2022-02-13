using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.RetetegRepository
{
    public interface IReteteRepository : IGenericRepository<Retete>
    {
        Retete GetByNume(string nume_ingredient);
        Retete GetByCategorie(Guid categorie_reteta);
        Retete GetByIdIncludingRetetaIngredient(Guid id_reteta);
        List<Retete> GetAllWithInclude();
    }
}
