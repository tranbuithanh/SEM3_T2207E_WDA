namespace QShop.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 0;

        public int TotalPage()
        {
            return (int)Math.Ceiling((double)TotalItems / PageSize);
        }
    }
}
