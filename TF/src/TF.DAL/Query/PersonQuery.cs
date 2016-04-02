using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class PersonQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [FIRSTNAME], [LASTNAME], [MIDNAME], [BIRTHDATE], [USER_GUID] UserGuid  FROM [BUSINESS.PERSON] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [FIRSTNAME], [LASTNAME], [MIDNAME], [BIRTHDATE], [USER_GUID] UserGuid  FROM [BUSINESS.PERSON] WHERE GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Person record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.PERSON] SET
                [FIRSTNAME] = @FIRSTNAME,
                [LASTNAME] = @LASTNAME,
                [MIDNAME] = @MIDNAME,
                [BIRTHDATE] = @BIRTHDATE,
                [HIDDEN] = @HIDDEN
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    FIRSTNAME = record.Firstname,
                    LASTNAME = record.Lastname,
                    MIDNAME = record.Midname,
                    BIRTHDATE = record.Birthdate,
                    HIDDEN = record.Hidden
                });
        }

        public static CommandDefinition Insert(Person record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.PERSON] (
                        [GUID_RECORD], 
                        [FIRSTNAME], 
                        [LASTNAME], 
                        [MIDNAME], 
                        [BIRTHDATE], 
                        [USER_GUID], 
                        [BATCH_GUID], 
                        [HIDDEN], 
                        [DELETED]) 
                        values (@GUID_RECORD, @FIRSTNAME, @LASTNAME, @MIDNAME, @BIRTHDATE, @USER_GUID, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    FIRSTNAME = record.Firstname,
                    LASTNAME = record.Lastname,
                    MIDNAME = record.Midname,
                    BIRTHDATE = record.Birthdate,
                    //MIDNAME = string.IsNullOrEmpty(record.MIDNAME) ? string.Empty : record.MIDNAME,
                    //BIRTHDATE = record.BIRTHDATE.HasValue ? record.BIRTHDATE.Value : (object)DBNull.Value,
                    USER_GUID = (Guid?)null,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.PERSON] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
