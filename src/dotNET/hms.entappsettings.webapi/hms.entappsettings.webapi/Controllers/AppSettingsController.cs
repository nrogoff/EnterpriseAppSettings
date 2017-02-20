using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using hms.entappsettings.bll;
using hms.entappsettings.contracts;
using hms.entappsettings.model;
using hms.entappsettings.repository.Repositories;

namespace hms.entappsettings.webapi.Controllers
{
    /// <summary>
    /// Api for getting Enterprise App Settings
    /// </summary>
    [RoutePrefix("api/AppSettings")]
    public class AppSettingsController : ApiController
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IAppSettingHandler _appSettingHandler;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettingRepository"></param>
        /// <param name="appSettingHandler"></param>
        /// <param name="mapper"></param>
        public AppSettingsController(IAppSettingRepository appSettingRepository,IAppSettingHandler appSettingHandler, IMapper mapper)
        {
            _appSettingRepository = appSettingRepository;
            _appSettingHandler = appSettingHandler;
            _mapper = mapper;
        }

        #region Public Endpoints

        /// <summary>
        /// RESTRICTED: Gets all App Settings
        /// </summary>
        /// <param name="includeInternals">Include internal falgged settings. Default is false</param>
        /// <param name="skip">default = 0</param>
        /// <param name="take">default = 20</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<AppSettingDTO>))]
        public IHttpActionResult GetAllAppSettings(bool includeInternals = false, int skip = 0, int take = 20)
        {
            var appSettings = _appSettingRepository.GetAllAppSettings().Skip(skip).Take(take);
            if (!appSettings.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<AppSettingDTO>>(appSettings));
        }

        /// <summary>
        /// Returns RESULTANT App Settings for a given Tenant and AppSettingsGroup. NO internal settings published.
        /// </summary>
        /// <param name="tenantId">The Id of the Tenant whose settings to return. This should be hard configured in the internal application config.</param>
        /// <param name="appSettingGroupId">The AppSetting Group Id (i.e. the type of client). This should be hard configured in the internal application config.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("{tenantId:int}/{appSettingGroupId}")]
        [ResponseType(typeof(List<AppSettingDTO>))]
        public IHttpActionResult GetAppSettings([FromUri]int tenantId, [FromUri]int appSettingGroupId)
        {
            var appSettings = _appSettingRepository.GetResultantAppSettings(tenantId, appSettingGroupId, false);
            if (!appSettings.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<AppSettingDTO>>(appSettings));
        }


        /// <summary>
        /// RESTRICTED: Returns App Settings for a given Tenant and App Setting Group with an Overridden flag.
        /// </summary>
        /// <param name="tenantId">The Id of the Tenant whose settings to return. This should be hard configured in the internal application config.</param>
        /// <param name="appSettingGroupId">The AppSetting Group Id (i.e. the type of client). This should be hard configured in the internal application config.</param>
        /// <param name="includeInternals">Include internal settings. Default is false</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [Route("WithOverrideFlag/{tenantId:int}/{appSettingGroupId}")]
        [ResponseType(typeof(List<AppSettingWithOverrideDTO>))]
        public IHttpActionResult GetAppSettingsWithOverride([FromUri]int tenantId, [FromUri]int appSettingGroupId, bool includeInternals = false)
        {
            var appSettingsWithOverrideDTOs = _appSettingHandler.GetAppSettingWithOverrideDtos(tenantId, appSettingGroupId, includeInternals);

            return Ok(appSettingsWithOverrideDTOs);
        }


        /// <summary>
        /// RESTRICTED: Update an existing Tenant
        /// </summary>
        /// <param name="appSettingId"></param>
        /// <param name="appSettingDTO"></param>
        /// <returns></returns>
        /// <response code="204">OK. No content</response>
        /// <response code="400">Bad Request: Id does not match the body</response>
        /// <response code="404">Not Found: Tenant Id not found</response>
        [Route("{appSettingId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppSetting(int appSettingId, AppSettingDTO appSettingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (appSettingDTO.AppSettingId != null && appSettingId != appSettingDTO.AppSettingId.Value)
            {
                return BadRequest();
            }
            if (!_appSettingRepository.Exists(appSettingId))
            {
                return NotFound();
            }
            try
            {
                _appSettingRepository.Update(_mapper.Map<AppSetting>(appSettingDTO));
                _appSettingRepository.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// RESTRICTED: Adds an App Setting
        /// </summary>
        /// <param name="appSettingDTO">A new App Setting</param>
        /// <returns></returns>
        /// <response code="201">Success, Created. See location header value for URI to newly created resource.</response>
        /// <response code="400">Bad Request: Passed model invalid</response>
        /// <response code="409">Conflict: Could not save due to a conflict</response>
        [HttpPost]
        [ResponseType(typeof(AppSettingDTO))]
        public IHttpActionResult PostAppSetting(AppSettingDTO appSettingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appSetting = _mapper.Map<AppSetting>(appSettingDTO);

            _appSettingRepository.Add(appSetting);

            try
            {
                _appSettingRepository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = appSetting.TenantId }, appSettingDTO);
        }

        /// <summary>
        /// RESTRICTED: Delete a Tenant
        /// </summary>
        /// <param name="appSettingId">The App Setting Id</param>
        /// <returns>Tenant DTO of deleted item</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("{appSettingId}")]
        [ResponseType(typeof(AppSettingDTO))]
        public IHttpActionResult DeleteTenant(int appSettingId)
        {
            var appSetting = _appSettingRepository.GetById(appSettingId);
            if (appSetting == null)
            {
                return NotFound();
            }
            _appSettingRepository.Delete(appSetting);
            _appSettingRepository.SaveChanges();
            return Ok(_mapper.Map<AppSettingDTO>(appSetting));
        }


        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appSettingRepository?.Dispose();
                _appSettingHandler?.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
