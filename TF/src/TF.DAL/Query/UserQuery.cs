using Dapper;
using System;
using TF.Data.Systems.Security;

namespace TF.DAL.Query
{
    class UserQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [LAST_LOGIN], [LOGIN_ATTEMPT_COUNT], [HIDDEN]  ParentId FROM [SYSTEM.SECURITY.USER] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [LAST_LOGIN], [LOGIN_ATTEMPT_COUNT], [HIDDEN] ParentId FROM [SYSTEM.SECURITY.USER] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(USER record)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.SECURITY.USER] SET
                [KEY] = @KEY,
                [LAST_LOGIN] = @LAST_LOGIN,
                [LOGIN_ATTEMPT_COUNT] = @LOGIN_ATTEMPT_COUNT,
                [BATCH_GUID] = @BATCH_GUID,
                [HIDDEN] = @HIDDEN
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.ID,
                    KEY = record.KEY,
                    LAST_LOGIN = record.LAST_LOGIN,
                    LOGIN_ATTEMPT_COUNT = record.LOGIN_ATTEMPT_COUNT,
                    BATCH_GUID = record.BATCH_GUID,
                    HIDDEN = record.HIDDEN
                });
        }

        public static CommandDefinition Insert(USER record)
        {
            return new CommandDefinition(
                @"INSERT INTO [SYSTEM.SECURITY.USER] (
                        [GUID_RECORD], 
                        [KEY], 
                        [LAST_LOGIN], 
                        [LOGIN_ATTEMPT_COUNT], 
                        [BATCH_GUID], 
                        [HIDDEN], 
                        [DELETED]) 
                        values (@GUID_RECORD, @KEY, @LAST_LOGIN, @LOGIN_ATTEMPT_COUNT, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.ID,
                    KEY = record.KEY,
                    LAST_LOGIN = record.LAST_LOGIN,
                    LOGIN_ATTEMPT_COUNT = record.LOGIN_ATTEMPT_COUNT,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.SECURITY.USER] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
