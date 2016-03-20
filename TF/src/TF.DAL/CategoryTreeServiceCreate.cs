using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TF.Data.Business;

namespace TF.DAL
{
    public partial class CategoryTreeService : ICategoryService
    {
        public Category Create(Category category)
        {
            return CreateAsync(category).Result;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            if (category.Id == Guid.Empty)
                category.Id = Guid.NewGuid();

            if (string.IsNullOrEmpty(category.Key))
                throw new ArgumentNullException("Key");

            if (string.IsNullOrEmpty(category.Name))
                throw new ArgumentNullException("Name");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    /// Check id
                    command.CommandText = string.Format("select 1 from [BUSINESS.CATEGORY_TREE] where [GUID_RECORD] = '{0}'", category.Id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                            throw new Exception("Record already exists");
                    }

                    /// Check key
                    command.CommandText = string.Format("select 1 from [BUSINESS.CATEGORY_TREE] where [KEY] = '{0}'", category.Key);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                            throw new Exception("Record already exists");
                    }

                    /// Check parent id
                    if (category.ParentId.HasValue && category.ParentId.Value != Guid.Empty)
                    {
                        command.CommandText = string.Format("select 1 from [BUSINESS.CATEGORY_TREE] where [GUID_RECORD] = '{0}'", category.ParentId);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows)
                                throw new Exception("Record does't exists");
                        }
                    }

                    command.CommandText = @"insert into [BUSINESS.CATEGORY_TREE] (
                        [GUID_RECORD], 
                        [KEY], 
                        [NAME], 
                        [PARENT_GUID], 
                        [BATCH_GUID], 
                        [HIDDEN], 
                        [DELETED]) 
                    values (@GUID_RECORD, @KEY, @NAME, @PARENT_GUID, @BATCH_GUID, @HIDDEN, @DELETED)";

                    command.Parameters.AddWithValue("@GUID_RECORD", category.Id);
                    command.Parameters.AddWithValue("@KEY", category.Key);
                    command.Parameters.AddWithValue("@NAME", category.Name);
                    command.Parameters.AddWithValue("@PARENT_GUID", category.ParentId.HasValue ? category.ParentId.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BATCH_GUID", DBNull.Value);
                    command.Parameters.AddWithValue("@HIDDEN", 0);
                    command.Parameters.AddWithValue("@DELETED", 0);

                    await command.ExecuteNonQueryAsync();
                }

                return category;
            }
        }
    }
}
