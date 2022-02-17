using Licenta.Models.DTOs;
using Licenta.Repositories.SubCategoriiIngredienteRepository;
using Licenta.Repositories.CategoriiReteteRepository;
using Licenta.Repositories.DatabaseRepository;
using Licenta.Repositories.ReteteRepository;
using Licenta.Repositories.UnitatiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Repositories.CategoriiIngredienteRepository;
using Licenta.Repositories.TipuriReteteRepository;
using Licenta.Repositories.PahareRepository;
using Licenta.Repositories.ReteteIngredienteRepository;
using Licenta.Repositories.AprecieriRepository;

// in servicii facem operatiile pe care ni le cere clientul, i.e. facem si modificari, nu doar accesari ca in repository

namespace Licenta.Services
{
    public interface IDemoService
    {
        public ISubCategoriiIngredienteRepository GetSubCategoriiIngredienteRepository();
        public ICategoriiIngredienteRepository GetCategoriiIngredienteRepository();
        public IIngredienteRepository GetIngredienteRepository();
        public IReteteIngredienteRepository GetReteteIngredienteRepository();
        public IUnitatiRepository GetUnitatiRepository();

        public IReteteRepository GetReteteRepository();
        public IAprecieriRepository GetAprecieriRepository();
        //public IRepository GetReteteRepository();
        public ITipuriReteteRepository GetTipuriReteteRepository();
        public ICategoriiReteteRepository GetCategoriiReteteRepository();
        public IPahareRepository GetPahareRepository();
       
        IngredienteDTO GetDataMappedByNume(string nume_ingredient);
    }
}
