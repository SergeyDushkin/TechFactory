using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
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

            this.logger.Trace("Call CategoryTreesController");
        }

        [EnableQuery]
        public async Task<IHttpActionResult> GetCategories(ODataQueryOptions<Category> queryOptions)
        {
            logger.Trace("Call CategoryTreesController GetCategories");

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

        public async Task<IHttpActionResult> GetGetCategory([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CategoryTreesController GetCategoryTree");

            var query = await service.GetByIdAsync(key);

            return Ok(query);
        }

        public async Task<IHttpActionResult> GetProducts([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CategoryTreesController GetProducts");

            var query = productCategoryService.GetProductsByCategoryId(key);

            return Ok(query);
        }

        public async Task<IHttpActionResult> GetChilds([FromODataUri] System.Guid key, ODataQueryOptions<Category> queryOptions)
        {
            logger.Trace("Call CategoryTreesController GetChilds");

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

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End CategoryTreesController");

            base.Dispose(disposing);
        }
    }
}
