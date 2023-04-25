using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Signalr_API.DataStorage;
using Signalr_API.Hubconfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signalr_API
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

            services.AddSignalR();
                       
            services.AddSingleton<Iinfoservices, infoservices>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Signalr_API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://www.demo.lucky2d.com", "https://www.lucky2d.com", 
                            "https://demo.lucky2d.com", "https://lucky2d.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()
                                .SetIsOriginAllowed((host) => true);
                    });

            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "CorsPolicy",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:51621")
            //                    .AllowAnyHeader()
            //                    .AllowAnyMethod()
            //                    .AllowCredentials()
            //                    .SetIsOriginAllowed((host) => true);
            //        });

            //});

            /* services.AddCors(options => options.AddPolicy("CorsPolicy",
             builder =>
             {
                 builder.WithOrigins("http://localhost:51621", "https://demo.lucky2d.com", "https://lucky2d.com")

                 .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
             }));*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
          // {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Signalr_API v1"));
            // }


            app.UseCors("CorsPolicy");

          /*  app.UseCors(builder =>
            {
                //https://testsignalr.happy6677.com/
                builder.WithOrigins("http://testsignalr.happy6677.com")
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();

            });*/

            //app.UseCors(builder =>
            //{
            //    builder.WithOrigins("http://localhost:4200")
            //    .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            //});

            /* app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());*/


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            }); 
        }
    }
}
