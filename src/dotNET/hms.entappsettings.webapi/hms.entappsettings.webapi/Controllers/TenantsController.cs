using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
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
    /// RESTRICTED API for getting and managing Tenants
    /// </summary>
    [RoutePrefix("api/Tenants")]
    public class TenantsController : ApiController
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tenantRepository"></param>
        /// <param name="mapper"></param>
        public TenantsController(ITenantRepository tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all Tenants
        /// </summary>
        /// <returns></returns>
        /// [Authorize]
        public IHttpActionResult GetTenants()
        {
            try
            {
                return Ok(_tenantRepository.GetAll().ProjectTo<TenantDTO>(_mapper.ConfigurationProvider));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Get tenant details by tenantId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(TenantDTO))]
        public IHttpActionResult GetTenant([FromUri] int id)
        {
            Tenant tenant = _tenantRepository.GetById(id);
            if (tenant == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<TenantDTO>(tenant);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing Tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="tenantDTO"></param>
        /// <returns></returns>
        /// <response code="204">OK. No content</response>
        /// <response code="400">Bad Request: Id does not match the body</response>
        /// <response code="404">Not Found: Tenant Id not found</response>
        [Route("{tenantId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTenant(int tenantId, TenantDTO tenantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tenantId != tenantDTO.TenantId)
            {
                return BadRequest();
            }
            if (!_tenantRepository.Exists(tenantId))
            {
                return NotFound();
            }
            try
            {
                _tenantRepository.Update(_mapper.Map<Tenant>(tenantDTO));
                _tenantRepository.SaveChanges();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return InternalServerError(ex);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        /// <summary>
        /// Add an Tenant
        /// </summary>
        /// <param name="tenantDTO">The new tenant DTO</param>
        /// <returns>The Tenant DTO as given. See location header value for URI to newly created resource.</returns>
        /// <response code="201">Success, Created. See location header value for URI to newly created resource.</response>
        /// <response code="400">Bad Request: Passed model invalid</response>
        /// <response code="409">Conflict: Could not save due to a conflict</response>
        [HttpPost]
        [ResponseType(typeof(TenantDTO))]
        public IHttpActionResult PostTenant(TenantDTO tenantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenant = _mapper.Map<Tenant>(tenantDTO);

            _tenantRepository.Add(tenant);

            try
            {
                _tenantRepository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = tenant.TenantId }, tenantDTO);
        }

        /// <summary>
        /// Delete a Tenant
        /// </summary>
        /// <param name="id">The TenantId</param>
        /// <returns>Tenant DTO of deleted item</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(TenantDTO))]
        public IHttpActionResult DeleteTenant(int id)
        {
            var tenant = _tenantRepository.GetById(id);
            if (tenant == null)
            {
                return NotFound();
            }
            _tenantRepository.Delete(tenant);
            _tenantRepository.SaveChanges();
            return Ok(_mapper.Map<TenantDTO>(tenant));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tenantRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
