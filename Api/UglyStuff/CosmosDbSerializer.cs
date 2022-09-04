using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.UglyStuff
{
    using System.IO;
    using System.Text.Json;
    using Azure.Core.Serialization;
    using Microsoft.Azure.Cosmos;

    /// <remarks>
    // See: https://github.com/Azure/azure-cosmos-dotnet-v3/blob/master/Microsoft.Azure.Cosmos.Samples/Usage/SystemTextJson/CosmosSystemTextJsonSerializer.cs
    /// </remarks>
    public sealed class CosmosDbSerializer : CosmosSerializer
    {
        private readonly JsonObjectSerializer systemTextJsonSerializer;

        public CosmosDbSerializer() : this(new JsonSerializerOptions())
        {

        }
        public CosmosDbSerializer(JsonSerializerOptions jsonSerializerOptions)
        {
            systemTextJsonSerializer = new JsonObjectSerializer(jsonSerializerOptions);
        }

        public override T FromStream<T>(Stream stream)
        {
            if (stream.CanSeek && stream.Length == 0)
            {
                return default;
            }

            if (typeof(Stream).IsAssignableFrom(typeof(T)))
            {
                return (T)(object)stream;
            }

            using (stream)
            {
                return (T)systemTextJsonSerializer.Deserialize(stream, typeof(T), default);
            }
        }

        public override Stream ToStream<T>(T input)
        {
            var streamPayload = new MemoryStream();
            systemTextJsonSerializer.Serialize(streamPayload, input, typeof(T), default);
            streamPayload.Position = 0;
            return streamPayload;
        }
    }
}
