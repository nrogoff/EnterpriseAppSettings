using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using hms.entappsettings.context;
using hms.entappsettings.repository.Repositories;
using Owin;

namespace hms.entappsettings.webapi
{
    /// <summary>
    /// Static class for bootstrapping IoC and other initiator code from Global.asax or Startup.cs
    /// </summary>
    public static class Bootstrapper
    {

        /// <summary>
        /// Implements Autofac IoC container for MVC and WebApi 2. Call from Global.asax
        /// </summary>
        public static void InitiateMvcAndWebApiAutofac()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            //var allOurAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("hms.")).Distinct();

            //Autofac
            var builder = new ContainerBuilder();

            #region MVC Stuff

            // Register your MVC controllers.
            builder.RegisterControllers(thisAssembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(thisAssembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            #endregion

            #region WebAPI

            // Register Web API 2 controllers
            builder.RegisterApiControllers(thisAssembly);

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            #endregion

            #region EF Contexts

            //Register EntAppSettings Context
            builder.RegisterType<EntAppSettingsDbContext>().As<IEntAppSettingsDbContext>();

            #endregion

            #region Repositories

            // Register all EntAppSetting Repoistories
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AppSettingRepository)))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            #endregion

            #region Register Other Types

            //Register other general types needed here


            #endregion

            #region AutoMapper Registration

            var autoMapperProfileTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract));
            var autoMapperProfiles = autoMapperProfileTypes.Select(p => (Profile)Activator.CreateInstance(p));
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().PropertiesAutowired();

            #endregion

            #region Set MVC and WebApi Resolver

            //Build container
            var container = builder.Build();

            // Set the MVC dependency resolver to be Autofac.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Set the Web API 2 dependency resolver to be Autofac
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            #endregion

            #region OWIN Middleware registration

            // OWIN MVC SETUP:

            // Register the Autofac middleware FIRST, then the Autofac MVC middleware.
            //app.UseAutofacMiddleware(container);
            //app.UseAutofacMvc();
            //app.UseAutofacWebApi(config);
            //app.UseWebApi(config);

            #endregion

            #region Debug Stuff

#if DEBUG
            // Check AutoMapper config - will exception if issues
            container.Resolve<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
            var autoMapperTypeMaps = container.Resolve<IMapper>().ConfigurationProvider.GetAllTypeMaps();
            Debug.WriteLine("=== AutoMapper Type Maps ===");
            foreach (var autoMapperTypeMap in autoMapperTypeMaps)
            {
                Debug.WriteLine($"- {autoMapperTypeMap.SourceType.Name} -> {autoMapperTypeMap.DestinationType.Name}");
            }

            var autofacRegistrations = container.ComponentRegistry.Registrations;
            //.SelectMany(x => x.Services)
            //.OfType<IServiceWithType>()
            //.Select(x => x.ServiceType);
            Debug.WriteLine("=== Autofac Registrations ===");
            foreach (var componentRegistration in autofacRegistrations)
            {
                Debug.WriteLine($"- {componentRegistration}");
            }
#endif

            #endregion
        }

//        /// <summary>
//        /// Implements Autofac container for WebApi 2 and OWIN. Call from Startup.
//        /// </summary>
//        /// <param name="app"></param>
//        public static void InitiateWebApiAutofac(IAppBuilder app)
//        {
//            var thisAssembly = Assembly.GetExecutingAssembly();
//            //var allOurAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("hms.")).Distinct();

//            //Autofac
//            var builder = new ContainerBuilder();

//            #region WebAPI

//            // Register Web API 2 controllers
//            builder.RegisterApiControllers(thisAssembly);

//            // Get your HttpConfiguration.
//            var config = GlobalConfiguration.Configuration;

//            // Register your Web API controllers.
//            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

//            // OPTIONAL: Register the Autofac filter provider.
//            builder.RegisterWebApiFilterProvider(config);

//            #endregion

//            #region EF Contexts

//            //Register EntAppSettings Context
//            builder.RegisterType<EntAppSettingsDbContext>().As<IEntAppSettingsDbContext>();

//            #endregion

//            #region Repositories

//            // Register all EntAppSetting Repoistories
//            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AppSettingRepository)))
//                .Where(t => t.Name.EndsWith("Repository"))
//                .AsImplementedInterfaces();

//            #endregion

//            #region Register Other Types

//            //Register other general types needed here


//            #endregion

//            #region AutoMapper Registration

//            var autoMapperProfileTypes = AppDomain.CurrentDomain.GetAssemblies()
//                .SelectMany(a => a.GetTypes().Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract));
//            var autoMapperProfiles = autoMapperProfileTypes.Select(p => (Profile)Activator.CreateInstance(p));
//            builder.Register(ctx => new MapperConfiguration(cfg =>
//            {
//                foreach (var profile in autoMapperProfiles)
//                {
//                    cfg.AddProfile(profile);
//                }
//            }));
//            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().PropertiesAutowired();

//            #endregion

//            #region Set WebApi Resolver

//            // Build the container.
//            var container = builder.Build();

//            // Set the Web API 2 dependency resolver to be Autofac
//            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

//            #endregion

//            #region OWIN Middleware registration

//            // OWIN MVC SETUP:

//            // Register the Autofac middleware FIRST, then the Autofac MVC middleware.
//            app.UseAutofacMiddleware(container);
//            app.UseAutofacMvc();
//            app.UseAutofacWebApi(config);
//            app.UseWebApi(config);

//            #endregion

//            #region Debug Stuff

//#if DEBUG
//            // Check AutoMapper config - will exception if issues
//            container.Resolve<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
//            var autoMapperTypeMaps = container.Resolve<IMapper>().ConfigurationProvider.GetAllTypeMaps();
//            Debug.WriteLine("=== AutoMapper Type Maps ===");
//            foreach (var autoMapperTypeMap in autoMapperTypeMaps)
//            {
//                Debug.WriteLine($"- {autoMapperTypeMap.SourceType.Name} -> {autoMapperTypeMap.DestinationType.Name}");
//            }

//            var autofacRegistrations = container.ComponentRegistry.Registrations;
//            //.SelectMany(x => x.Services)
//            //.OfType<IServiceWithType>()
//            //.Select(x => x.ServiceType);
//            Debug.WriteLine("=== Autofac Registrations ===");
//            foreach (var componentRegistration in autofacRegistrations)
//            {
//                Debug.WriteLine($"- {componentRegistration}");
//            }
//#endif

//            #endregion

//        }
    }
}