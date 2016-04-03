using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Systems.Security;

namespace TF.Web.API.Controllers
{
    public class UserIdentitysController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IUserIdentityRepository UserIdentityRepository;
        private readonly ILogger logger;

        public UserIdentitysController(
            IUserIdentityRepository UserIdentityRepository,
            ILogger logger)
        {
            this.UserIdentityRepository = UserIdentityRepository;
            this.logger = logger;

            this.logger.Trace("Call UnitController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<UserIdentity> queryOptions)
        {
            logger.Trace("Call UserIdentitysController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = UserIdentityRepository.GetAll();

            var query = (IQueryable<UserIdentity>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UserIdentitysController Get by Id");

            var query = UserIdentityRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] UserIdentity entity)
        {
            logger.Trace("Call UserIdentitysController Post");

            var record = UserIdentityRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] UserIdentity entity)
        {
            logger.Trace("Call UserIdentitysController Put");

            var record = UserIdentityRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UserIdentitysController Delete");

            UserIdentityRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End UserIdentitysController");

            base.Dispose(disposing);
        }
    }
}
