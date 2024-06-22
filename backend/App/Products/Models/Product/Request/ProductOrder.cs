using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backend.Products.Models.Product;

[DefaultValue(Newest)]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductOrder
{
    Newest,
    Name
}