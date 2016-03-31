using System;

namespace TF.Data.Business.WMS
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public short Priority { get; set; }
        public Guid ItemId { get; set; }
        public Guid UomId { get; set; }
        public float Qty { get; set; }
        public float BaseQty { get; set; }
        public float Price { get; set; }
        public float BasePrice { get; set; }
        public float Amount { get; set; }
        public float BaseAmount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Item { get; set; }
        public virtual Uom Uom { get; set; }
    }
}