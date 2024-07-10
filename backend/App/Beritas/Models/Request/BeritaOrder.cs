using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backend.Beritas.Models.Request;

[DefaultValue(Newest)]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BeritaOrder
{
    Newest,
    Name
}