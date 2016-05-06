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
    public class CurrenciesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly ICurrencyRepository CurrencyRepository;
        private readonly ILogger logger;

        public CurrenciesController(
            ICurrencyRepository CurrencyRepository,
            ILogger logger)
        {
            this.CurrencyRepository = CurrencyRepository;
            this.logger = logger;

            this.logger.Trace("Call CurrencyController");
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Currency> queryOptions)
        {
            logger.Trace("Call CurrencyController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = await CurrencyRepository.GetAllAsync();

            var query = (IQueryable<Currency>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CurrencyController Get by Id");

            var query = await CurrencyRepository.GetByIdAsync(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Currency entity)
        {
            logger.Trace("Call CurrencyController Post");

            var record = await CurrencyRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Currency entity)
        {
            logger.Trace("Call CurrencyController Put");

            var record = await CurrencyRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CurrencyController Delete");

            await CurrencyRepository.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End CurrencyController");

            base.Dispose(disposing);
        }
    }
}
