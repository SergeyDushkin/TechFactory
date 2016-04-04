using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Business;

namespace TF.Web.API.Controllers
{
    public class LocationsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly ILocationRepository locationRepository;
        private readonly IUnitRepository unitRepository;
        private readonly ILogger logger;

        public LocationsController(
            IUnitRepository unitRepository,
            ILocationRepository locationRepository,
            ILogger logger)
        {
            this.unitRepository = unitRepository;
            this.locationRepository = locationRepository;
            this.logger = logger;

            this.logger.Trace("Call LocationController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<Location> queryOptions)
        {
            logger.Trace("Call LocationController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = locationRepository.GetAll();

            var query = (IQueryable<Location>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LocationController Get by Id");

            var query = locationRepository.GetById(key);
            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Location entity)
        {
            logger.Trace("Call LocationController Post");

            var record = locationRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Location entity)
        {
            logger.Trace("Call LocationController Put");

            var record = locationRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LocationController Delete");

            locationRepository.Delete(key);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetUnit([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LocationController GetUnit");

            var location = locationRepository.GetById(key);
            if ((location == null) || (!location.UnitId.HasValue))
            {
                return NotFound();
            }

            var unit = unitRepository.GetById(location.UnitId.Value);
            return Ok(unit);
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End LocationController");

            base.Dispose(disposing);
        }
    }
}
