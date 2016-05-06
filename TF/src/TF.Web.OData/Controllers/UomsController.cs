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
    public class UomsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IUomRepository uomRepository;
        private readonly ILogger logger;

        public UomsController(
            IUomRepository uomRepository,
            ILogger logger)
        {
            this.uomRepository = uomRepository;
            this.logger = logger;

            this.logger.Trace("Call UomController");
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Uom> queryOptions)
        {
            logger.Trace("Call UomController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = await uomRepository.GetAllAsync();

            var query = (IQueryable<Uom>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UomController Get by Id");

            var query = await uomRepository.GetByIdAsync(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Uom entity)
        {
            logger.Trace("Call UomController Post");

            var record = await uomRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Uom entity)
        {
            logger.Trace("Call UomController Put");

            var record = await uomRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UomController Delete");

            await uomRepository.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End UomController");

            base.Dispose(disposing);
        }
    }
}
