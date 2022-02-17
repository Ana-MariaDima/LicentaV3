using Licenta.Models.DTOs;
using Licenta.Models.Relations.Many_to_Many;
using Licenta.Repositories.SubCategoriiIngredienteRepository;
using Licenta.Repositories.CategoriiReteteRepository;
using Licenta.Repositories.DatabaseRepository;
using Licenta.Repositories.RetetegRepository;
using Licenta.Repositories.ReteteRepository;
using Licenta.Repositories.UnitatiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Repositories.CategoriiIngredienteRepository;
using Licenta.Repositories.TipuriReteteRepository;
using Licenta.Repositories.PahareRepository;

namespace Licenta.Services
{
    public class DemoService : IDemoService
    {
        public IIngredienteRepository _ingredienteRepository;
        public IReteteIngredienteRepository _reteteIngredienteRepository;
        public ISubCategoriiIngredienteRepository _subCategoriiIngredienteRepository;
        public ICategoriiIngredienteRepository _categoriiIngredienteRepository;
        public ICategoriiReteteRepository _categoriiReteteRepository;
        public ITipuriReteteRepository _tipuriReteteRepository;
        public IPahareRepository _pahareRepository;
        public IUnitatiRepository _unitatiRepository;
        public IReteteRepository _reteteRepository;


        //primim o instanta de dto al ingredientului pe care o putem folosii peste tot in serviciile noastre
        public DemoService(IIngredienteRepository ingredienteRepository,
                            IReteteIngredienteRepository reteteIngredienteRepository,
                            ISubCategoriiIngredienteRepository SubCategoriiIngredienteRepository,
                            ICategoriiIngredienteRepository CategoriiIngredienteRepository,
                            ICategoriiReteteRepository categoriiReteteRepository,
                            IPahareRepository pahareRepository,
                            ITipuriReteteRepository tipuriReteteRepository,
                            IUnitatiRepository unitatiRepository,
                            IReteteRepository reteteRepository)
        {
            _ingredienteRepository = ingredienteRepository;
            _reteteIngredienteRepository = reteteIngredienteRepository;
            _subCategoriiIngredienteRepository = SubCategoriiIngredienteRepository;
            _categoriiIngredienteRepository = CategoriiIngredienteRepository;
            _categoriiReteteRepository = categoriiReteteRepository;
            _pahareRepository = pahareRepository;
            _tipuriReteteRepository = tipuriReteteRepository;
            _unitatiRepository =unitatiRepository;
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
        public ISubCategoriiIngredienteRepository GetSubCategoriiIngredienteRepository()
        {
            return _subCategoriiIngredienteRepository;
        }
        public ICategoriiIngredienteRepository GetCategoriiIngredienteRepository()
        {
            return _categoriiIngredienteRepository;
        }

        public ICategoriiReteteRepository GetCategoriiReteteRepository()
        {
            return _categoriiReteteRepository;
        }
        public ITipuriReteteRepository GetTipuriReteteRepository()
        {
            return _tipuriReteteRepository;
        }
        public IPahareRepository GetPahareRepository()
        {
            return _pahareRepository;
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
