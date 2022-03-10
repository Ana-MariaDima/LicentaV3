using Licenta.Data;
using Licenta.Repositories.DatabaseRepository;
using Licenta.Services;
using Licenta.Services.AuthService;
using Licenta.Utilities;
using Licenta.Utilities.Extensions;
using Licenta.Utilities.JWTutils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta
{
    public class Startup
    {
        public string CorsAllowSpecificOrigin = "http://localhost:4200/";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Licenta", Version = "v1" });
            });

            //services.AddTransient<IIngredienteRepository, IngredienteRepository>();
            // services.AddTransient<IDemoService, DemoService>();
            services.AddRepositories(); //aici voi adauga toate repo-urile
            services.AddServices();//aici toate serviciile
            //cand folosim Transient se creaza de fiecare data cate o instanta noua 
            //mai putem folosi si AddSingleton -- creaza osingura instanta care este folosita peste tot 
            //sau AddScoped --in cadrul unui request se creaza o singura instanta pentru toate injectarile 
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddSpaStaticFiles(Configuration =>
            //{
            //    Configuration.RootPath = "ClientApp/dist";
            //});
            /* services.AddCors(option =>
             {
                 option.AddPolicy(name: CorsAllowSpecificOrigin,
                     builder =>
                     {
                         builder.WithOrigins("https://localhost:4200", "https://localhost:4201") //for dev and prod  (for example)
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials();
                     });
             });*/
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            //Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IJWTUtils, JWTUtils>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)// aici pot adauga seederele )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Licenta v1"));
            }


          //  app.UseCors(CorsAllowSpecificOrigin);
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles",
                EnableDefaultFiles = true
            });
            // app.UseMiddleware<JWTMiddleware>();

            app.UseAuthorization();

            //for frontend
            //app.UseStaticFiles();



            app.UseCors(
              options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "ClientApp";
            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }
    }
}
