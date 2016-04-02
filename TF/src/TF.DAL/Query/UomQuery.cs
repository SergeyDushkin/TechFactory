using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class UomQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME] FROM [BUSINESS.UOM]");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME] FROM [BUSINESS.UOM] where GUID_RECORD = @id", new { id });
        }

        public static CommandDefinition Update(Uom record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.UOM]
                SET [KEY] = @KEY,
                [NAME] = @NAME
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    KEY = record.Key,
                    NAME = record.Name
                });
        }

        public static CommandDefinition Insert(Uom record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.UOM] ([GUID_RECORD], [KEY], [NAME], [BATCH_GUID], [HIDDEN], [DELETED]) 
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
                @"DELETE [BUSINESS.UOM] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
