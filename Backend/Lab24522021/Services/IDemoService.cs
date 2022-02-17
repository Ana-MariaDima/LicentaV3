using Licenta.Models.DTOs;
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

// in servicii facem operatiile pe care ni le cere clientul, i.e. facem si modificari, nu doar accesari ca in repository

namespace Licenta.Services
{
    public interface IDemoService
    {
        public IIngredienteRepository GetIngredienteRepository();
        public ISubCategoriiIngredienteRepository GetSubCategoriiIngredienteRepository();
        public ICategoriiIngredienteRepository GetCategoriiIngredienteRepository();
        public ITipuriReteteRepository GetTipuriReteteRepository();
        public ICategoriiReteteRepository GetCategoriiReteteRepository();
        public IPahareRepository GetPahareRepository();
        public IUnitatiRepository GetUnitatiRepository();
        public IReteteIngredienteRepository GetReteteIngredienteRepository();
        public IReteteRepository GetReteteRepository();
        IngredienteDTO GetDataMappedByNume(string nume_ingredient);
    }
}
