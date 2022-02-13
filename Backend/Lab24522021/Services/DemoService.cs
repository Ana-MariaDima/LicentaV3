using Licenta.Models.DTOs;
using Licenta.Models.Relations.Many_to_Many;
using Licenta.Repositories.CategoriiIngredienteRepository;
using Licenta.Repositories.CategoriiReteteRepository;
using Licenta.Repositories.DatabaseRepository;
using Licenta.Repositories.RetetegRepository;
using Licenta.Repositories.ReteteRepository;
using Licenta.Repositories.UnitatiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class DemoService : IDemoService
    {
        public IIngredienteRepository _ingredienteRepository;
        public IReteteIngredienteRepository _reteteIngredienteRepository;
        public ICategoriiIngredienteRepository _categoriiIngredienteRepository;
        public ICategoriiReteteRepository _categoriiReteteRepository;
        public IUnitatiRepository _unitatiRepository;
        public IReteteRepository _reteteRepository;


        //primim o instanta de dto al ingredientului pe care o putem folosii peste tot in serviciile noastre
        public DemoService(IIngredienteRepository ingredienteRepository,
                            IReteteIngredienteRepository reteteIngredienteRepository,
                            ICategoriiIngredienteRepository categoriiIngredienteRepository, 
                            ICategoriiReteteRepository categoriiReteteRepository,
                            IUnitatiRepository unitatiRepository,
                            IReteteRepository reteteRepository)
        {
            _ingredienteRepository = ingredienteRepository;
            _reteteIngredienteRepository = reteteIngredienteRepository;
            _categoriiIngredienteRepository = categoriiIngredienteRepository;
            _categoriiReteteRepository = categoriiReteteRepository;
            _unitatiRepository=unitatiRepository;
            _reteteRepository = reteteRepository;
        }
        public IIngredienteRepository GetIngredienteRepository()
        {
            return _ingredienteRepository;
        }
        public IReteteIngredienteRepository GetReteteIngredienteRepository()
        {
            return _reteteIngredienteRepository;
        }
        public ICategoriiIngredienteRepository GetCategoriiIngredienteRepository()
        {
            return _categoriiIngredienteRepository;
        }

        public ICategoriiReteteRepository GetCategoriiReteteRepository()
        {
            return _categoriiReteteRepository;
        }
        public IUnitatiRepository GetUnitatiRepository()
        {
            return _unitatiRepository;
        }
        public IReteteRepository GetReteteRepository()
        {
            return _reteteRepository;
        }
        public IngredienteDTO GetDataMappedByNume(string nume_ingredient)
        {
            Ingrediente ingredient = _ingredienteRepository.GetByNume(nume_ingredient);
            IngredienteDTO result = new IngredienteDTO()
            {
                Nume_ingredient = ingredient.Nume_ingredient//,
               // Categorie_ingredient = ingredient.Categorie_ingredient

            };
            return result;

        }
    }
}
