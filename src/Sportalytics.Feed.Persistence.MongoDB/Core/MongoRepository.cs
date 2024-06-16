using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Sportalytics.Feed.Domain.Interfaces;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Persistence.MongoDB.Core;

public class MongoRepository<T>(IMongoDatabase database, string collectionName) : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _collection = database.GetCollection<T>(collectionName);
    private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

    public IFindFluent<T, T> Query(Expression<Func<T, bool>> filter)
    {
        var foundList = _collection.Find(filter);
        return foundList;
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