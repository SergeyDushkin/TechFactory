using System;
using System.Collections.Generic;

namespace TF.Data.Business.WMS
{
    public class Order
    {
        public Guid Id { get; set; }

        /// <summary>
        /// valid values: SO
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Date/Time when order should be processed
        /// </summary>
        public DateTime DueDate { get; set; }

        public string Number { get; set; }
        public DateTime Date { get; set; }

        public short LinesCount { get; set; }

        public float Amount { get; set; }
        public float BaseAmount { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Unit Customer { get; set; }

        public Guid SourceId { get; set; }
        public virtual Location Source { get; set; }

        public Guid DestinationId { get; set; }
        public virtual Location Destination { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public virtual ICollection<OrderLine> Lines { get; set; }
    }
}
