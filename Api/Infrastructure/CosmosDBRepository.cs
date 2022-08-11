using Microsoft.Azure.Cosmos;

namespace Api.Infrastructure;
public class CosmosDBRepository : IRepository
{
	private readonly Database database;
    public CosmosDBRepository(Database database)
    {
        this.database = database;
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
}