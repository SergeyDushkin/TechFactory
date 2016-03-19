﻿using System;
namespace TF.Data.Business.WMS
{
    /// <summary>
    /// Price for product
    /// </summary>
    public class Price
    {
        /// <summary>
        /// Price identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product identity
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public double ProductPrice { get; set; }
    }
}
