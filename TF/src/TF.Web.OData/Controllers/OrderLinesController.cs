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
    public class OrderLinesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IOrderLineRepository orderLineRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductPriceService productPriceRepository;
        private readonly IUomRepository uomRepository;
        private readonly ILogger logger;

        public OrderLinesController(
            IOrderRepository orderRepository,
            IOrderLineRepository orderLineRepository,
            IProductRepository productRepository,
            IUomRepository uomRepository,
            IProductPriceService productPriceRepository,
            ILogger logger)
        {
            this.orderRepository = orderRepository;
            this.orderLineRepository = orderLineRepository;
            this.productRepository = productRepository;
            this.productPriceRepository = productPriceRepository;
            this.uomRepository = uomRepository;
            this.logger = logger;

            this.logger.Trace("Call OrderLineController");
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<OrderLine> queryOptions)
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

            var data = await orderLineRepository.GetAllAsync();

            var query = (IQueryable<OrderLine>)queryOptions
                .ApplyTo(data.AsQueryable());

            return Ok(query);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController Get by Id");

            var query = await orderLineRepository.GetByIdAsync(key);
            return Ok(query);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] OrderLine entity)
        {
            logger.Trace("Call OrderLineController Post");

            var price = productPriceRepository.GetByProductId(entity.ItemId);

            if (price != null)
            {
                entity.Price = (float)price.Price;
                entity.Amount = entity.Qty * entity.Price;
            }

            var record = await orderLineRepository.CreateAsync(entity);

            await Task.Factory.StartNew(async () => await RecalcOrderAsync(entity.OrderId));

            return Created(record);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid key, [FromBody] OrderLine entity)
        {
            logger.Trace("Call OrderLineController Put");

            var price = productPriceRepository.GetByProductId(entity.ItemId);

            if (price != null)
            {
                entity.Price = (float)price.Price;
                entity.Amount = entity.Qty * entity.Price;
            }

            var record = await orderLineRepository.UpdateAsync(entity);

            await Task.Factory.StartNew(() => RecalcOrderAsync(entity.OrderId));

            return Updated(record);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController Delete");

            var line = await orderLineRepository.GetByIdAsync(key);

            await orderLineRepository.DeleteAsync(key);

            await Task.Factory.StartNew(() => RecalcOrderAsync(line.OrderId));

            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetOrder([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController GetOrder");

            var orderLine = await orderLineRepository.GetByIdAsync(key);
            if (orderLine == null)
            {
                return NotFound();
            }

            var order = await orderRepository.GetByIdAsync(orderLine.OrderId);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetItem([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController GetItem");

            var orderLine = await orderLineRepository.GetByIdAsync(key);
            if (orderLine == null)
            {
                return NotFound();
            }

            var item = productRepository.GetById(orderLine.ItemId);
            return Ok(item);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUom([FromODataUri] System.Guid key)
        {
            logger.Trace("Call OrderLineController GetUom");

            var orderLine = await orderLineRepository.GetByIdAsync(key);
            if (orderLine == null)
            {
                return NotFound();
            }

            var uom = await uomRepository.GetByIdAsync(orderLine.UomId);
            return Ok(uom);
        }

        private async Task<Order> RecalcOrderByLineIdAsync(Guid id)
        {
            var line = await orderLineRepository.GetByIdAsync(id);
            return await RecalcOrderAsync(line.OrderId);
        }

        private async Task<Order> RecalcOrderAsync(Guid id)
        {
            var order = orderRepository.GetById(id);

            if (order != null)
            {
                var lines = await orderLineRepository.GetByOrderIdAsync(id);

                order.LinesCount = Convert.ToInt16(lines.Count());
                order.Amount = lines.Sum(r => r.Amount);
                order = await orderRepository.UpdateAsync(order);
            }

            return order;
        }

        protected override void Dispose(bool disposing)
        {
            logger.Trace("End OrderLineController");

            base.Dispose(disposing);
        }
    }
}
