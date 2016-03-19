using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TF.Data.Business;

namespace TF.DAL
{
    public partial class CategoryTreeService : ICategoryService
    {
        public void Delete(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from [BUSINESS.CATEGORY_TREE] where [GUID_RECORD] = @GUID_RECORD";
                    command.Parameters.AddWithValue("@GUID_RECORD", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
