using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TF.Data.Business;

namespace TF.DAL
{
    public partial class CategoryTreeService : ICategoryService
    {
        public Category Update(Category category)
        {
            return UpdateAsync(category).Result;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            if (category.Id == Guid.Empty)
                throw new ArgumentNullException("Id");

            if (string.IsNullOrEmpty(category.Key))
                throw new ArgumentNullException("Key");

            if (string.IsNullOrEmpty(category.Name))
                throw new ArgumentNullException("Name");

            var record = GetById(category.Id);

            var alterColumns = new List<string>();
            var alterValues = new List<SqlParameter>();

            if (!String.Equals(record.Key, category.Key))
            {
                alterColumns.Add("[KEY] = @KEY");
                alterValues.Add(new SqlParameter("@KEY", category.Key));
                /// add check constraint
            }

            if (!String.Equals(record.Name, category.Name))
            {
                alterColumns.Add("[NAME] = @NAME");
                alterValues.Add(new SqlParameter("@NAME", category.Name));
            }

            if (!Nullable<Guid>.Equals(record.ParentId, category.ParentId))
            {
                alterColumns.Add("[PARENT_GUID] = @PARENT_GUID");
                alterValues.Add(new SqlParameter("@PARENT_GUID", category.ParentId));

                /// add check constraint
            }

            if (alterColumns.Any())
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = String.Format(
                            "update [BUSINESS.CATEGORY_TREE] set {0} where [GUID_RECORD] = @GUID_RECORD",
                            String.Join(", ", alterColumns));

                        command.Parameters.AddWithValue("@GUID_RECORD", category.Id);
                        command.Parameters.AddRange(alterValues.ToArray());
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }

            return record;
        }
    }
}
