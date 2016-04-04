using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using TF.Data.Business;

namespace TF.Web.API.Controllers
{
    public class EmployeesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        
        private readonly IEmployeeRepository employeeRepository;
        private readonly ILogger logger;

        public EmployeesController(
            IEmployeeRepository employeeRepository,
            ILogger logger)
        {
            this.employeeRepository = employeeRepository;
            this.logger = logger;

            this.logger.Trace("Call EmployeesController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<Employee> queryOptions)
        {
            logger.Trace("Call EmployeesController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = employeeRepository.GetAll();

            var query = (IQueryable<Employee>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call EmployeesController Get by Id");

            var query = employeeRepository.GetById(key);

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Employee entity)
        {
            logger.Trace("Call EmployeesController Post");

            var record = employeeRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Employee entity)
        {
            logger.Trace("Call EmployeesController Put");

            var record = employeeRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call EmployeesController Delete");

            employeeRepository.Delete(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End EmployeesController");

            base.Dispose(disposing);
        }
    }
}
