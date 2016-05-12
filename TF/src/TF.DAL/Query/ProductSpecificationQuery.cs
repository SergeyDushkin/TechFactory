using Dapper;
using System;
using TF.DAL.Models;

namespace TF.DAL.Query
{
    class ProductSpecificationQuery
    {
        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT * FROM [BUSINESS.WMS.KIT_SPEC] where GUID_RECORD = @id", new { id });
        }

        public static CommandDefinition ByParentId(Guid id)
        {
            return new CommandDefinition("SELECT * [BUSINESS.WMS.KIT_SPEC] where PARENT_GUID = @id", new { id });
        }

        public static CommandDefinition ByChildId(Guid id)
        {
            return new CommandDefinition("SELECT * FROM [BUSINESS.WMS.KIT_SPEC] where CHILD_GUID = @id", new { id });
        }

        public static CommandDefinition Update(BUSINESS_WMS_KIT_SPEC record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.WMS.KIT_SPEC] SET 
                    [PARENT_GUID] = @PARENT_GUID,
                    [CHILD_GUID] = @CHILD_GUID,
                    [CHILD_UOM_GUID] = @CHILD_UOM_GUID,
                    [BASE_QTY] = @BASE_QTY,
                    [BATCH_GUID] = @BATCH_GUID
                    [HIDDEN] = @HIDDEN
                    [DELETED] = @DELETED
                WHERE GUID_RECORD = @GUID_RECORD", record);
        }

        public static CommandDefinition Insert(BUSINESS_WMS_KIT_SPEC record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.WMS.KIT_SPEC] (
                    [GUID_RECORD], [PARENT_GUID], [CHILD_GUID], [CHILD_UOM_GUID], [BASE_QTY], [BATCH_GUID], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @PARENT_GUID, @CHILD_GUID,  @CHILD_UOM_GUID, @BASE_QTY, @BATCH_GUID, @HIDDEN, @DELETED)", record);
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"DELETE [BUSINESS.WMS.KIT_SPEC] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
