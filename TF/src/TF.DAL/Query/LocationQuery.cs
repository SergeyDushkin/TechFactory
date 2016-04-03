using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class LocationQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [BUSINESSUNIT_GUID] UnitId, [TYPE], [KEY], [NAME] FROM [BUSINESS.LOCATION] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [BUSINESSUNIT_GUID] UnitId, [TYPE], [KEY], [NAME] FROM [BUSINESS.LOCATION] where GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Location record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.LOCATION]
                SET [KEY] = @KEY,
                [NAME] = @NAME,
                [BUSINESSUNIT_GUID] = @BUSINESSUNIT_GUID,
                [TYPE] = @TYPE
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    KEY = record.Key,
                    NAME = record.Name,
                    BUSINESSUNIT_GUID = record.UnitId,
                    TYPE = record.Type
                });
        }

        public static CommandDefinition Insert(Location record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.LOCATION] ([GUID_RECORD], [BUSINESSUNIT_GUID], [TYPE], [KEY], [NAME], [BATCH_GUID], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @BUSINESSUNIT_GUID, @TYPE, @KEY, @NAME, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    BUSINESSUNIT_GUID = record.UnitId,
                    TYPE = record.Type,
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
                @"UPDATE [BUSINESS.LOCATION] SET [DELETED] = 1 WHERE[GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
