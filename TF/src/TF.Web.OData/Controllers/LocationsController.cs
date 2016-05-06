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
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Location> queryOptions)
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

            var data = await locationRepository.GetAllAsync();

            var query = (IQueryable<Location>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LocationController Get by Id");

            var query = await locationRepository.GetByIdAsync(key);
            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Location entity)
        {
            logger.Trace("Call LocationController Post");

            var record = await locationRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Location entity)
        {
            logger.Trace("Call LocationController Put");

            var record = await locationRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LocationController Delete");

            await locationRepository.DeleteAsync(key);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUnit([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LocationController GetUnit");

            var location = await locationRepository.GetByIdAsync(key);
            if ((location == null) || (!location.UnitId.HasValue))
            {
                return NotFound();
            }

            var unit = await unitRepository.GetByIdAsync(location.UnitId.Value);
            return Ok(unit);
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End LocationController");

            base.Dispose(disposing);
        }
    }
}
