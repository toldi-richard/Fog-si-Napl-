using BasicAuth;
using Fogasi_naplo_webapi.Models;
using Fogasi_naplo_webapi.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fogasi_naplo_webapi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Szolg�ltat�sok hozz�ad�s�ra...
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<FogasiNaploContext>(
                o => o.UseMySql(Configuration.GetConnectionString("FogasinaploDB"),
                ServerVersion.Parse("10.4.21-mariadb")));

            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IUserService, FelhasznalokRepository>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                // Ezekr�l a c�mekr�l fogad post �s put k�r�seket, ha m�shol fut a kliens
                // ki kell ezeket b�v�teni.
                builder.WithOrigins("http://localhost:8080")
                       .WithOrigins("http://localhost:8100")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            // Swagger el�r�se: https://localhost:5001/index.html vagy
            // https://localhost:5001

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fog�si Napl�",
                    Description = "Projektmunka Szegedi SZC Vasv�ri P�l Gazdas�gi �s Informatikai Technikum: Toldi Rich�rd, Kov�cs R�ka Fruzsina, Szekeres D�niel",
                    Version = "v1"
                });

                // Dokument�ci� megjelen�t�s�re kell, ha nem jelenik meg automatikusan, de a projektben is kell a kikommentelt r�sz hozz�...
                //var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                //c.IncludeXmlComments(filePath);
            });
        }

        // HTTP k�r�sek be�ll�t�s�ra szolg�l� met�dus...
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Swagger UI el�r�se: https://localhost:5001/index.html vagy
            // https://localhost:5001

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fog�si Napl� v1");
                c.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseCors("MyPolicy");
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
