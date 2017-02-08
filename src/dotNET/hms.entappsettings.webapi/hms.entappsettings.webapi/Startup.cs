using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using hms.entappsettings.context;
using hms.entappsettings.contracts;
using hms.entappsettings.contracts.Automapper;
using hms.entappsettings.repository.Repositories;
using hms.entappsettings.webapi.Areas.HelpPage.Controllers;
using Owin;

#pragma warning disable 1591
namespace hms.entappsettings.webapi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Initiate WebAPI IoC
            // Bootstrapper.InitiateWebApiAutofac(app);

            ConfigureAuth(app);
        }
    }
}
#pragma warning restore 1591
