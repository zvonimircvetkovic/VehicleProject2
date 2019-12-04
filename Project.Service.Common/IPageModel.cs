namespace Project.Service.Common
{
    public interface IPageModel
    {
        int CurrentPage { get; set; }
        bool HasNext { get; }
        bool HasPrevious { get; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        int TotalPages { get; set; }
    }
}