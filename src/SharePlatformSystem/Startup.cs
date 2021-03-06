﻿using System;
using SharePlatformSystem.Framework.AspNetCore.Mvc.Extensions;
using System.Linq;
using System.Threading;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharePlatformSystem.Auth.EfRepository;
using SharePlatformSystem.Auth.App;
using SharePlatformSystem.Mvc;
using SharePlatformSystem.Framework.AspNetCore;
using System.IO;
using Castle.Facilities.Logging;
using SharePlatformSystem.Log4Net.Logging.Log4Net;
using SharePlatformSystem.Core.PlugIns;
using SharePlatformSystem.Core.Reflection.Extensions;
using SharePlatformSystem.Dependency;
using SharePlatformSystem.Framework.AspNetCore.Configuration;
using Autofac;

namespace SharePlatformSystem
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public IConfigurationRoot Configuration { get; }
        public static readonly AsyncLocal<IocManager> IocManager = new AsyncLocal<IocManager>();
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var mvc = services.AddMvc();
            var builder = new ContainerBuilder();
            


            mvc.PartManager.ApplicationParts.Add(new Microsoft.AspNetCore.Mvc.ApplicationParts.AssemblyPart(typeof(Framework.AspNetCore.Configuration.SharePlatformAspNetCoreConfiguration).GetAssembly()));
            services.AddSingleton(Configuration);
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //关闭GDPR规范
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(option =>
            {
                option.ModelBinderProviders.Insert(0, new JsonBinderProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddOptions();

            services.AddRouting(options => options.LowercaseUrls = false);

            //映射配置文件
            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));

            //在startup里面只能通过这种方式获取到appsettings里面的值，不能用IOptions😰
            var dbType = ((ConfigurationSection)Configuration.GetSection("AppSetting:DbType")).Value;
            if (dbType == Define.DBTYPE_SQLSERVER)
            {
                services.AddDbContext<SharePlatformDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SharePlatformDBContext")));
            }
            else  //mysql
            {
                services.AddDbContext<SharePlatformDBContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("SharePlatformDBContext")));
            }

            return services.AddSharePlatform<Web.SharePlatformSystemWebMvcModule>(
           options =>
           {
              
               options.IocManager = IocManager.Value ?? new IocManager();
               //options.IocManager.Register<Framework.AspNetCore.Configuration.ISharePlatformAspNetCoreConfiguration, Framework.AspNetCore.Configuration.SharePlatformAspNetCoreConfiguration>();

               //options.PlugInSources.Add(
               //    new AssemblyFileListPlugInSource(
               //        Path.Combine(_hostingEnvironment.ContentRootPath, @"bin\Debug\netcoreapp2.2\SharePlatformSystem.Demo.MVC.dll")
               //    )
               //);

               //Configure Log4Net logging
               options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                   f => f.UseSharePlatformLog4Net().WithConfig(_hostingEnvironment.ContentRootPath+"/log4net.config")
               );

               var propInjector = options.IocManager.IocContainer.Kernel.ComponentModelBuilder
                   .Contributors
                   .OfType<Castle.MicroKernel.ModelBuilder.Inspectors.PropertiesDependenciesModelInspector>()
                   .Single();

               options.IocManager.IocContainer.Kernel.ComponentModelBuilder.RemoveContributor(propInjector);
               options.IocManager.IocContainer.Kernel.ComponentModelBuilder.AddContributor(new Dependency.SharePlatformPropertiesDependenciesModelInspector(new Castle.MicroKernel.SubSystems.Conversion.DefaultConversionManager()));
           }
            );

            //使用AutoFac进行注入
           // return new AutofacServiceProvider(AutofacExt.InitAutofac(services));
        }

        public IServiceProvider ConfigureServicess(IServiceCollection services)
        {
            //使用AutoFac进行注入
            return new AutofacServiceProvider(AutofacExt.InitAutofac(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSharePlatform(); //Initializes framework.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseEmbeddedFiles(); //Allows to expose embedded files to the web!

            app.UseMvc(routes =>
            {
            app.ApplicationServices.GetRequiredService<ISharePlatformAspNetCoreConfiguration>().RouteConfiguration.ConfigureAll(routes);
            });
            //app.UseMvc(routes =>
            //{
            //    routes.MapAreaRoute(
            //    name: "DemoMVC",
            //    areaName: "DemoMVC",
            //    template: "DemoMVC/{controller=Blog}/{action=Index}/{id?}");
            //    routes.MapRoute(
            //        name: "defaultWithArea",
            //        template: "{area}/{controller=Home}/{action=Index}/{id?}");

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            //app.UseMvcWithDefaultRoute();

        }
    }
}
