using NLog;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Systems;

namespace TF.Web.OData.Controllers
{
    public class LinksController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly ILinkRepository linkRepository;
        private readonly ILogger logger;

        public LinksController(
            ILinkRepository linkRepository,
            ILogger logger)
        {
            this.linkRepository = linkRepository;
            this.logger = logger;

            this.logger.Trace("Call LinksController");
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LinksController Get by Id");

            var query = await linkRepository.GetByIdAsync(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Link entity)
        {
            logger.Trace("Call LinksController Post");

            var record = await linkRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Link entity)
        {
            logger.Trace("Call LinksController Put");

            var record = await linkRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call LinksController Delete");

            await linkRepository.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End LinksController");

            base.Dispose(disposing);
        }
    }
}
