namespace Web.Models
{
    public class PaginationInfoViewModel
    {
        public int PageId { get; set; }

        public int TotalItems { get; set; }

        public int  ItemsOnPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsOnPage); // 25 ürün varsa  25/4 = 7 sayfa olması gerekir.

        public bool HasPrevious => PageId > 1 ;

        public bool HasNext => PageId < TotalPages;

        public int RangeStrat => ((PageId - 1) * ItemsOnPage) + 1;

        public int RangeEnd => RangeStrat + ItemsOnPage - 1;
    }
}
