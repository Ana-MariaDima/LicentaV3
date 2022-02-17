using Licenta.Models.Relations.One_to_Many;
using Licenta.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories.SubCategoriiIngredienteRepository
{
    public interface ISubCategoriiIngredienteRepository: IGenericRepository<SubCategoriiIngrediente>
    {
       
        SubCategoriiIngrediente GetByNume(string name);


    }
}
