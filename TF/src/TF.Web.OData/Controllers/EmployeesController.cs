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
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Employee> queryOptions)
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

            var data = await employeeRepository.GetAllAsync();

            var query = (IQueryable<Employee>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call EmployeesController Get by Id");

            var query = await employeeRepository.GetByIdAsync(key);

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Employee entity)
        {
            logger.Trace("Call EmployeesController Post");

            var record = await employeeRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Employee entity)
        {
            logger.Trace("Call EmployeesController Put");

            var record = await employeeRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call EmployeesController Delete");

            await employeeRepository.DeleteAsync(key);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End EmployeesController");

            base.Dispose(disposing);
        }
    }
}
