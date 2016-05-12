using System;

namespace TF.Data.Business.WMS
{
    public class ProductSpecification
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }
        public Guid ChildUomId { get; set; }

        /// <summary>
        /// reference to the parent product/service
        /// </summary>
        public virtual Product Parent { get; set; }

        /// <summary>
        /// reference to the child product/service (ingredient)
        /// </summary>
        public virtual Product Child { get; set; }

        /// <summary>
        /// UOM of child product
        /// </summary>
        public virtual Uom ChildUom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float BaseQty { get; set; }
    }
}
