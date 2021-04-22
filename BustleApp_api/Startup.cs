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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using BustleApp_api.Domain.Utilities;
using BustleApp_api.Repository.DatabaseContext;
using BustleApp_api.Repository.Injections;

namespace BustleApp_api
{
    public class Startup
    {
        public IConfigurationRoot ConfigurationSetUp { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            ConfigurationSetUp = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public string ConnectionString => Configuration.GetConnectionString("Default");

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Configurations>(Configuration.GetSection("BustleConfigurations"));
            services.AddDbContext<BustleContext>(opt => { opt.UseSqlServer(ConnectionString); });

            services.AddAuthorization();
            services.AddRepository();
            services.AddControllers();

            //Register Swagger Generator, Defining 1 or more swagger documents
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bustle API",
                    Description = "API services for Bustle APP",
                    TermsOfService = new Uri("https://aerglotechnology.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Aerglo Technology",
                        Email = "info@aerglotechnology.com",
                        Url = new Uri("http://aerglotechnology.com/#contact"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under Bustle APP",
                        Url = new Uri("http://aerglotechnology.com/#contact"),
                    }
                });


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            //Enable middleware to server swagger ui(html,js,css)
            //Specify swagger url
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Bustle API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
