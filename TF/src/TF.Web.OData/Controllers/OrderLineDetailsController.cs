using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using TF.Data.Business;
using TF.Data.Business.WMS;

namespace TF.Web.OData.Controllers
{
    public class OrderLineDetailsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IOrderLineDetailRepository orderLineDetailRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IOrderLineRepository orderLineRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IUomRepository uomRepository;
        private readonly ILogger logger;

        public OrderLineDetailsController(
            IOrderLineDetailRepository orderLineDetailRepository,
            ILocationRepository locationRepository,
            IOrderLineRepository orderLineRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUomRepository uomRepository,
            ILogger logger)
        {
            this.orderLineDetailRepository = orderLineDetailRepository;
            this.locationRepository = locationRepository;
            this.orderLineRepository = orderLineRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.uomRepository = uomRepository;
            this.logger = logger;

            this.logger.Trace("Call OrderLineDetailController");
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<OrderLineDetail> queryOptions)
        {
            logger.Trace("Call OrderLineDetailController Get All");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var data = orderLineDetailRepository.GetAll();

            var query = (IQueryable<OrderLineDetail>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController Get by Id");

            var query = orderLineDetailRepository.GetById(key);
            return Ok(query);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] OrderLineDetail entity)
        {
            logger.Trace("Call OrderLineDetailController Post");

            var record = orderLineDetailRepository.Create(entity);
            return Created(record);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] System.Guid key, [FromBody] OrderLineDetail entity)
        {
            logger.Trace("Call OrderLineDetailController Put");

            var record = orderLineDetailRepository.Update(entity);
            return Updated(record);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController Delete");

            orderLineDetailRepository.Delete(key);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetLocation([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetLocation");

            var orderLineDetail = orderLineDetailRepository.GetById(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var location = locationRepository.GetById(orderLineDetail.LocationId);
            return Ok(location);
        }

        [HttpGet]
        public IHttpActionResult GetOrderLine([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetOrderLine");

            var orderLineDetail = orderLineDetailRepository.GetById(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var orderLine = orderLineRepository.GetById(orderLineDetail.OrderLineId);
            return Ok(orderLine);
        }

        [HttpGet]
        public IHttpActionResult GetOrder([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetOrder");

            var orderLineDetail = orderLineDetailRepository.GetById(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var order = orderRepository.GetById(orderLineDetail.OrderId);
            return Ok(order);
        }

        [HttpGet]
        public IHttpActionResult GetItem([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetItem");

            var orderLineDetail = orderLineDetailRepository.GetById(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var item = productRepository.GetById(orderLineDetail.ItemId);
            return Ok(item);
        }

        [HttpGet]
        public IHttpActionResult GetUom([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetUom");

            var orderLineDetail = orderLineDetailRepository.GetById(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var uom = uomRepository.GetById(orderLineDetail.UomId);
            return Ok(uom);
        }


        protected override void Dispose(bool disposing)
        {
            logger.Trace("End OrderLineDetailController");

            base.Dispose(disposing);
        }
    }
}
