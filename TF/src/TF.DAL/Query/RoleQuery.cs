using Dapper;
using System;
using TF.Data.Systems.Security;

namespace TF.DAL.Query
{
    class RoleQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [HIDDEN]  FROM [SYSTEM.SECURITY.ROLE] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [HIDDEN]  FROM [SYSTEM.SECURITY.ROLE] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Role record)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.SECURITY.ROLE] SET
                [KEY] = @KEY,
                [NAME] = @NAME,
                [BATCH_GUID] = @BATCH_GUID,
                [HIDDEN] = @HIDDEN
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    KEY = record.Key,
                    NAME = record.Name,
                    BATCH_GUID = record.BatchGuid,
                    HIDDEN = record.Hidden
                });
        }

        public static CommandDefinition Insert(Role record)
        {
            return new CommandDefinition(
                @"INSERT INTO [SYSTEM.SECURITY.ROLE] (
                        [GUID_RECORD], 
                        [KEY], 
                        [NAME], 
                        [BATCH_GUID], 
                        [HIDDEN], 
                        [DELETED]) 
                        values (@GUID_RECORD, @KEY, @NAME, @BATCH_GUID, @HIDDEN, @DELETED)", new
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
                @"UPDATE [SYSTEM.SECURITY.ROLE] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
