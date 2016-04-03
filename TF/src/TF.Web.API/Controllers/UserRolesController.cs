using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Systems.Security;

namespace TF.Web.API.Controllers
{
    public class UserRolesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IUserRoleRepository UserRoleRepository;
        private readonly ILogger logger;

        public UserRolesController(
            IUserRoleRepository UserRoleRepository,
            ILogger logger)
        {
            this.UserRoleRepository = UserRoleRepository;
            this.logger = logger;

            this.logger.Trace("Call UserRolesController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<UserRole> queryOptions)
        {
            logger.Trace("Call UserRolesController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = UserRoleRepository.GetAll();

            var query = (IQueryable<UserRole>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UserRolesController Get by Id");

            var query = UserRoleRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] UserRole entity)
        {
            logger.Trace("Call UserRolesController Post");

            var record = UserRoleRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] UserRole entity)
        {
            logger.Trace("Call UserRolesController Put");

            var record = UserRoleRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UserRolesController Delete");

            UserRoleRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End UsersController");

            base.Dispose(disposing);
        }
    }
}
