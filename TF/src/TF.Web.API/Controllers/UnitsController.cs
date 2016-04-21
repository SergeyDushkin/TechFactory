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
        public IHttpActionResult Get(ODataQueryOptions<Unit> queryOptions)
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

            var data = unitRepository.GetAll();

            var query = (IQueryable<Unit>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UnitController Get by Id");

            var query = unitRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Unit entity)
        {
            logger.Trace("Call UnitController Post");

            var record = unitRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Unit entity)
        {
            logger.Trace("Call UnitController Put");

            var record = unitRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call UnitController Delete");

            unitRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End UnitController");

            base.Dispose(disposing);
        }
    }
}
