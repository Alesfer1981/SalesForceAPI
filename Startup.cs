using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Viam.SalesForceAPI.Model.Configuration;
using SwaggerOptions = Viam.SalesForceAPI.Contracts.SwaggerOptions;

namespace Viam.SalesForceAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // All endpoints need authorization using our custom authorization filter
                //options.Filters.Add(new CustomAuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            });

            services.Configure<ConfigurationModel>(Configuration.GetSection("Settings"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sales Force API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Viamericas - SalesForce API",
                        Email = "customerservice@viamerica.com",
                        Url = null
                    },
                    Description = "API restfull to integration process between Viamericas and SalesForce"

                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                c.IncludeXmlComments(System.IO.Path.Combine(basePath, "Viam.SalesForceAPI.xml"));
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesForce API integration V1");
            });

            app.UseRouting();
            //app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("No data found!");
            //    });
            //});


            #region Para mostrar pantalla por default en caso de especificar Uri API
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("No data to show");
            });
            #endregion Para mostrar pantalla por default en caso de especificar Uri API
        }

        //private string GetXmlCommentsPath()
        //{
        //    var app = System.AppContext.BaseDirectory;
        //    return System.IO.Path.Combine(app.ApplicationBasePath, "ASPNETCoreSwaggerDemo.xml");
        //}
    }
}
