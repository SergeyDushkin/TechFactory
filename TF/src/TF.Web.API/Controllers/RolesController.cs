using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Systems.Security;

namespace TF.Web.API.Controllers
{
    public class RolesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IRoleRepository RoleRepository;
        private readonly ILogger logger;

        public RolesController(
            IRoleRepository RoleRepository,
            ILogger logger)
        {
            this.RoleRepository = RoleRepository;
            this.logger = logger;

            this.logger.Trace("Call RolesController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<Role> queryOptions)
        {
            logger.Trace("Call RolesController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = RoleRepository.GetAll();

            var query = (IQueryable<Role>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call RolesController Get by Id");

            var query = RoleRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Role entity)
        {
            logger.Trace("Call RolesController Post");

            var record = RoleRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Role entity)
        {
            logger.Trace("Call RolesController Put");

            var record = RoleRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call RolesController Delete");

            RoleRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End RolesController");

            base.Dispose(disposing);
        }
    }
}
