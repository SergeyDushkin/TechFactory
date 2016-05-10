using Dapper;
using System;
using TF.Data.Systems;

namespace TF.DAL.Query
{
    class LinkQuery
    {
        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [REFERENCE_GUID] ReferenceId, [URI] Uri FROM [SYSTEM.LINK] where GUID_RECORD = @id", new { id });
        }

        public static CommandDefinition ByReferenceId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [REFERENCE_GUID] ReferenceId, [URI] Uri FROM [SYSTEM.LINK] where REFERENCE_GUID = @id", new { id });
        }

        public static CommandDefinition Update(Link record)
        {
            return new CommandDefinition(
                @"UPDATE [SYSTEM.LINK]
                SET [REFERENCE_GUID] = @REFERENCE_GUID,
                [URI] = @URI
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    REFERENCE_GUID = record.ReferenceId,
                    URI = record.Uri
                });
        }

        public static CommandDefinition Insert(Link record)
        {
            return new CommandDefinition(
                @"INSERT INTO [SYSTEM.LINK] ([GUID_RECORD], [REFERENCE_GUID], [URI], [BATCH_GUID], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @REFERENCE_GUID, @URI,  @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    REFERENCE_GUID = record.ReferenceId,
                    URI = record.Uri,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"DELETE [SYSTEM.LINK] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
