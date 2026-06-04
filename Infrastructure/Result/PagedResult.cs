namespace Infrastructure.Result;

public class PagedResult<T>
{
    public  IReadOnlyList<T>  Items {get;}
    public int TotalCount { get;}
    public int Page { get;}
    public int PageSize { get;}
    public int TotalPages  => (int)Math.Ceiling(TotalCount / (double)PageSize);
    private bool HasNextPage => Page  < TotalPages;
    private bool  HasPreviousPage => Page > 1;


     private PagedResult(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }

       public static PagedResult<T> Ok(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
        => new(items, totalCount, page, pageSize);
}
