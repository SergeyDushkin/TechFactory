using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class EmployeeQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [BUSINESSUNIT_GUID] UnitId, [PERSON_GUID] PersonId FROM [BUSINESS.EMPLOYEE] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [BUSINESSUNIT_GUID] UnitId, [PERSON_GUID] PersonId FROM [BUSINESS.EMPLOYEE] where GUID_RECORD = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition ByUnitId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [BUSINESSUNIT_GUID] UnitId, [PERSON_GUID] PersonId FROM [BUSINESS.EMPLOYEE] where BUSINESSUNIT_GUID = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition ByPersonId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [BUSINESSUNIT_GUID] UnitId, [PERSON_GUID] PersonId FROM [BUSINESS.EMPLOYEE] where PERSON_GUID = @id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Employee record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.EMPLOYEE]
                SET [BUSINESSUNIT_GUID] = @BUSINESSUNIT_GUID,
                [PERSON_GUID] = @PERSON_GUID
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    BUSINESSUNIT_GUID = record.UnitId,
                    PERSON_GUID = record.PersonId
                });
        }

        public static CommandDefinition Insert(Employee record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.EMPLOYEE] ([GUID_RECORD], [BUSINESSUNIT_GUID], [DEPARTMENT_GUID], [POSITION_GUID], [START_DATE], [END_DATE], [PERSON_GUID], [BATCH_GUID], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @BUSINESSUNIT_GUID, @DEPARTMENT_GUID, @POSITION_GUID, @START_DATE, @END_DATE, @PERSON_GUID, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    BUSINESSUNIT_GUID = record.UnitId,
                    DEPARTMENT_GUID = (Guid?)null,
                    POSITION_GUID = (Guid?)null,
                    START_DATE = (DateTime?) null,
                    END_DATE = (DateTime?) null,
                    PERSON_GUID = record.PersonId,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.EMPLOYEE] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
