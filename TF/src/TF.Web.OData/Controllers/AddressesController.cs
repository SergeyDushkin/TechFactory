using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Address> queryOptions)
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

            var data = await AddressRepository.GetAllAsync();

            var query = (IQueryable<Address>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call AddressController Get by Id");

            var query = await AddressRepository.GetByIdAsync(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Address entity)
        {
            logger.Trace("Call AddressController Post");

            var record = await AddressRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Address entity)
        {
            logger.Trace("Call AddressController Put");

            var record = await AddressRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call AddressController Delete");

            await AddressRepository.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End AddressController");

            base.Dispose(disposing);
        }
    }
}
