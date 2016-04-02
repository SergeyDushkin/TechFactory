using Dapper;
using System;
using TF.Data.Systems.Security;

namespace TF.DAL.Query
{
    class UserRoleQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [USER_GUID] UserGuid, [ROLE_GUID] RoleGuid, [HIDDEN] FROM [SYSTEM.SECURITY.USER_ROLE] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [USER_GUID] UserGuid, [ROLE_GUID] RoleGuid, [HIDDEN] FROM [SYSTEM.SECURITY.USER_ROLE] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(UserRole record)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.SECURITY.USER_ROLE] SET
                [USER_GUID] = @USER_GUID,
                [ROLE_GUID] = @ROLE_GUID,
                [BATCH_GUID] = @BATCH_GUID,
                [HIDDEN] = @HIDDEN
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    USER_GUID = record.UserGuid,
                    ROLE_GUID = record.RoleGuid,
                    BATCH_GUID = record.BatchGuid,
                    HIDDEN = record.Hidden
                });
        }

        public static CommandDefinition Insert(UserRole record)
        {
            return new CommandDefinition(
                @"INSERT INTO [SYSTEM.SECURITY.USER_ROLE] (
                        [GUID_RECORD], 
                        [USER_GUID], 
                        [ROLE_GUID], 
                        [BATCH_GUID], 
                        [HIDDEN], 
                        [DELETED]) 
                        values (@GUID_RECORD, @USER_GUID, @ROLE_GUID, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    USER_GUID = record.UserGuid,
                    ROLE_GUID = record.RoleGuid,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.SECURITY.USER_ROLE] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
