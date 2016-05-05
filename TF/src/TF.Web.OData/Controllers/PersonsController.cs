using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Business;

namespace TF.Web.OData.Controllers
{
    public class PersonsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IPersonRepository PersonRepository;
        private readonly ILogger logger;

        public PersonsController(
            IPersonRepository PersonRepository,
            ILogger logger)
        {
            this.PersonRepository = PersonRepository;
            this.logger = logger;

            this.logger.Trace("Call PersonsController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<Person> queryOptions)
        {
            logger.Trace("Call PersonsController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = PersonRepository.GetAll();

            var query = (IQueryable<Person>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call PersonsController Get by Id");

            var query = PersonRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Person entity)
        {
            logger.Trace("Call PersonsController Post");

            var record = PersonRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Person entity)
        {
            logger.Trace("Call PersonsController Put");

            var record = PersonRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call PersonsController Delete");

            PersonRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End PersonsController");

            base.Dispose(disposing);
        }
    }
}
