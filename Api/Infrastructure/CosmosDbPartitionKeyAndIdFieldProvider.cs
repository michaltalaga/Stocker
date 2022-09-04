using Microsoft.Azure.Cosmos;

namespace Api.Infrastructure;

public class CosmosDbPartitionKeyAndIdFieldProvider : ICosmosDbPartitionKeyAndIdFieldProvider
{
    public string GetId<T>(T item)
    {
        return item.GetType().GetProperty("id").GetValue(item) as string;
    }

    public PartitionKey GetPartitionKey<T>(T item)
    {
        return new PartitionKey(item.GetType().GetProperty("OwnerEmail").GetValue(item) as string);
    }
}