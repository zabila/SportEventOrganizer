using System.Linq.Expressions;
using MongoDB.Driver;
using Sportalytics.Feed.Domain.Interfaces;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Persistence.MongoDB.Core;

public class MongoRepository<T>(IMongoDatabase database, string collectionName) : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _collection = database.GetCollection<T>(collectionName);
    private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

    public async Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken)
    {
        var foundList = await _collection.Find(filter).ToListAsync(cancellationToken);
        return foundList.AsQueryable();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, options: null, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var filter = _filterDefinitionBuilder.Eq(e => e.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        var filter = _filterDefinitionBuilder.Eq(e => e.Id, entity.Id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }
}