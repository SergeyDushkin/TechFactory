using Dapper;
using System;
using TF.Data.Business.WMS;

namespace TF.DAL.Query
{
    class OrderLineQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition(@"SELECT 
                [GUID_RECORD] Id,
	            [ORDER_GUID] OrderId,
	            [PRIORITY],			
	            [ITEM_GUID] ItemId,
	            [UOM_GUID] UomId,
	            [QTY],
	            [BASE_QTY] BaseQty,
	            [PRICE],
	            [BASE_PRICE] BasePrice,
	            [AMOUNT],
	            [BASE_AMOUNT] BaseAmount
                FROM [BUSINESS.WMS.ORDER_LINE] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition(@"SELECT 
                [GUID_RECORD] Id,
	            [ORDER_GUID] OrderId,
	            [PRIORITY],			
	            [ITEM_GUID] ItemId,
	            [UOM_GUID] UomId,
	            [QTY],
	            [BASE_QTY] BaseQty,
	            [PRICE],
	            [BASE_PRICE] BasePrice,
	            [AMOUNT],
	            [BASE_AMOUNT] BaseAmount
                FROM[BUSINESS.WMS.ORDER_LINE] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(OrderLine record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.WMS.ORDER_LINE]
                SET [ORDER_GUID] = @ORDER_GUID,
	                [PRIORITY] = @PRIORITY,
	                [ITEM_GUID] = @ITEM_GUID,
	                [UOM_GUID] = @UOM_GUID,
	                [QTY] = @QTY,
	                [BASE_QTY] = @BASE_QTY,
	                [PRICE] = @PRICE,
	                [BASE_PRICE] = @BASE_PRICE,
	                [AMOUNT] = @AMOUNT,
	                [BASE_AMOUNT] = @BASE_AMOUNT
                    WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    ORDER_GUID = record.OrderId,
                    PRIORITY = record.Priority,
                    ITEM_GUID = record.ItemId,
                    UOM_GUID = record.UomId,
                    QTY = record.Qty,
                    BASE_QTY = record.BaseQty,
                    PRICE = record.Price,
                    BASE_PRICE = record.BasePrice,
                    AMOUNT = record.Amount,
                    BASE_AMOUNT = record.BaseAmount
                });
        }

        public static CommandDefinition Insert(OrderLine record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.WMS.ORDER_LINE] ([GUID_RECORD],
	                [ORDER_GUID],
	                [PRIORITY],			
	                [ITEM_GUID],
	                [UOM_GUID],
	                [QTY],
	                [BASE_QTY],
	                [PRICE],
	                [BASE_PRICE],
	                [AMOUNT],
	                [BASE_AMOUNT],
                    [BATCH_GUID],
                    [HIDDEN],
                    [DELETED]) 
                VALUES (@GUID_RECORD,
                    @ORDER_GUID,
                    @PRIORITY,
                    @ITEM_GUID,
                    @UOM_GUID,
                    @QTY, 
                    @BASE_QTY,
                    @PRICE,
                    @BASE_PRICE,
                    @AMOUNT,
                    @BASE_AMOUNT,
                    @BATCH_GUID,
                    @HIDDEN,
                    @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    ORDER_GUID = record.OrderId,
                    PRIORITY = record.Priority,
                    ITEM_GUID = record.ItemId,
                    UOM_GUID = record.UomId,
                    QTY = record.Qty,
                    BASE_QTY = record.BaseQty,
                    PRICE = record.Price,
                    BASE_PRICE = record.BasePrice,
                    AMOUNT = record.Amount,
                    BASE_AMOUNT = record.BaseAmount,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"DELETE [BUSINESS.WMS.ORDER_LINE] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
