﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Example.Serde;

// TODO (tai): can this be simpler, like a single annotation on the class or record itself?
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(JsonElement))] // add this
public partial class CustomJsonContext : JsonSerializerContext;
