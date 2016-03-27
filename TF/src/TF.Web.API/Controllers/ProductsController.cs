﻿using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Business.WMS;

namespace TF.Web.API.Controllers
{
    public class ProductsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IProductService productService;
        private readonly IProductPriceService productPriceService;
        private readonly IProductCategoryService productCategoryService;
        private readonly ILogger logger;

        public ProductsController(
            IProductService productService,
            IProductPriceService productPriceService,
            IProductCategoryService productCategoryService,
            ILogger logger)
        {
            this.productService = productService;
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

            var data = productService.Get();

            var query = (IQueryable<Product>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController GetProduct");

            var query = productService.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Product entity)
        {
            logger.Trace("Call ProductsController Post");

            var record = productService.Create(entity);
            return Created<Product>(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Product entity)
        {
            logger.Trace("Call ProductsController Put");

            var record = productService.Update(entity);
            return Updated<Product>(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductsController Delete");

            productService.Delete(key);
            productPriceService.DeleteByProduct(key);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddPrice([FromODataUri] System.Guid key, [FromBody] ProductPrice entity)
        {
            logger.Trace("Call ProductsController AddPrice");

            entity.ProductId = key;

            var record = productPriceService.Create(entity);
            return Created<ProductPrice>(record);
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

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ProductsController");

            base.Dispose(disposing);
        }
    }
}
