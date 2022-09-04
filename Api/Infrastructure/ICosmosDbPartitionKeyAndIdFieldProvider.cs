using Microsoft.Azure.Cosmos;

namespace Api.Infrastructure;

public interface ICosmosDbPartitionKeyAndIdFieldProvider
{
    PartitionKey GetPartitionKey<T>(T item);
    string GetId<T>(T item);
}
