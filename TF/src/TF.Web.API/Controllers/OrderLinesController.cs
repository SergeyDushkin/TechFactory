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
    public class OrderLinesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IOrderLineRepository orderLineRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IUomRepository uomRepository;
        private readonly ILogger logger;

        public OrderLinesController(
            IOrderRepository orderRepository,
            IOrderLineRepository orderLineRepository,
            IProductRepository productRepository,
             IUomRepository uomRepository,
            ILogger logger)
        {
            this.orderRepository = orderRepository;
            this.orderLineRepository = orderLineRepository;
            this.productRepository = productRepository;
            this.uomRepository = uomRepository;
            this.logger = logger;

            this.logger.Trace("Call OrderLineController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<OrderLine> queryOptions)
        {
            logger.Trace("Call OrderLineController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = orderLineRepository.GetAll();

            var query = (IQueryable<OrderLine>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController Get by Id");

            var query = orderLineRepository.GetById(key);
            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] OrderLine entity)
        {
            logger.Trace("Call OrderLineController Post");

            var record = orderLineRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] OrderLine entity)
        {
            logger.Trace("Call OrderLineController Put");

            var record = orderLineRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController Delete");

            orderLineRepository.Delete(key);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetOrder([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController GetOrder");

            var orderLine = orderLineRepository.GetById(key);
            if (orderLine == null)
            {
                return NotFound();
            }

            var order = orderRepository.GetById(orderLine.OrderId);
            return Ok(order);
        }

        [HttpGet]
        public IHttpActionResult GetItem([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController GetItem");

            var orderLine = orderLineRepository.GetById(key);
            if (orderLine == null)
            {
                return NotFound();
            }

            var item = productRepository.GetById(orderLine.ItemId);
            return Ok(item);
        }

        [HttpGet]
        public IHttpActionResult GetUom([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController GetUom");

            var orderLine = orderLineRepository.GetById(key);
            if (orderLine == null)
            {
                return NotFound();
            }

            var uom = uomRepository.GetById(orderLine.UomId);
            return Ok(uom);
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End OrderLineController");

            base.Dispose(disposing);
        }
    }
}
