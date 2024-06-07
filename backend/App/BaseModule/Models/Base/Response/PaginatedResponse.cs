namespace backend.BaseModule.Models.Base;

public class PaginatedResponse<T>
{
    public string Message { get; set; }
    public IEnumerable<T> Data { get; set; }
    public int Page { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
}