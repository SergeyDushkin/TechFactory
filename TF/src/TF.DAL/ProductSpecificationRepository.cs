using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.DAL.Query;
using TF.Data.Business.WMS;
using TF.DAL.Models;

namespace TF.DAL
{
    public class ProductSpecificationRepository : IProductSpecificationRepository
    {
        private readonly NoodleDbContext context;

        public ProductSpecificationRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public async Task<ProductSpecification> CreateAsync(ProductSpecification specification)
        {
            if (specification.Id == Guid.Empty)
                specification.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(ProductSpecificationQuery.Insert(ToModel(specification)));
                return specification;
            }
        }

        public async Task<ProductSpecification> UpdateAsync(ProductSpecification specification)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(ProductSpecificationQuery.Update(ToModel(specification)));
                return specification;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(ProductSpecificationQuery.Delete(id));
            }
        }

        public async Task<ProductSpecification> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<BUSINESS_WMS_KIT_SPEC>(ProductSpecificationQuery.ById(id));

                return query.Select(r => ToDTO(r)).SingleOrDefault();
            }
        }

        public async Task<IEnumerable<ProductSpecification>> GetByParentIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<BUSINESS_WMS_KIT_SPEC>(ProductSpecificationQuery.ByParentId(id));
                return query.Select(r => ToDTO(r)).ToList();
            }
        }

        public async Task<IEnumerable<ProductSpecification>> GetByChildIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<BUSINESS_WMS_KIT_SPEC>(ProductSpecificationQuery.ByChildId(id));
                return query.Select(r => ToDTO(r)).ToList();
            }
        }

        private ProductSpecification ToDTO(BUSINESS_WMS_KIT_SPEC record)
        {
            return new ProductSpecification
            {
                BaseQty = record.BASE_QTY,
                Child = new Product
                {
                    Id = record.CHILD_GUID
                },
                ChildUom = new Data.Business.Uom
                {
                    Id = record.CHILD_UOM_GUID
                },
                Id = record.GUID_RECORD,
                Parent = new Product
                {
                    Id = record.PARENT_GUID
                }
            };
        }

        private BUSINESS_WMS_KIT_SPEC ToModel(ProductSpecification record)
        {
            return new BUSINESS_WMS_KIT_SPEC
            {
                BASE_QTY = record.BaseQty,
                BATCH_GUID = null,
                CHILD_GUID = record.Child.Id,
                CHILD_UOM_GUID = record.ChildUom.Id,
                DELETED = false,
                GUID_RECORD = record.Id,
                HIDDEN = false,
                PARENT_GUID = record.Parent.Id
            };
        }
    }
}
