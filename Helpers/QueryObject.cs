namespace WebApplication1.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsDecending { set; get; } = false;
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
