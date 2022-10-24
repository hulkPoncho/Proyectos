using Newtonsoft.Json;
using System.Collections.Generic;

public class Person
{
    [JsonProperty("@id")]
    public string Id { get; set; }
    public string name { get; set; }
    public string url { get; set; }
}

public class Root
{
    [JsonProperty("?xml")]
    public Xml Xml { get; set; }
    public Root2 root { get; set; }
}

public class Root2
{
    public List<Person> person { get; set; }
}

public class Xml
{
    [JsonProperty("@version")]
    public string Version { get; set; }

    [JsonProperty("@standalone")]
    public string Standalone { get; set; }
}

