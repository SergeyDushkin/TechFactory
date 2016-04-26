using Dapper;
using System;
using TF.Data.Business.WMS;

namespace TF.DAL.Query
{
    class OrderQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition(@"SELECT 
                [GUID_RECORD] Id, 
                [TYPE], 
                [STATUS_CODE] StatusCode, 
                [DUEDATE], 
                [NUMBER], 
                [DATE], 
                [CUSTOMER_GUID] CustomerId, 
                [SOURCE_GUID] SourceId, 
                [DESTINATION_GUID] DestinationId, 
                [CURRENCY_GUID] CurrencyId, 
                [LINES] LinesCount, 
                [AMOUNT], 
                [BASE_AMOUNT] BaseAmount
                FROM [BUSINESS.WMS.ORDER] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition(@"SELECT 
                [GUID_RECORD] Id, 
                [TYPE], 
                [STATUS_CODE] StatusCode, 
                [DUEDATE], 
                [NUMBER], 
                [DATE], 
                [CUSTOMER_GUID] CustomerId, 
                [SOURCE_GUID] SourceId, 
                [DESTINATION_GUID] DestinationId, 
                [CURRENCY_GUID] CurrencyId, 
                [LINES] LinesCount, 
                [AMOUNT], 
                [BASE_AMOUNT] BaseAmount
                FROM[BUSINESS.WMS.ORDER] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Order record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.WMS.ORDER]
                SET [TYPE] = @TYPE,
                [STATUS_CODE] = @STATUS_CODE,
                [DUEDATE] = @DUEDATE,
                [NUMBER] = @NUMBER,
                [DATE] = @DATE,
                [CUSTOMER_GUID] = @CUSTOMER_GUID,
                [SOURCE_GUID] = @SOURCE_GUID,
                [DESTINATION_GUID] = @DESTINATION_GUID,
                [CURRENCY_GUID] = @CURRENCY_GUID,
                [LINES] = @LINES,
                [AMOUNT] = @AMOUNT,
                [BASE_AMOUNT] = @BASE_AMOUNT
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    TYPE = record.Type,
                    STATUS_CODE = record.StatusCode,
                    DUEDATE = record.DueDate,
                    NUMBER = record.Number,
                    DATE = record.Date,
                    CUSTOMER_GUID = record.CustomerId,
                    SOURCE_GUID = record.SourceId,
                    DESTINATION_GUID = record.DestinationId,
                    CURRENCY_GUID = record.CurrencyId,
                    LINES = record.LinesCount,
                    AMOUNT = record.Amount,
                    BASE_AMOUNT = record.BaseAmount
                });
        }

        public static CommandDefinition Insert(Order record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.WMS.ORDER] ([GUID_RECORD],
                [TYPE],
                [STATUS_CODE],
                [DUEDATE],
                [NUMBER],
                [DATE],
                [CUSTOMER_GUID],
                [SOURCE_GUID],
                [DESTINATION_GUID],
                [CURRENCY_GUID],
                [LINES],
                [AMOUNT],
                [BASE_AMOUNT],
                [BATCH_GUID],
                [HIDDEN],
                [DELETED]) 
                VALUES (@GUID_RECORD, @TYPE, @STATUS_CODE, @DUEDATE, @NUMBER, @DATE, @CUSTOMER_GUID, 
                @SOURCE_GUID, @DESTINATION_GUID, @CURRENCY_GUID, @LINES, @AMOUNT, 
                @BASE_AMOUNT, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    TYPE = record.Type,
                    STATUS_CODE = record.StatusCode,
                    DUEDATE = record.DueDate,
                    NUMBER = record.Number,
                    DATE = record.Date,
                    CUSTOMER_GUID = record.CustomerId,
                    SOURCE_GUID = record.SourceId,
                    DESTINATION_GUID = record.DestinationId,
                    CURRENCY_GUID = record.CurrencyId,
                    LINES = record.LinesCount,
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
                @"UPDATE [BUSINESS.WMS.ORDER] SET[DELETED] = 1 WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
