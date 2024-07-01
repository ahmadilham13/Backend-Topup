using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backend.Entities;

[DefaultValue(Active)]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductStatus
{
    Active,
    NonActive
}