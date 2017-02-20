using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hms.entappsettings.contracts;
using hms.entappsettings.model;
using hms.entappsettings.repository.Repositories;

namespace hms.entappsettings.webapi.Controllers
{
    /// <summary>
    /// RESTRICTED Api for getting and managing App Setting Groups
    /// </summary>
    [RoutePrefix("api/AppSettingGroups")]
    public class AppSettingGroupsController : ApiController
    {
        private readonly IAppSettingGroupRepository _appSettingGroupRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettingGroupRepository"></param>
        /// <param name="mapper"></param>
        public AppSettingGroupsController(IAppSettingGroupRepository appSettingGroupRepository, IMapper mapper)
        {
            _appSettingGroupRepository = appSettingGroupRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the App Setting Groups
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        //[Authorize]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<AppSettingGroupDTO>))]
        public IEnumerable<AppSettingGroupDTO> GetAppSettingGroups()
        {
            try
            {
                return _appSettingGroupRepository.GetAll().ProjectTo<AppSettingGroupDTO>(_mapper.ConfigurationProvider);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Returns the App Setting Groups details for a single group Id
        /// </summary>
        /// <param name="appSettingGroupId"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("{appSettingGroupId}")]
        [ResponseType(typeof(AppSettingGroupDTO))]
        public IHttpActionResult GetAppSettingGroup([FromUri]int appSettingGroupId)
        {
            AppSettingGroup appSettingGroup = _appSettingGroupRepository.GetById(appSettingGroupId);
            if (appSettingGroup == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<AppSettingGroupDTO>(appSettingGroup);
            return Ok(result);
        }

        /// <summary>
        /// Returns the App Setting Groups details for a given Group Path
        /// </summary>
        /// <param name="groupPath">The groups path in the format '\Core\Grandparent\Parent\Child'. MUST be URL encoded e.g. %5CCore%5CGrandParent%5CParent</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request: Null or missing parameter</response>
        [Route("ByPath")]
        [ResponseType(typeof(AppSettingGroupDTO))]
        public IHttpActionResult GetAppSettingGroupByPath(string groupPath)
        {
            if (groupPath == null)
                return BadRequest($"Parameter '{nameof(groupPath)}' is NULL");
                //throw new ArgumentNullException(nameof(groupPath));

            AppSettingGroup appSettingGroup = _appSettingGroupRepository.GetAll().FirstOrDefault(g => g.GroupPath == groupPath);
            if (appSettingGroup == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<AppSettingGroupDTO>(appSettingGroup);
            return Ok(result);
        }

        /// <summary>
        /// RESTRICTED: Update an existing App Setting Group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appSettingGroupDTO"></param>
        /// <returns></returns>
        /// <response code="204">Success, No Content</response>
        /// <response code="400">Bad Request: Id does not match the body</response>
        /// <response code="404">Not Found: AppSettingGroup Id not found</response>
        [Route("{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppSettingGroup(int id, AppSettingGroupDTO appSettingGroupDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != appSettingGroupDTO.AppSettingGroupId)
            {
                return BadRequest();
            }
            if (!_appSettingGroupRepository.Exists(id))
            {
                return NotFound();
            }

            try
            {
                _appSettingGroupRepository.Update(_mapper.Map<AppSettingGroup>(appSettingGroupDTO));
                _appSettingGroupRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
                
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// RESTRICTED: Add an App Setting Group
        /// </summary>
        /// <param name="appSettingGroupDTO"></param>
        /// <returns>Header contains uri to get created resource. see loctaion header value.</returns>
        /// <response code="201">Success, Created. See location header value for URI to newly created resource.</response>
        /// <response code="400">Bad Request: Passed model invalid</response>
        /// <response code="409">Conflict: Could not save due to a conflict</response>
        [HttpPost]
        [ResponseType(typeof(AppSettingGroupDTO))]
        public IHttpActionResult PostAppSettingGroup(AppSettingGroupDTO appSettingGroupDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appSettingGroup = _mapper.Map<AppSettingGroup>(appSettingGroupDTO);

            _appSettingGroupRepository.Add(appSettingGroup);

            try
            {
                _appSettingGroupRepository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = appSettingGroup.AppSettingGroupId }, appSettingGroupDTO);
        }

        /// <summary>
        /// RESTRICTED: Delete an App Setting Group
        /// </summary>
        /// <param name="appSettingGroupId"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("{appSettingGroupId}")]
        [ResponseType(typeof(AppSettingGroupDTO))]
        public IHttpActionResult DeleteAppSettingGroup(int appSettingGroupId)
        {
            var appSettingGroup = _appSettingGroupRepository.GetById(appSettingGroupId);
            if (appSettingGroup == null)
            {
                return NotFound();
            }
            _appSettingGroupRepository.Delete(appSettingGroup);
            _appSettingGroupRepository.SaveChanges();
            return Ok(_mapper.Map<AppSettingGroupDTO>(appSettingGroup));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appSettingGroupRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}