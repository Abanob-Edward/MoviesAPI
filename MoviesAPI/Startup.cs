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
using MoviesAPI.Models;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ??? ???? ?????? ??? ???? ????? ???? 
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<APPlicationDBContext>(options =>
                options.UseSqlServer(connectionString)
                );
         //   services.AddCors();
            services.AddControllers();
            //to add GenresServic in project 
            services.AddTransient<IGenresService, GenresServic>();
            //to add MoviesService in project 

            services.AddTransient<IMoviesService, MoviesService>();
            // after install automapper call it in the starup file 
            services.AddAutoMapper(typeof(Program));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "MoviesAPbybob", 
                    Version = "v1" ,
                    Description = "My first API" ,
                    TermsOfService = new Uri(uriString:"https://www.google.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Abanob Edward ",
                        Email = "abanobefwa@gmail.com",
                        Url = new Uri(uriString: "https://www.google.com"),
                    },
                    License = new OpenApiLicense{
                    Name = "my license",
                    Url = new Uri(uriString: "https://www.google.com"),
                    }
                      
                });
                c.AddSecurityDefinition(name: "Bearar", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter Yor JWT After world 'Bearar'"
                });
                // add here requrment 
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearar"
                            },
                            Name = "Bearar" ,
                            In = ParameterLocation.Header
                        },
                        new List<String>()
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            // for security 
            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // this is a cores to filter the function 
            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
