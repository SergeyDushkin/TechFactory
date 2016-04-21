using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using TF.Data.Business;

namespace TF.Web.API.Controllers
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
        public IHttpActionResult Get(ODataQueryOptions<Currency> queryOptions)
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

            var data = CurrencyRepository.GetAll();

            var query = (IQueryable<Currency>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CurrencyController Get by Id");

            var query = CurrencyRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Currency entity)
        {
            logger.Trace("Call CurrencyController Post");

            var record = CurrencyRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Currency entity)
        {
            logger.Trace("Call CurrencyController Put");

            var record = CurrencyRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call CurrencyController Delete");

            CurrencyRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End CurrencyController");

            base.Dispose(disposing);
        }
    }
}
