using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Systems.Security;

namespace TF.Web.OData.Controllers
{
    public class UsersController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IUserRepository UserRepository;
        private readonly ILogger logger;

        public UsersController(
            IUserRepository UserRepository,
            ILogger logger)
        {
            this.UserRepository = UserRepository;
            this.logger = logger;

            this.logger.Trace("Call UsersController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<User> queryOptions)
        {
            logger.Trace("Call UsersController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = UserRepository.GetAll();

            var query = (IQueryable<User>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UsersController Get by Id");

            var query = UserRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] User entity)
        {
            logger.Trace("Call UsersController Post");

            var record = UserRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] User entity)
        {
            logger.Trace("Call UsersController Put");

            var record = UserRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UsersController Delete");

            UserRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End UsersController");

            base.Dispose(disposing);
        }
    }
}
