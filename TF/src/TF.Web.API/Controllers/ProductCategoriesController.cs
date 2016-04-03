using NLog;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Business.WMS;

namespace TF.Web.API.Controllers
{
    public class ProductCategoriesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IProductCategoryService productCategoryService;
        private readonly ILogger logger;

        public ProductCategoriesController(
            IProductCategoryService productCategoryService,
            ILogger logger)
        {
            this.productCategoryService = productCategoryService;
            this.logger = logger;

            this.logger.Trace("Call ProductCategoriesController");
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductCategoriesController Get");

            var query = productCategoryService.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] ProductCategory entity)
        {
            logger.Trace("Call ProductCategoriesController Post");

            var record = productCategoryService.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] ProductCategory entity)
        {
            logger.Trace("Call ProductCategoriesController Put");

            var record = productCategoryService.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call ProductCategoriesController Delete");

            productCategoryService.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End ProductCategoriesController");

            base.Dispose(disposing);
        }
    }
}
