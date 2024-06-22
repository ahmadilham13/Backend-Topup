namespace backend.BaseModule.Models.Base;

public class ListFilter
{
    public int? Limit { get; set; } = 20;
    #nullable enable
    public string? Query { get; set; }
    public string? Includes { get; set; }
}