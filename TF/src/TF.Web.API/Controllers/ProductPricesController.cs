using NLog;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Business.WMS;

namespace TF.Web.API.Controllers
{
    public class ProductPricesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IProductPriceService productPriceService;
        private readonly ILogger logger;

        public ProductPricesController(
            IProductPriceService productPriceService,
            ILogger logger)
        {
            this.productPriceService = productPriceService;
            this.logger = logger;

            this.logger.Trace("Call ProductPricesController");
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductPricesController Get");

            var query = productPriceService.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] ProductPrice entity)
        {
            logger.Trace("Call ProductPricesController Post");

            var record = productPriceService.Create(entity);
            return Created<ProductPrice>(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] ProductPrice entity)
        {
            logger.Trace("Call ProductPricesController Put");

            var record = productPriceService.Update(entity);
            return Updated<ProductPrice>(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductPricesController Delete");

            productPriceService.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ProductPricesController");

            base.Dispose(disposing);
        }
    }
}
