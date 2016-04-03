using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class ContactDetailQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [CONTACT_GUID] ContactGuid, [PRIORITY], [TYPE],  [VALUE], [VERIFIED], [ALLOW], [HIDDEN] FROM [BUSINESS.CONTACT_DETAIL] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [CONTACT_GUID] ContactGuid, [PRIORITY], [TYPE],  [VALUE], [VERIFIED], [ALLOW], [HIDDEN] FROM [BUSINESS.CONTACT_DETAIL] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(ContactDetail record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.CONTACT_DETAIL] SET
                [CONTACT_GUID] = @CONTACT_GUID,
                [PRIORITY] = @PRIORITY,
                [TYPE] = @TYPE,
                [VALUE] = @VALUE,
                [VERIFIED] = @VERIFIED,
                [ALLOW] = @ALLOW,
                [BATCH_GUID] = @BATCH_GUID,
                [HIDDEN] = @HIDDEN
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    CONTACT_GUID = record.ContactGuid,
                    PRIORITY = record.Priority,
                    TYPE = record.Type,
                    VALUE = record.Value,
                    VERIFIED = record.Verified,
                    ALLOW = record.Allow,
                    BATCH_GUID = record.BatchGuid,
                    HIDDEN = record.Hidden
                });
        }

        public static CommandDefinition Insert(ContactDetail record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.CONTACT_DETAIL] (
                        [GUID_RECORD], 
                        [CONTACT_GUID], 
                        [PRIORITY], 
                        [TYPE], 
                        [VALUE], 
                        [VERIFIED], 
                        [ALLOW], 
                        [BATCH_GUID],
                        [HIDDEN], 
                        [DELETED]) 
                        values (@GUID_RECORD, @CONTACT_GUID, @PRIORITY, @TYPE, @VALUE, @VERIFIED, @ALLOW, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    CONTACT_GUID = record.ContactGuid,
                    PRIORITY = record.Priority,
                    TYPE = record.Type,
                    VALUE = record.Value,
                    VERIFIED = record.Verified,
                    ALLOW = record.Allow,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.CONTACT_DETAIL] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
