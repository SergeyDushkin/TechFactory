using Dapper;
using System;
using TF.Data.Systems;

namespace TF.DAL.Query
{
    class EntityQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [PARENT_GUID] ParentId FROM [SYSTEM.ENTITY]");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [PARENT_GUID] ParentId FROM [SYSTEM.ENTITY] where GUID_RECORD = @id", new { id });
        }

        public static CommandDefinition ByParentId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [PARENT_GUID] ParentId FROM [SYSTEM.ENTITY] where PARENT_GUID = @id", new { id });
        }

        public static CommandDefinition Update(Entity record)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.ENTITY]
                SET [KEY] = @KEY,
                [NAME] = @NAME,
                [PARENT_GUID] = @PARENT_GUID
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    KEY = record.Key,
                    NAME = record.Name,
                    PARENT_GUID = record.ParentId
                });
        }

        public static CommandDefinition Insert(Entity record)
        {
            return new CommandDefinition(
                @"INSERT INTO [SYSTEM.ENTITY] ([GUID_RECORD], [KEY], [NAME], [PARENT_GUID], [BATCH_GUID], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @KEY, @NAME, @PARENT_GUID, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    KEY = record.Key,
                    NAME = record.Name,
                    PARENT_GUID = record.ParentId,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"DELETE [SYSTEM.ENTITY] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
