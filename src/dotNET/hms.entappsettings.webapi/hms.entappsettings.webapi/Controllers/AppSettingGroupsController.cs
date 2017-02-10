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
using hms.entappsettings.context;
using hms.entappsettings.contracts;
using hms.entappsettings.model;
using hms.entappsettings.repository.Repositories;

namespace hms.entappsettings.webapi.Controllers
{
    /// <summary>
    /// Public Api for gettinge App Setting Groups
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
        //[Authorize]
        public IQueryable<AppSettingGroupDTO> GetAppSettingGroups()
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
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(AppSettingGroupDTO))]
        public IHttpActionResult GetAppSettingGroup(int id)
        {
            AppSettingGroup appSettingGroup = _appSettingGroupRepository.GetById(id);
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

        //// PUT: api/AppSettingGroups/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAppSettingGroup(int id, AppSettingGroup appSettingGroup)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != appSettingGroup.AppSettingGroupId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(appSettingGroup).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AppSettingGroupExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/AppSettingGroups
        //[ResponseType(typeof(AppSettingGroup))]
        //public IHttpActionResult PostAppSettingGroup(AppSettingGroup appSettingGroup)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AppSettingGroups.Add(appSettingGroup);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = appSettingGroup.AppSettingGroupId }, appSettingGroup);
        //}

        //// DELETE: api/AppSettingGroups/5
        //[ResponseType(typeof(AppSettingGroup))]
        //public IHttpActionResult DeleteAppSettingGroup(int id)
        //{
        //    AppSettingGroup appSettingGroup = db.AppSettingGroups.Find(id);
        //    if (appSettingGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    db.AppSettingGroups.Remove(appSettingGroup);
        //    db.SaveChanges();

        //    return Ok(appSettingGroup);
        //}

        //private bool AppSettingGroupExists(int id)
        //{
        //    return db.AppSettingGroups.Count(e => e.AppSettingGroupId == id) > 0;
        //}

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