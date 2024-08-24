using System.Xml.Serialization;
using Example.Serde;

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

WriteLine();

// XML serialized force this to be public,
// because we don't use source gen like JSON.
