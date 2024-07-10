using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backend.Entities;

[DefaultValue(Banner)]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Beritatype
{
    Banner,
}