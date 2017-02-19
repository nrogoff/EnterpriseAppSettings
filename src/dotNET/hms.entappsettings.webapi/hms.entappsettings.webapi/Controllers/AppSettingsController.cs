using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using hms.entappsettings.contracts;
using hms.entappsettings.repository.Repositories;

namespace hms.entappsettings.webapi.Controllers
{
    /// <summary>
    /// Public Api for getting Enterprise App Settings
    /// </summary>
    [RoutePrefix("api/AppSettings")]
    public class AppSettingsController : ApiController
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;

        public AppSettingsController(IAppSettingRepository appSettingRepository, IMapper mapper)
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
        }

        #region Public Endpoints

        /// <summary>
        /// Returns RESULTANT App Settings for a given Tenant and AppSettingsGroup. NO internal settings published.
        /// </summary>
        /// <param name="tenantId">The Id of the Tenant whose settings to return. This should be hard configured in the internal application config.</param>
        /// <param name="appSettingGroupId">The AppSetting Group Id (i.e. the type of client). This should be hard configured in the internal application config.</param>
        /// <returns></returns>
        [Route("{tenantId:int}/{appSettingGroupId}")]
        [ResponseType(typeof(List<AppSettingDTO>))]
        public IHttpActionResult GetAppSettings([FromUri]int tenantId, [FromUri]int appSettingGroupId)
        {
            var appSettings = _appSettingRepository.GetResultantAppSettings(tenantId, appSettingGroupId, false);
            if (!appSettings.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<AppSettingDTO>>(appSettings));
        }



        #endregion
    }
}
