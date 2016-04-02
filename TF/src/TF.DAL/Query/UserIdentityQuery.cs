using Dapper;
using System;
using TF.Data.Systems.Security;

namespace TF.DAL.Query
{
    class UserIdentityQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [USER_GUID] ID, [PROVIDER], [KEY], [BATCH_GUID], [HIDDEN] FROM [SYSTEM.SECURITY.USER_IDENTITY] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [USER_GUID] ID, [PROVIDER], [KEY], [BATCH_GUID], [HIDDEN] FROM [SYSTEM.SECURITY.USER_IDENTITY] WHERE USER_GUID = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(UserIdentity record)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.SECURITY.USER_IDENTITY] SET
                [PROVIDER] = @PROVIDER,
                [KEY] = @KEY,
                [BATCH_GUID] = @BATCH_GUID,
                [HIDDEN] = @HIDDEN
                WHERE USER_GUID = @USER_GUID", new
                {
                    USER_GUID = record.Id,
                    PROVIDER = record.Provider,
                    KEY = record.Key,
                    BATCH_GUID = record.BatchGuid,
                    HIDDEN = record.Hidden
                });
        }

        public static CommandDefinition Insert(UserIdentity record)
        {
            return new CommandDefinition(
                @"INSERT INTO [SYSTEM.SECURITY.USER_IDENTITY] (
                        [USER_GUID],
                        [PROVIDER], 
                        [KEY], 
                        [BATCH_GUID], 
                        [HIDDEN], 
                        [DELETED]) 
                        values (@USER_GUID, @PROVIDER, @KEY, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    USER_GUID = record.Id,
                    PROVIDER = record.Provider,
                    KEY = record.Key,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.SECURITY.USER_IDENTITY] SET [DELETED] = 1 WHERE [USER_GUID] = @USER_GUID", new
                {
                    USER_GUID = id
                });
        }
    }
}
