using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Example.Serde;

// This type is for playing with JSON and XML serializer.
// Hence, no property or field explicitly access.
// ReSharper disable all NotAccessedPositionalProperty.Global
[Serializable]
public record Person(string Name, DateTime Date, List<Person>? Friends = null)
{
    // ReSharper disable once UnusedMember.Local
    // required by XML serialization
    private Person()
        : this(string.Empty, DateTime.Now) { }

    // ignored because I don't know how to fix ref-cycle in XML
    [XmlIgnore]
    public List<Person>? Friends { get; set; } = Friends;

    private static readonly JsonSerializerOptions JsonOption =
        new()
        {
            WriteIndented = true,
            // resolve reference cycle
            ReferenceHandler = ReferenceHandler.Preserve,
            TypeInfoResolver = CustomJsonContext.Default,
        };

#pragma warning disable IL2026, IL3050
    // IL2026 and IL3050 are false positive, confirmed.
    // https://github.com/dotnet/runtime/issues/51544#issuecomment-1516232559
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, typeof(Person), JsonOption);
#pragma warning restore IL2026, IL3050
    }
}
