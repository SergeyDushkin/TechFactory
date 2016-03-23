using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Business.WMS;

namespace TF.Web.API.Controllers
{
    public class ProductsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IProductService service;
        private readonly IProductPriceService productPriceService;
        private readonly IProductCategoryService productCategoryService;
        private readonly ILogger logger;

        public ProductsController(
            IProductService service,
            IProductPriceService productPriceService,
            IProductCategoryService productCategoryService,
            ILogger logger)
        {
            this.service = service;
            this.productPriceService = productPriceService;
            this.productCategoryService = productCategoryService;
            this.logger = logger;

            this.logger.Trace("Call ProductsController");
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Product> queryOptions)
        {
            logger.Trace("Call ProductsController GetProducts");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = service.Get();

            var query = (IQueryable<Product>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController GetProduct");

            var query = service.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Product entity)
        {
            logger.Trace("Call ProductsController Post");

            var record = service.Create(entity);
            return Created<Product>(record);
        }

        /*
        [AcceptVerbs("PATCH")]
        [HttpPatch]
        public async Task<IHttpActionResult> Patch([FromODataUri] System.Guid key, Delta<Product> product)
        {
            logger.Trace("Call ProductsController Patch");

            return Ok();
        }
        */

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Product entity)
        {
            logger.Trace("Call ProductsController Put");

            var record = service.Update(entity);
            return Updated<Product>(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController Delete");

            service.Delete(key);
            return Ok();
        }
        /*
        [HttpPost]
        public async Task<IHttpActionResult> AddPrice([FromODataUri] System.Guid key, [FromBody] ProductPrice entity)
        {
            logger.Trace("Call ProductsController AddPrice");

            entity.ProductId = key;

            var record = productPriceService.Create(entity);
            return Created<ProductPrice>(record);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPrices([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController GetPrices");

            var query = productPriceService.GetByProductId(key);

            return Ok(query);
        }
        */
        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ProductsController");

            base.Dispose(disposing);
        }
    }
}
