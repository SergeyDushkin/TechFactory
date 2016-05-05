using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using TF.Data.Business;
using TF.Data.Business.WMS;

namespace TF.Web.API.Controllers
{
    public class CategoriesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly ICategoryService service;
        private readonly IProductCategoryService productCategoryService;
        private readonly ILogger logger;

        public CategoriesController(
            ICategoryService service,
            IProductCategoryService productCategoryService,
            ILogger logger)
        {
            this.service = service;
            this.productCategoryService = productCategoryService;
            this.logger = logger;

            this.logger.Trace("Call CategoriesController");
        }

        [EnableQuery]
        public async Task<IHttpActionResult> GetCategories(ODataQueryOptions<Category> queryOptions)
        {
            logger.Trace("Call CategoriesController GetCategories");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = await service.GetAllAsync();

            var query = (IQueryable<Category>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        public async Task<IHttpActionResult> GetCategory([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CategoriesController GetCategory by key {0}", key);

            var query = await service.GetByIdAsync(key);

            return Ok(query);
        }

        //public async Task<IHttpActionResult> GetCategoriesByKey([FromODataUri] string key)
        //{
        //    logger.Trace("Call CategoriesController GetCategoriesByKey");

        //    var query = await service.GetAllAsync();
        //    var data = query.SingleOrDefault(r => r.Key == key);

        //    return Ok(data);
        //}

        public IHttpActionResult GetProducts([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CategoriesController GetProducts");

            var query = productCategoryService.GetProductsByCategoryId(key);

            return Ok(query);
        }

        public async Task<IHttpActionResult> GetChilds([FromODataUri] System.Guid key, ODataQueryOptions<Category> queryOptions)
        {
            logger.Trace("Call CategoriesController GetChilds");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = await service.GetByParentIdAsync(key);

            var query = (IQueryable<Category>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Category entity)
        {
            logger.Trace("Call CategoriesController Post");

            var record = await service.CreateAsync(entity);
            return Created<Category>(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Category entity)
        {
            logger.Trace("Call CategoriesController Put");

            var record = await service.UpdateAsync(entity);
            return Updated<Category>(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CategoriesController Delete");

            await service.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End CategoriesController");

            base.Dispose(disposing);
        }
    }
}
