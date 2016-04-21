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
    public class ContactDetailsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IContactDetailRepository ContactDetailRepository;
        private readonly ILogger logger;

        public ContactDetailsController(
            IContactDetailRepository ContactDetailRepository,
            ILogger logger)
        {
            this.ContactDetailRepository = ContactDetailRepository;
            this.logger = logger;

            this.logger.Trace("Call ContactDetailsController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<ContactDetail> queryOptions)
        {
            logger.Trace("Call ContactDetailsController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = ContactDetailRepository.GetAll();

            var query = (IQueryable<ContactDetail>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ContactDetailsController Get by Id");

            var query = ContactDetailRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] ContactDetail entity)
        {
            logger.Trace("Call ContactDetailsController Post");

            var record = ContactDetailRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] ContactDetail entity)
        {
            logger.Trace("Call ContactDetailsController Put");

            var record = ContactDetailRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ContactDetailsController Delete");

            ContactDetailRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ContactDetailsController");

            base.Dispose(disposing);
        }
    }
}
