using Dapper;
using System;
using TF.Data.Business.WMS;

namespace TF.DAL.Query
{
    class OrderLineDetailQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition(@"SELECT 
                [GUID_RECORD] Id,
                [ORDER_GUID] OrderId,
                [ORDER_LINE_GUID] OrderLineId,
                [PRIORITY],		
                [ITEM_GUID] ItemId,
                [LOCATION_GUID] LocationId,
                [UOM_GUID] UomId,
                [QTY],
                [BASE_QTY],
                [NUMBER]
                FROM [BUSINESS.WMS.ORDER_LINE_DETAIL] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition(@"SELECT 
                [GUID_RECORD] Id,
                [ORDER_GUID] OrderId,
                [ORDER_LINE_GUID] OrderLineId,
                [PRIORITY],		
                [ITEM_GUID] ItemId,
                [LOCATION_GUID] LocationId,
                [UOM_GUID] UomId,
                [QTY],
                [BASE_QTY],
                [NUMBER]
                FROM[BUSINESS.WMS.ORDER_LINE_DETAIL] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(OrderLineDetail record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.WMS.ORDER_LINE_DETAIL]
                SET [ORDER_GUID] = @ORDER_GUID,
                    [ORDER_LINE_GUID] = @ORDER_LINE_GUID,
                    [PRIORITY] = @PRIORITY,		
                    [ITEM_GUID] = @ITEM_GUID,
                    [LOCATION_GUID] = @LOCATION_GUID,
                    [UOM_GUID] = @UOM_GUID,
                    [QTY] = @QTY,
                    [BASE_QTY] = @BASE_QTY,
                    [NUMBER] = @NUMBER
                    WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    ORDER_GUID = record.OrderId,
                    ORDER_LINE_GUID = record.OrderLineId,
                    PRIORITY = record.Priority,
                    ITEM_GUID = record.ItemId,
                    LOCATION_GUID = record.LocationId,
                    UOM_GUID = record.UomId,
                    QTY = record.Qty,
                    BASE_QTY = record.BaseQty,
                    NUMBER = record.Number
                });
        }

        public static CommandDefinition Insert(OrderLineDetail record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.WMS.ORDER_LINE_DETAIL] ([GUID_RECORD],
                    [ORDER_GUID],
                    [ORDER_LINE_GUID],
                    [PRIORITY],		
                    [ITEM_GUID],
                    [LOCATION_GUID],
                    [UOM_GUID],
                    [QTY],
                    [BASE_QTY],
                    [NUMBER],
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
                    ORDER_LINE_GUID = record.OrderLineId,
                    PRIORITY = record.Priority,
                    ITEM_GUID = record.ItemId,
                    LOCATION_GUID = record.LocationId,
                    UOM_GUID = record.UomId,
                    QTY = record.Qty,
                    BASE_QTY = record.BaseQty,
                    NUMBER = record.Number,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"DELETE [BUSINESS.WMS.ORDER_LINE_DETAIL] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
