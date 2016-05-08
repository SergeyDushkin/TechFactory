using Microsoft.OData.Core;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Business;
using TF.Data.Business.WMS;

namespace TF.Web.OData.Controllers
{
    public class OrdersController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IOrderRepository orderRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IUnitRepository unitRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IOrderLineRepository orderLineRepository;
        private readonly IProductRepository productRepository;
        private readonly ILogger logger;

        public OrdersController(
            IUnitRepository unitRepository,
            IOrderRepository orderRepository,
            ILocationRepository locationRepository,
            ICurrencyRepository currencyRepository,
            IOrderLineRepository orderLineRepository,
            IProductRepository productRepository,
            ILogger logger)
        {
            this.orderRepository = orderRepository;
            this.unitRepository = unitRepository;
            this.locationRepository = locationRepository;
            this.currencyRepository = currencyRepository;
            this.orderLineRepository = orderLineRepository;
            this.productRepository = productRepository;
            this.logger = logger;

            this.logger.Trace("Call OrderController");
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Order> queryOptions)
        {
            logger.Trace("Call OrderController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = await orderRepository.GetAllAsync();

            var query = (IQueryable<Order>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 5)]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Order> queryOptions, [FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController Get by Id");

            var query = await orderRepository.GetByIdAsync(key);

            var lineQuery = orderLineRepository.GetByOrderId(key).Select(r =>
            {
                r.Item = productRepository.GetById(r.ItemId);
                return r;
            });

            query.Lines = lineQuery.ToList();

            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Order entity)
        {
            logger.Trace("Call OrderController Post");

            var orders = await orderRepository.GetAllAsync();
            var draft = orders.FirstOrDefault(r => r.StatusCode == "DRAFT");

            if (draft != null)
            {
                return Redirect(new Uri(String.Format("http://partner-web-api-v1.azurewebsites.net/odata/Orders({0})", draft.Id)));
                //var response = Request.CreateResponse(HttpStatusCode.Found);
                //response.Headers.Location = new Uri(String.Format("http://partner-web-api-v1.azurewebsites.net/odata/Orders({0})", draft.Id));
                //return response;

                //return await Get(null, draft.Id);
                //return Ok(draft);
                //return RedirectToRoute("Get", new { key = draft.Id });
            }

            entity.Amount = 0;
            entity.BaseAmount = 0;
            entity.Date = System.DateTime.Now;
            entity.DueDate = System.DateTime.Now.AddHours(1);
            entity.LinesCount = 0;
            entity.Number = "XXX";
            entity.StatusCode = "DRAFT";
            entity.Type = "SO";

            var record = await orderRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] Order entity)
        {
            logger.Trace("Call OrderController Put");

            var order = await orderRepository.GetByIdAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            order.StatusCode = entity.StatusCode;

            var record = await orderRepository.UpdateAsync(order);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController Delete");

            await orderRepository.DeleteAsync(key);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetCustomer");

            var order = await orderRepository.GetByIdAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            var customer = await unitRepository.GetByIdAsync(order.CustomerId);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetSource([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetSource");

            var order = await orderRepository.GetByIdAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            var source = await locationRepository.GetByIdAsync(order.SourceId);
            return Ok(source);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDestination([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetDestination");

            var order = await orderRepository.GetByIdAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            var destination = await locationRepository.GetByIdAsync(order.DestinationId);
            return Ok(destination);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCurrency([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetCurrency");

            var order = await orderRepository.GetByIdAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            var currency = await currencyRepository.GetByIdAsync(order.CurrencyId);
            return Ok(currency);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetLines([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetLines");

            var lines = await orderLineRepository.GetByOrderIdAsync(key);
            if ((lines == null) || (lines.Count() == 0))
            {
                return NotFound();
            }

            return Ok(lines);
        }

        /*
        [HttpPost]
        public IHttpActionResult AddLine([FromODataUri] System.Guid key, [FromBody] OrderLine entity)
        {
            logger.Trace("Call OrderController AddLine");

            entity.OrderId = key;

            var record = orderLineRepository.Create(entity);
            return Created<OrderLine>(record);
        }
        */

        [HttpPost]
        public async Task<IHttpActionResult> Confirm([FromODataUri] System.Guid key, ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var order = await orderRepository.GetByIdAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            if (order.Type != "DRAFT")
            {
                return NotFound();
            }

            order.Type = "NEW";

            var record = await orderRepository.UpdateAsync(order);

            return Ok(record);

            //return StatusCode(HttpStatusCode.NoContent);
        }


        protected override void Dispose(bool disposing)
        {
            logger.Trace("End OrderController");

            base.Dispose(disposing);
        }
    }
}
