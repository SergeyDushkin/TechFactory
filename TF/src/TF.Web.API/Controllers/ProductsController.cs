using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using TF.Data.Business;
using TF.Data.Business.WMS;
using System.Web.Http.Tracing;

namespace TF.Web.API.Controllers
{
    public class ProductsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IProductRepository productRepository;
        private readonly IProductPriceService productPriceService;
        private readonly IProductCategoryService productCategoryService;
        private readonly ILogger logger;

        public ProductsController(
            IProductRepository productRepository,
            IProductPriceService productPriceService,
            IProductCategoryService productCategoryService
            ,ILogger logger
            )
        {
            this.productRepository = productRepository;
            this.productPriceService = productPriceService;
            this.productCategoryService = productCategoryService;

            this.logger = logger;
            this.logger.Trace("Call ProductsController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<Product> queryOptions)
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

            var data = productRepository.Get();

            data = data.Select(r =>
            {
                r.Price = productPriceService.GetByProductId(r.Id);
                return r;
            });

            var query = queryOptions
                .ApplyTo(data.AsQueryable());

            //data = query.ToList();

            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController GetProductById {0}", key);

            var query = productRepository.GetById(key);

            if (query == null)
                return NotFound();

            query.Price = productPriceService.GetByProductId(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Product entity)
        {
            logger.Trace("Call ProductsController Post");

            var record = productRepository.Create(entity);
            return Created<Product>(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Product entity)
        {
            logger.Trace("Call ProductsController Put");

            var record = productRepository.Update(entity);
            return Updated<Product>(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController Delete");

            productRepository.Delete(key);
            productPriceService.DeleteByProduct(key);

            return Ok();
        }

        [HttpPost]
        [ODataRoute("Products({key})/Price")]
        public IHttpActionResult CreatePrice([FromODataUri] System.Guid key, [FromBody] ProductPrice entity)
        {
            logger.Trace("Call ProductsController PostPrice");

            entity.ProductId = key;

            var record = productPriceService.Create(entity);
            return Created(record);
        }

        [HttpPut]
        [ODataRoute("Products({key})/Price")]
        public IHttpActionResult UpdatePrice([FromODataUri] System.Guid key, [FromBody] ProductPrice entity)
        {
            logger.Trace("Call ProductsController PutPrice");

            entity.ProductId = key;

            var record = productPriceService.Update(entity);
            return Updated(record);
        }

        [HttpGet]
        public IHttpActionResult GetPrice([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController GetPrice");

            var query = productPriceService.GetByProductId(key);

            if (query != null)
                return Ok(query);

            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetCategories([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController GetCategories");

            var query = productCategoryService.GetCategoriesByProductId(key);

            if (query != null)
                return Ok(query);

            return NotFound();
        }

        [HttpGet]
        [ODataRoute("Products({key})/Categories({relatedKey})")]
        public IHttpActionResult GetCategoryByRelatedKey([FromODataUri] System.Guid key, [FromODataUri] System.Guid relatedKey)
        {
            logger.Trace("Call ProductsController GetCategory");

            var query = productCategoryService
                .GetCategoriesByProductId(key)
                .Where(r => r.Id == relatedKey);

            if (query != null)
                return Ok(query);

            return NotFound();
        }

        [HttpPost]
        [ODataRoute("Products({key})/Categories")]
        public IHttpActionResult AddToCategories([FromODataUri] System.Guid key, [FromBody] Category entity)
        {
            logger.Trace("Call ProductsController PostToCategories");

            //var record = productCategoryService.Create(entity);
            //return Created(record);

            return Ok();
        }

        [HttpPut]
        [ODataRoute("Products({key})/Categories({relatedKey})")]
        public IHttpActionResult UpdateCategories([FromODataUri] System.Guid key, [FromODataUri] System.Guid relatedKey, Category entity)
        {
            logger.Trace("Call ProductsController PutToCategories");

            //var record = productCategoryService.Update(entity);
            //return Created(record);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ProductsController");

            base.Dispose(disposing);
        }
    }
}
