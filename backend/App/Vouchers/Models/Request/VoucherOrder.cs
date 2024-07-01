using System.ComponentModel;
using System.Text.Json.Serialization;

namespace backend.Vouchers.Models.Request;

[DefaultValue(Newest)]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VoucherOrder
{
    Newest,
    Code
}