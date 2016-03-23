using System;
using System.Linq;
using Dapper;
using TF.Data.Business.WMS;

namespace TF.DAL
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly NoodleDbContext context;

        public ProductPriceService(NoodleDbContext context)
        {
            this.context = context;
        }

        public ProductPrice Create(ProductPrice price)
        {
            if (price == null)
                throw new ArgumentNullException("price");

            if (price.Id == Guid.Empty)
                price.Id = Guid.NewGuid();

            if (price.ProductId == Guid.Empty)
                throw new ArgumentNullException("ProductId");

            using (var connection = context.CreateConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    /// Check id
                    command.CommandText = string.Format("select 1 from [BUSINESS.WMS.PRICE] where [GUID_RECORD] = '{0}'", price.Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            throw new Exception("Record already exists");
                    }

                    /// Check product id
                    command.CommandText = string.Format("select 1 from [BUSINESS.WMS.PRICE] where [ITEM_GUID] = '{0}'", price.ProductId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            throw new Exception("Record already exists");
                    }

                    command.CommandText = @"insert into [BUSINESS.WMS.PRICE] (
	                    [GUID_RECORD],
	                    [ITEM_GUID],
	                    [CURRENCY_GUID],
	                    [LOCATION_GUID],
	                    [PRICE],
	                    [BATCH_GUID],
	                    [HIDDEN],
	                    [DELETED]) 
                    values (@GUID_RECORD, @ITEM_GUID, @CURRENCY_GUID, @LOCATION_GUID, @PRICE, @BATCH_GUID, @HIDDEN, @DELETED)";

                    connection.Execute(command.CommandText, new {
                        GUID_RECORD = price.Id,
                        ITEM_GUID = price.ProductId,
                        CURRENCY_GUID = Guid.Empty,
                        LOCATION_GUID = (Guid?)null,
                        PRICE = price.Price,
                        BATCH_GUID = (Guid?)null,
                        HIDDEN = 0,
                        DELETED = 0
                    });
                }
            }

            return price;
        }

        public ProductPrice Update(ProductPrice price)
        {
            if (price == null)
                throw new ArgumentNullException("price");

            if (price.Id == Guid.Empty)
                throw new ArgumentNullException("Id");

            if (price.ProductId == Guid.Empty)
                throw new ArgumentNullException("ProductId");

            using (var connection = context.CreateConnection())
            {
                connection.Execute("update [BUSINESS.WMS.PRICE] set [ITEM_GUID] = @ITEM_GUID, [PRICE] = @PRICE where [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = price.Id,
                    ITEM_GUID = price.ProductId,
                    PRICE = price.Price
                });
            }

            return price;
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (var connection = context.CreateConnection())
            {
                connection.Execute("delete from [BUSINESS.WMS.PRICE] where [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
            }
        }

        public void DeleteByProduct(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (var connection = context.CreateConnection())
            {
                connection.Execute("delete from [BUSINESS.WMS.PRICE] where [ITEM_GUID] = @ITEM_GUID", new
                {
                    ITEM_GUID = id
                });
            }
        }

        public ProductPrice GetByProductId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (var connection = context.CreateConnection())
            {
                return connection.Query<ProductPrice>(new CommandDefinition("select GUID_RECORD Id, ITEM_GUID ProductId, PRICE from [BUSINESS.WMS.PRICE] where [ITEM_GUID] = @ITEM_GUID", new { ITEM_GUID = id })).SingleOrDefault();
            }
        }

        public ProductPrice GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (var connection = context.CreateConnection())
            {
                return connection.Query<ProductPrice>(new CommandDefinition("select GUID_RECORD Id, ITEM_GUID ProductId, PRICE from [BUSINESS.WMS.PRICE] where [GUID_RECORD] = @GUID_RECORD", new { GUID_RECORD = id })).SingleOrDefault();
            }
        }
    }
}