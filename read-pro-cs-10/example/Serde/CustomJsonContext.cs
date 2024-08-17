using System.Text.Json;
using System.Text.Json.Serialization;

namespace Example;

#pragma warning disable S1135
// TODO (tai): can this be simpler, like a single annotation on the class or record itself?
#pragma warning restore S1135

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(JsonElement))] // add this
public partial class CustomJsonContext : JsonSerializerContext;
