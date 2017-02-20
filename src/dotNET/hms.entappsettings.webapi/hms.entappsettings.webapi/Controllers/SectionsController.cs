using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hms.entappsettings.repository.Repositories;
using hms.entappsettings.contracts;
using hms.entappsettings.model;

namespace hms.entappsettings.webapi.Controllers
{
    /// <summary>
    /// RESTRICTED API for getting and managing Enterprise App Setting Sections
    /// </summary>
    [RoutePrefix("api/AppSettingSections")]
    public class AppSettingSectionsController : ApiController
    {
        private readonly IAppSettingSectionRepository _appSectionRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSectionRepository"></param>
        /// <param name="mapper"></param>
        public AppSettingSectionsController(IAppSettingSectionRepository appSectionRepository, IMapper mapper)
        {
            _appSectionRepository = appSectionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all Sections
        /// </summary>
        /// <returns></returns>
        /// [Authorize]
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<AppSettingSectionDTO>))]
        public IHttpActionResult GetAppSettingSection()
        {
            try
            {
                return Ok(_appSectionRepository.GetAll().ProjectTo<AppSettingSectionDTO>(_mapper.ConfigurationProvider));
            }
            catch (Exception e)
            {
                return  InternalServerError(e);
            }
        }

        /// <summary>
        /// Get App Setting Section details by id
        /// </summary>
        /// <param name="appSettingSectionId">App Setting Section Id</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("{appSettingSectionId}")]
        [ResponseType(typeof(AppSettingSectionDTO))]
        public IHttpActionResult GetAppSettingSection([FromUri] int appSettingSectionId)
        {
            AppSettingSection appSettingSection = _appSectionRepository.GetById(appSettingSectionId);
            if (appSettingSection == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<AppSettingSectionDTO>(appSettingSection);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing App Setting Section
        /// </summary>
        /// <param name="appSettingSectionId">App Setting Section Id</param>
        /// <param name="appSettingSectionDTO">App Setting Section DTO</param>
        /// <returns></returns>
        /// <response code="204">OK. No content</response>
        /// <response code="400">Bad Request: Id does not match the body</response>
        /// <response code="404">Not Found: AppSettingSection Id not found</response>
        [Route("{appSettingSectionId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppSettingSection(int appSettingSectionId, AppSettingSectionDTO appSettingSectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (appSettingSectionId != appSettingSectionDTO.AppSettingSectionId)
            {
                return BadRequest();
            }
            if (!_appSectionRepository.Exists(appSettingSectionId))
            {
                return NotFound();
            }
            try
            {
                _appSectionRepository.Update(_mapper.Map<AppSettingSection>(appSettingSectionDTO));
                _appSectionRepository.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        /// <summary>
        /// Add an App Setting Section
        /// </summary>
        /// <param name="appSettingSectionDTO">The new appSettingSection DTO</param>
        /// <returns>The AppSettingSectionDTO as given. The header contains uri to get created resource (and it's Id). see loctaion header value.</returns>
        /// <response code="201">Success, Created. See location header value for URI to newly created resource.</response>
        /// <response code="400">Bad Request: Passed model invalid</response>
        [HttpPost]
        [ResponseType(typeof(AppSettingSectionDTO))]
        public IHttpActionResult PostAppSettingSection(AppSettingSectionDTO appSettingSectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appSettingSection = _mapper.Map<AppSettingSection>(appSettingSectionDTO);

            _appSectionRepository.Add(appSettingSection);

            try
            {
                _appSectionRepository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = appSettingSection.AppSettingSectionId }, appSettingSectionDTO);
        }

        /// <summary>
        /// Delete a AppSettingSection
        /// </summary>
        /// <param name="appSettingSectionId">The App Setting Section Id</param>
        /// <returns>AppSettingSectionDTO of deleted item</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("{appSettingSectionId}")]
        [ResponseType(typeof(AppSettingSectionDTO))]
        public IHttpActionResult DeleteAppSettingSection(int appSettingSectionId)
        {
            var appSettingSection = _appSectionRepository.GetById(appSettingSectionId);
            if (appSettingSection == null)
            {
                return NotFound();
            }
            _appSectionRepository.Delete(appSettingSection);
            _appSectionRepository.SaveChanges();
            return Ok(_mapper.Map<AppSettingSectionDTO>(appSettingSection));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appSectionRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
