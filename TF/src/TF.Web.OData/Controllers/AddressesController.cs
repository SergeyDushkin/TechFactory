using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Business;

namespace TF.Web.OData.Controllers
{
    public class AddressesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IAddressRepository AddressRepository;
        private readonly ILogger logger;

        public AddressesController(
            IAddressRepository AddressRepository,
            ILogger logger)
        {
            this.AddressRepository = AddressRepository;
            this.logger = logger;

            this.logger.Trace("Call AddressController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<Address> queryOptions)
        {
            logger.Trace("Call AddressController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = AddressRepository.GetAll();

            var query = (IQueryable<Address>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call AddressController Get by Id");

            var query = AddressRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Address entity)
        {
            logger.Trace("Call AddressController Post");

            var record = AddressRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Address entity)
        {
            logger.Trace("Call AddressController Put");

            var record = AddressRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call AddressController Delete");

            AddressRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End AddressController");

            base.Dispose(disposing);
        }
    }
}
