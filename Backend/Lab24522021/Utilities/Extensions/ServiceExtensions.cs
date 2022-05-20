using Licenta.Repositories.SubCategoriiIngredienteRepository;
using Licenta.Repositories.CategoriiReteteRepository;
using Licenta.Repositories.DatabaseRepository;
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
using Licenta.Repositories.ReteteIngredienteRepository;
using Licenta.Repositories.AprecieriRepository;
using Licenta.Services.AuthService;

namespace Licenta.Utilities.Extensions
{
    public  static class ServiceExtensions
    {

        //vom crea cate o metoda noua pt servicii si repouri

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIngredienteRepository, IngredienteRepository>();
            services.AddScoped<ISubCategoriiIngredienteRepository, SubCategoriiIngredienteRepository>();
            services.AddScoped<ICategoriiIngredienteRepository, CategoriiIngredienteRepository>();
            services.AddScoped<ITipuriReteteRepository, TipuriReteteRepository>();
            services.AddScoped<ICategoriiReteteRepository, CategoriiReteteRepository>();
            services.AddScoped<IPahareRepository, PahareRepository>();
            services.AddScoped<IUnitatiRepository, UnitatiRepository>();
            services.AddScoped<IReteteIngredienteRepository, ReteteIngredienteRepository>();
            services.AddScoped<IReteteRepository, ReteteRepository>();
            services.AddScoped<IAprecieriRepository, AprecieriRepository>();




            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDemoService, DemoService>();
            services.AddTransient<IUserService, UserService>();

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
