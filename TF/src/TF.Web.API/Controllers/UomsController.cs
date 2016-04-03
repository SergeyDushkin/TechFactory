using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Business;

namespace TF.Web.API.Controllers
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
        public IHttpActionResult Get(ODataQueryOptions<Uom> queryOptions)
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

            var data = uomRepository.GetAll();

            var query = (IQueryable<Uom>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UomController Get by Id");

            var query = uomRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Uom entity)
        {
            logger.Trace("Call UomController Post");

            var record = uomRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Uom entity)
        {
            logger.Trace("Call UomController Put");

            var record = uomRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UomController Delete");

            uomRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End UomController");

            base.Dispose(disposing);
        }
    }
}
