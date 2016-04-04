using System;

namespace TF.Data.Business.WMS
{
    public class OrderLineDetail
    {
        public Guid Id { get; set; }
        
        public short Priority { get; set; }
        public float Qty { get; set; }
        public float BaseQty { get; set; }
        public string Number { get; set; }

        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; }

        public Guid OrderLineId { get; set; }
        public virtual OrderLine OrderLine { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Guid ItemId { get; set; }
        public virtual Product Item { get; set; }

        public Guid UomId { get; set; }
        public virtual Uom Uom { get; set; }
    }
}