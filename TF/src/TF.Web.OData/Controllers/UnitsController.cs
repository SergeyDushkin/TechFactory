using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Business;

namespace TF.Web.API.Controllers
{
    public class UnitsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IUnitRepository unitRepository;
        private readonly ILogger logger;

        public UnitsController(
            IUnitRepository unitRepository,
            ILogger logger)
        {
            this.unitRepository = unitRepository;
            this.logger = logger;

            this.logger.Trace("Call UnitController");
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Unit> queryOptions)
        {
            logger.Trace("Call UnitController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = await unitRepository.GetAllAsync();

            var query = (IQueryable<Unit>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UnitController Get by Id");

            var query = await unitRepository.GetByIdAsync(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Unit entity)
        {
            logger.Trace("Call UnitController Post");

            var record = await unitRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Unit entity)
        {
            logger.Trace("Call UnitController Put");

            var record = await unitRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UnitController Delete");

            await unitRepository.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End UnitController");

            base.Dispose(disposing);
        }
    }
}
