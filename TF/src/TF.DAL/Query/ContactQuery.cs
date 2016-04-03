using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class ContactQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [RECORD_GUID] RecordGuid, [TYPE],  [HIDDEN]  FROM [BUSINESS.CONTACT] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [RECORD_GUID] RecordGuid, [TYPE],  [HIDDEN]  FROM [BUSINESS.CONTACT] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Contact record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.CONTACT] SET
                [ENTITY_GUID] = @ENTITY_GUID,
                [RECORD_GUID] = @RECORD_GUID,
                [TYPE] = @TYPE,
                [BATCH_GUID] = @BATCH_GUID,
                [HIDDEN] = @HIDDEN
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    ENTITY_GUID = Guid.Empty,
                    RECORD_GUID = record.RecordGuid,
                    TYPE = record.Type,
                    BATCH_GUID = record.BatchGuid,
                    HIDDEN = record.Hidden
                });
        }

        public static CommandDefinition Insert(Contact record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.CONTACT] (
                        [GUID_RECORD], 
                        [ENTITY_GUID], 
                        [RECORD_GUID], 
                        [TYPE], 
                        [BATCH_GUID], 
                        [HIDDEN], 
                        [DELETED]) 
                        values (@GUID_RECORD, @ENTITY_GUID, @RECORD_GUID, @TYPE, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    ENTITY_GUID = Guid.Empty,
                    RECORD_GUID = record.RecordGuid,
                    TYPE = record.Type,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.CONTACT] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
