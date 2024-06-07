using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backend.Products.Models.Category;

[DefaultValue(Newest)]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CategoryOrder
{
    Newest,
    Name
}