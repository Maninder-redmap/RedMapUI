using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Redmap.Events.Logging;
using Redmap.Events.Repository;
using Redmap.Events.Repository.Interface;
using Redmap.Events.Services;
using Redmap.Events.Services.AutoMapperProfile;
using Redmap.Events.Services.Interface;
using System;

namespace Redmap.Events
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {
        private readonly string siteURL;

        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            siteURL = configuration.GetValue<string>("SiteInfo:Url");
        }
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile(Configuration));
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddCors();
            
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(20);//You can set Time   
            });
            // Add MVC services to the services container.
            services
                .AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Register the Swagger generator, defining 1 or more Swagger documents            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Radmap Events", Version = "v1" });

                c.IncludeXmlComments(string.Format(@"{0}\Redmap.Events.XML", System.AppDomain.CurrentDomain.BaseDirectory));

            });

            // Add the Kendo UI services to the services container.
            services.AddKendo();

            #region services registration            
            services.AddTransient<ILogMessagesService, LogMessagesService>();
            services.AddSingleton<ILogService, LogNLogService>();
            #endregion

            #region repository registration            
            services.AddTransient<ILogMessageRepository, LogMessageRepository>();
            #endregion


        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>       
        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogService logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);
            
            //enabled cor-orig
            app.UseCors(
        options => options.WithOrigins(siteURL).AllowAnyMethod()
    );
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = "swagger";
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthorization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Events}/{action=Index}/{id?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});            
        }
    }
}
