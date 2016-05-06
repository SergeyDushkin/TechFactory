using Microsoft.OData.Core;
using NLog;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> Get(ODataQueryOptions<OrderLineDetail> queryOptions)
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

            var data = await orderLineDetailRepository.GetAllAsync();

            var query = (IQueryable<OrderLineDetail>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController Get by Id");

            var query = await orderLineDetailRepository.GetByIdAsync(key);
            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] OrderLineDetail entity)
        {
            logger.Trace("Call OrderLineDetailController Post");

            var record = await orderLineDetailRepository.CreateAsync(entity);
            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] OrderLineDetail entity)
        {
            logger.Trace("Call OrderLineDetailController Put");

            var record = await orderLineDetailRepository.UpdateAsync(entity);
            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController Delete");

            await orderLineDetailRepository.DeleteAsync(key);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetLocation([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetLocation");

            var orderLineDetail = await orderLineDetailRepository.GetByIdAsync(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var location = await locationRepository.GetByIdAsync(orderLineDetail.LocationId);
            return Ok(location);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetOrderLine([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetOrderLine");

            var orderLineDetail = await orderLineDetailRepository.GetByIdAsync(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var orderLine = await orderLineRepository.GetByIdAsync(orderLineDetail.OrderLineId);
            return Ok(orderLine);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetOrder([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetOrder");

            var orderLineDetail = await orderLineDetailRepository.GetByIdAsync(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var order = await orderRepository.GetByIdAsync(orderLineDetail.OrderId);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetItem([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetItem");

            var orderLineDetail = await orderLineDetailRepository.GetByIdAsync(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var item = productRepository.GetById(orderLineDetail.ItemId);
            return Ok(item);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUom([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineDetailController GetUom");

            var orderLineDetail = await orderLineDetailRepository.GetByIdAsync(key);
            if (orderLineDetail == null)
            {
                return NotFound();
            }

            var uom = await uomRepository.GetByIdAsync(orderLineDetail.UomId);
            return Ok(uom);
        }


        protected override void Dispose(bool disposing)
        {
            logger.Trace("End OrderLineDetailController");

            base.Dispose(disposing);
        }
    }
}
