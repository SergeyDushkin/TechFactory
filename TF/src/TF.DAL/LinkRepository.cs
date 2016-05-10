using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TF.Data.Systems;
using TF.DAL.Query;

namespace TF.DAL
{
    public class LinkRepository : ILinkRepository
    {
        private readonly NoodleDbContext context;

        public LinkRepository(NoodleDbContext context)
        {
            this.context = context;
        }

        public async Task<Link> CreateAsync(Link link)
        {
            if (link.Id == Guid.Empty)
                link.Id = Guid.NewGuid();

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(LinkQuery.Insert(link));
                return link;
            }
        }

        public async Task<Link> UpdateAsync(Link link)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(LinkQuery.Update(link));
                return link;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(LinkQuery.Delete(id));
            }
        }

        public async Task<IEnumerable<Link>> GetByReferenceIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                return await connection.QueryAsync<Link>(LinkQuery.ByReferenceId(id));
            }
        }

        public async Task<Link> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                var query = await connection.QueryAsync<Link>(LinkQuery.ById(id));
                
                return query.SingleOrDefault();
            }
        }
    }
}
