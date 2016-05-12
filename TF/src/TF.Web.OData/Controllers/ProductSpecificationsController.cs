using Microsoft.OData.Core;
using NLog;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Business.WMS;

namespace TF.Web.OData.Controllers
{
    public class ProductSpecificationsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IProductSpecificationRepository productSpecificationRepository;
        private readonly ILogger logger;

        public ProductSpecificationsController(
            IProductSpecificationRepository productSpecificationRepository,
            ILogger logger)
        {
            this.productSpecificationRepository = productSpecificationRepository;
            this.logger = logger;

            this.logger.Trace("Call ProductSpecificationsController");
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductSpecificationsController Get by Id");

            var query = await productSpecificationRepository.GetByIdAsync(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] ProductSpecification entity)
        {
            logger.Trace("Call ProductSpecificationsController Post");

            var record = await productSpecificationRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] ProductSpecification entity)
        {
            logger.Trace("Call ProductSpecificationsController Put");

            var record = await productSpecificationRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductSpecificationsController Delete");

            await productSpecificationRepository.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ProductSpecificationsController");

            base.Dispose(disposing);
        }
    }
}
