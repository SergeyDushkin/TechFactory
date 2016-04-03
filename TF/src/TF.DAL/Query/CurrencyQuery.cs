using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class CurrencyQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME] FROM [BUSINESS.CURRENCY] WHERE WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME] FROM [BUSINESS.CURRENCY] where GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Currency record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.CURRENCY]
                SET [KEY] = @KEY,
                [NAME] = @NAME
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    KEY = record.Key,
                    NAME = record.Name
                });
        }

        public static CommandDefinition Insert(Currency record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.CURRENCY] ([GUID_RECORD], [KEY], [NAME], [BATCH_GUID], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @KEY, @NAME, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    KEY = record.Key,
                    NAME = record.Name,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.CURRENCY] SET [DELETED] = 1 WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
