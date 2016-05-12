using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Systems;
using System.Threading.Tasks;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class ProductSpecificationRepositoryTest
    {
        [TestMethod]
        public async Task ProductSpecificationRepositoryCRUDTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();

            IProductSpecificationRepository repository = new ProductSpecificationRepository(context);

            var id = Guid.NewGuid();

            var record = new ProductSpecification
            {
                Id = id,
                BaseQty = 10,
                Child = new Product
                {
                    Id = Guid.Empty
                },
                ChildUom = new Data.Business.Uom
                {
                    Id = Guid.Empty
                },
                Parent = new Product
                {
                    Id = Guid.Empty
                }
            };
            
            await repository.CreateAsync(record);

            record.BaseQty = 15;

            await repository.UpdateAsync(record);

            var record2 = await repository.GetByIdAsync(id);

            Assert.AreEqual(record.Id, record2.Id);
            Assert.AreEqual(record.BaseQty, record2.BaseQty);

            await repository.DeleteAsync(record.Id);

            var record3 = await repository.GetByIdAsync(id);

            Assert.IsNull(record3);
        }
    }
}
