using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class CategoryQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [PARENT_GUID] ParentId FROM [BUSINESS.CATEGORY_TREE]");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [PARENT_GUID] ParentId FROM [BUSINESS.CATEGORY_TREE] where GUID_RECORD = @id", new { id });
        }

        public static CommandDefinition ByParentId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [PARENT_GUID] ParentId FROM [BUSINESS.CATEGORY_TREE] where PARENT_GUID = @id", new { id });
        }

        public static CommandDefinition Update(Category record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.CATEGORY_TREE]
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

        public static CommandDefinition Insert(Category record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.CATEGORY_TREE] ([GUID_RECORD], [KEY], [NAME], [PARENT_GUID], [BATCH_GUID], [HIDDEN], [DELETED]) 
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
                @"DELETE [BUSINESS.CATEGORY_TREE] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
