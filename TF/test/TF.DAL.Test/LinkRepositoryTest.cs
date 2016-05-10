using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Systems;
using System.Threading.Tasks;

namespace TF.DAL.Test
{
    [TestClass]
    public class LinkRepositoryTest
    {
        [TestMethod]
        public async Task LinkRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            ILinkRepository repository = new LinkRepository(context);

            var id = Guid.NewGuid();

            var record = new Link
            {
                Id = id,
                ReferenceId = Guid.Empty,
                Uri = "test"
            };
            
            await repository.CreateAsync(record);

            record.Uri = "U_" + record.Uri;

            await repository.UpdateAsync(record);

            var record2 = await repository.GetByIdAsync(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.Uri, record2.Uri);

            await repository.DeleteAsync(record.Id);

            var record3 = await repository.GetByIdAsync(id);

            Assert.IsNull(record3);
        }
    }
}
