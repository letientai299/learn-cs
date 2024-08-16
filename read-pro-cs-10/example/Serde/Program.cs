using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using static Utils.Logs;

var a = new Person("A", DateTime.Now.AddDays(-1));
var p = new Person(
    "Tai",
    DateTime.Now,
    [
        a,
        new Person("B", DateTime.Now.AddDays(1)),
        new Person("C", DateTime.Now.AddYears(10)),
        new Person("D", DateTime.Now.AddMonths(-1)),
    ]
);

a.Friends = [p];
Log(p);
Log(a);

// Don't know how to config source generator for XML like JSON yet.
// But, I don't care much as I probably won't use XML any soon.
#pragma warning disable IL2026
var xml = new XmlSerializer(typeof(Person));
var buf = new StringWriter();
xml.Serialize(buf, p);
#pragma warning restore IL2026
Log(buf);

Console.WriteLine();

// XML serialized force this to be public,
// because we don't use source gen like JSON.
[Serializable]
public record Person(string Name, DateTime Date, Person[]? Friends = null)
{
    // ReSharper disable once UnusedMember.Local
    // required by XML serialization
    private Person()
        : this(string.Empty, DateTime.Now) { }

    // ignored because I don't know how to fix ref-cycle in XML
    [XmlIgnore]
    public Person[]? Friends { get; set; } = Friends;

#pragma warning disable IL2026
#pragma warning disable IL3050
    // IL2026 and IL3050 are false positive, confirmed.
    // https://github.com/dotnet/runtime/issues/51544#issuecomment-1516232559
    public override string ToString()
    {
        return JsonSerializer.Serialize(
            this,
            typeof(Person),
            new JsonSerializerOptions
            {
                WriteIndented = true,
                // resolve reference cycle
                ReferenceHandler = ReferenceHandler.Preserve,
                TypeInfoResolver = CustomJsonContext.Default,
            }
        );
#pragma warning restore IL2026
#pragma warning disable IL3050
    }
}

// TODO (tai): can this be simpler, like a single annotation on the class or record itself?
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(JsonElement))] // add this
public partial class CustomJsonContext : JsonSerializerContext { }
