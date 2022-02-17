using Licenta.Repositories.SubCategoriiIngredienteRepository;
using Licenta.Repositories.CategoriiReteteRepository;
using Licenta.Repositories.DatabaseRepository;
using Licenta.Repositories.RetetegRepository;
using Licenta.Repositories.ReteteRepository;
using Licenta.Repositories.UnitatiRepository;
using Licenta.Services;
using Licenta.Utilities.Seeders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Repositories.CategoriiIngredienteRepository;
using Licenta.Repositories.TipuriReteteRepository;
using Licenta.Repositories.PahareRepository;

namespace Licenta.Utilities.Extensions
{
    public  static class ServiceExtensions
    {

        //vom crea cate o metoda noua pt servicii si repouri

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IIngredienteRepository, IngredienteRepository>();
            services.AddTransient<ISubCategoriiIngredienteRepository, SubCategoriiIngredienteRepository>();
            services.AddTransient<ICategoriiIngredienteRepository, CategoriiIngredienteRepository>();
            services.AddTransient<ITipuriReteteRepository, TipuriReteteRepository>();
            services.AddTransient<ICategoriiReteteRepository, CategoriiReteteRepository>();
            services.AddTransient<IPahareRepository, PahareRepository>();
            services.AddTransient<IUnitatiRepository, UnitatiRepository>();
            services.AddTransient<IReteteIngredienteRepository, ReteteIngredienteRepository>();
            services.AddTransient<IReteteRepository, ReteteRepository>();



            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDemoService, DemoService>();
            return services;
        }
        public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddTransient<IngredienteSteeders>();
            services.AddTransient<CategorieIngredienteSeeder>();
            return services;
        }
    }
}
