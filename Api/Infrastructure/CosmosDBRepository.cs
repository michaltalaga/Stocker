using Microsoft.Azure.Cosmos;

namespace Api.Infrastructure;
public class CosmosDBRepository : IRepository
{
	private readonly Database database;
	private readonly ICosmosDbPartitionKeyAndIdFieldProvider cosmosDbPartitionKeyAndIdFieldProvider;

	public CosmosDBRepository(Database database, ICosmosDbPartitionKeyAndIdFieldProvider cosmosDbPartitionKeyAndIdFieldProvider)
    {
        this.database = database;
		this.cosmosDbPartitionKeyAndIdFieldProvider = cosmosDbPartitionKeyAndIdFieldProvider;
	}
    Container GetContainerFor<T>() => database.GetContainer(typeof(T).Name);
	
	public async Task Add<T>(T item)
	{
        var container = GetContainerFor<T>();
		await container.CreateItemAsync(item);
	}

	//public async Task<T> GetById<T>(string id)
	//{
	//	var container = GetContainerFor<T>();
	//	return await container.ReadItemAsync<T>(id, new PartitionKey(id));
	//}

	public IQueryable<T> Query<T>()
	{
		var container = GetContainerFor<T>();
		return container.GetItemLinqQueryable<T>(true);
	}

	public async Task Update<T>(T item)
	{
		var container = GetContainerFor<T>();
		await container.UpsertItemAsync(item);
	}
	public async Task Delete<T>(T item)
	{
		var container = GetContainerFor<T>();
		var id = cosmosDbPartitionKeyAndIdFieldProvider.GetId(item);
		var partitionKey = cosmosDbPartitionKeyAndIdFieldProvider.GetPartitionKey(item);
        await container.DeleteItemAsync<T>(id, partitionKey);
	}
}