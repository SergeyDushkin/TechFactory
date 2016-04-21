using Microsoft.Data.OData;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using TF.Data.Business;
using TF.Data.Business.WMS;

namespace TF.Web.API.Controllers
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
        public IHttpActionResult Get(ODataQueryOptions<Order> queryOptions)
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

            var data = orderRepository.GetAll();

            var query = (IQueryable<Order>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 5)]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController Get by Id");

            var query = orderRepository.GetById(key);

            var lineQuery = orderLineRepository.GetByOrderId(key).Select(r =>
            {
                r.Item = productRepository.GetById(r.ItemId);
                return r;
            });

            query.Lines = lineQuery.ToList();

            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Order entity)
        {
            logger.Trace("Call OrderController Post");

            var record = orderRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] Order entity)
        {
            logger.Trace("Call OrderController Put");

            var record = orderRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController Delete");

            orderRepository.Delete(key);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetCustomer([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetCustomer");

            var order = orderRepository.GetById(key);
            if (order == null)
            {
                return NotFound();
            }

            var customer = unitRepository.GetById(order.CustomerId);
            return Ok(customer);
        }

        [HttpGet]
        public IHttpActionResult GetSource([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetSource");

            var order = orderRepository.GetById(key);
            if (order == null)
            {
                return NotFound();
            }

            var source = locationRepository.GetById(order.SourceId);
            return Ok(source);
        }

        [HttpGet]
        public IHttpActionResult GetDestination([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetDestination");

            var order = orderRepository.GetById(key);
            if (order == null)
            {
                return NotFound();
            }

            var destination = locationRepository.GetById(order.DestinationId);
            return Ok(destination);
        }

        [HttpGet]
        public IHttpActionResult GetCurrency([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetCurrency");

            var order = orderRepository.GetById(key);
            if (order == null)
            {
                return NotFound();
            }

            var currency = currencyRepository.GetById(order.CurrencyId);
            return Ok(currency);
        }

        [HttpGet]
        public IHttpActionResult GetLines([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderController GetLines");

            var lines = orderLineRepository.GetByOrderId(key);
            if ((lines == null) || (lines.Count() == 0))
            {
                return NotFound();
            }

            return Ok(lines);
        }

        [HttpPost]
        public IHttpActionResult AddLine([FromODataUri] System.Guid key, [FromBody] OrderLine entity)
        {
            logger.Trace("Call OrderController AddLine");

            entity.OrderId = key;

            var record = orderLineRepository.Create(entity);
            return Created<OrderLine>(record);
        }


        protected override void Dispose(bool disposing)
        {
            logger.Trace("End OrderController");

            base.Dispose(disposing);
        }
    }
}
