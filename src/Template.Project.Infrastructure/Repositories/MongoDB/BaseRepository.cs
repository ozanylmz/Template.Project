using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using Template.Project.Domain.Interfaces;
using Template.Project.Domain.SeedWork;
using Template.Project.Infrastructure.DbConfigurations.MongoDB;

namespace Template.Project.Infrastructure.Repositories.MongoDB
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : MongoBaseEntity
    {
        protected readonly IMongoCollection<T> Collection;
        private readonly MongoSettings connection;

        protected BaseRepository(IOptions<MongoSettings> options)
        {
            this.connection = options.Value;
            var client = new MongoClient(this.connection.ConnectionString);
            var db = client.GetDatabase(this.connection.DatabaseName);
            this.Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public virtual async Task<List<T>> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? await Collection.AsQueryable().ToListAsync()
                : Collection.AsQueryable().Where(predicate).ToList();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<T>>)entities, options)).IsAcknowledged;
        }

        public virtual async Task<T> UpdateAsync(string id, T entity)
        {
            return await Collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
        }

        public virtual async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public virtual async Task<T> DeleteAsync(string id)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            return await Collection.FindOneAndDeleteAsync(filter);
        }
    }
}
