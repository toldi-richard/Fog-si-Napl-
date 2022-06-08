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

        // Szolgáltatások hozzáadására...
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
                // Ezekrõl a címekrõl fogad post és put kéréseket, ha máshol fut a kliens
                // ki kell ezeket bõvíteni.
                builder.WithOrigins("http://localhost:8080")
                       .WithOrigins("http://localhost:8100")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            // Swagger elérése: https://localhost:5001/index.html vagy
            // https://localhost:5001

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fogási Napló",
                    Description = "Projektmunka Szegedi SZC Vasvári Pál Gazdasági és Informatikai Technikum: Toldi Richárd, Kovács Réka Fruzsina, Szekeres Dániel",
                    Version = "v1"
                });

                // Dokumentáció megjelenítésére kell, ha nem jelenik meg automatikusan, de a projektben is kell a kikommentelt rész hozzá...
                //var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                //c.IncludeXmlComments(filePath);
            });
        }

        // HTTP kérések beállítására szolgáló metódus...
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Swagger UI elérése: https://localhost:5001/index.html vagy
            // https://localhost:5001

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fogási Napló v1");
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
