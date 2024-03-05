using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models
{
    public class HomeViewModel
    {
        public List<CatalogItemViewModel> CatologItems { get; set; } = new List<CatalogItemViewModel>();

        public List<SelectListItem>? Brands { get; set; } 

        public List<SelectListItem>? Categories { get; set; } 

        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public PaginationInfoViewModel PaginationInfo { get; set; } = null!;
    }
}
