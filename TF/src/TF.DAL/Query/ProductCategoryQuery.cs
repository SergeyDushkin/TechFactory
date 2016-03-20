using Dapper;
using System;
using TF.Data.Business.WMS;

namespace TF.DAL.Query
{
    class ProductCategoryQuery
    {
        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [CATEGORY_GUID] CategoryId, [ITEM_GUID] ProductId FROM [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] where GUID_RECORD = @id", new { id });
        }

        public static CommandDefinition ByProductId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [CATEGORY_GUID] CategoryId, [ITEM_GUID] ProductId FROM [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] where ITEM_GUID = @id", new { id });
        }

        public static CommandDefinition ByCategoryId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [CATEGORY_GUID] CategoryId, [ITEM_GUID] ProductId FROM [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] where CATEGORY_GUID = @id", new { id });
        }

        public static CommandDefinition ProductsByCategoryId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [TYPE], [KEY], [NAME] FROM [BUSINESS.WMS.PRODUCT_N_SERVICE] where GUID_RECORD IN (SELECT [ITEM_GUID] FROM [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] where CATEGORY_GUID = @id)", new { id });
        }

        public static CommandDefinition CategoriesByProductId(Guid id)
        {
            return new CommandDefinition("SELECT [GUID_RECORD] Id, [KEY], [NAME], [PARENT_GUID] ParentId FROM [BUSINESS.CATEGORY_TREE] where GUID_RECORD IN (SELECT [CATEGORY_GUID] FROM [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] where ITEM_GUID = @id)", new { id });
        }

        public static CommandDefinition Update(ProductCategory record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] 
                SET [CATEGORY_GUID] = @CATEGORY_GUID,
                [ITEM_GUID] = @ITEM_GUID
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = record.Id,
                    ITEM_GUID = record.ProductId,
                    CATEGORY_GUID = record.CategoryId
                });
        }

        public static CommandDefinition Insert(ProductCategory record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] ([GUID_RECORD], [CATEGORY_GUID], [ITEM_GUID], [BATCH_GUID], [HIDDEN], [DELETED]) 
                VALUES (@GUID_RECORD, @CATEGORY_GUID, @ITEM_GUID, @BATCH_GUID, @HIDDEN, @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    ITEM_GUID = record.ProductId,
                    CATEGORY_GUID = record.CategoryId,
                    BATCH_GUID = (Guid?)null,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"DELETE [BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY] WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
