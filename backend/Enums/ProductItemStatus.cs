using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backend.Entities;

[DefaultValue(Available)]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductItemStatus
{
    Available,
    Unavailable
}