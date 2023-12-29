namespace CoreBussiness.PaginationsService;

public class PaginationResult<T>
{
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<T>? Items { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}