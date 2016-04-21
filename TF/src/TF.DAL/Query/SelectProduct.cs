using Dapper;
using System;
using TF.Data.Business.WMS;

namespace TF.DAL.Query
{
    class SelectProduct
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [TYPE], [KEY], [NAME] FROM [BUSINESS.WMS.PRODUCT_N_SERVICE]");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [TYPE], [KEY], [NAME] FROM [BUSINESS.WMS.PRODUCT_N_SERVICE] where GUID_RECORD = @id", new { id });
        }

        public static CommandDefinition ByKey(string key)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [TYPE], [KEY], [NAME] FROM [BUSINESS.WMS.PRODUCT_N_SERVICE] where [KEY] = @key", new { key });
        }

        public static CommandDefinition Update(Product product)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.WMS.PRODUCT_N_SERVICE] 
                SET [TYPE] = @TYPE,
                [KEY] = @KEY,
                [NAME] = @NAME,
                [UOM_GUID] = @UOM_GUID,
                [ALLOW_NEGATIVE_QTY] = @ALLOW_NEGATIVE_QTY
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    TYPE = product.Type,
                    KEY = product.Key,
                    NAME = product.Name,
                    UOM_GUID = Guid.Empty,
                    ALLOW_NEGATIVE_QTY = 0,
                    GUID_RECORD = product.Id
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"DELETE [BUSINESS.WMS.PRODUCT_N_SERVICE] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }

        public static CommandDefinition Insert(Product product)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.WMS.PRODUCT_N_SERVICE] ([GUID_RECORD], [TYPE], [KEY], [NAME], [UOM_GUID], [ALLOW_NEGATIVE_QTY], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @TYPE, @KEY, @NAME, @UOM_GUID, @ALLOW_NEGATIVE_QTY, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = product.Id,
                    TYPE = product.Type,
                    KEY = product.Key,
                    NAME = product.Name,
                    UOM_GUID = Guid.Empty,
                    ALLOW_NEGATIVE_QTY = 0,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }
    }
}
