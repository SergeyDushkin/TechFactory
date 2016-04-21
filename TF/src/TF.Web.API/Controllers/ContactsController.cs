using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using TF.Data.Business;

namespace TF.Web.API.Controllers
{
    public class ContactsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IContactRepository ContactRepository;
        private readonly ILogger logger;

        public ContactsController(
            IContactRepository ContactRepository,
            ILogger logger)
        {
            this.ContactRepository = ContactRepository;
            this.logger = logger;

            this.logger.Trace("Call PersonsController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<Contact> queryOptions)
        {
            logger.Trace("Call ContactsController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = ContactRepository.GetAll();

            var query = (IQueryable<Contact>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ContactsController Get by Id");

            var query = ContactRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Contact entity)
        {
            logger.Trace("Call ContactsController Post");

            var record = ContactRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Contact entity)
        {
            logger.Trace("Call ContactsController Put");

            var record = ContactRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ContactsController Delete");

            ContactRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ContactsController");

            base.Dispose(disposing);
        }
    }
}
