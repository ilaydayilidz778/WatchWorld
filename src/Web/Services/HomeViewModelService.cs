using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;

        public HomeViewModelService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IRepository<Brand> brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }
        public async Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId, int? brandId, int pageId)
        {
            var specProducts = new CatalogFilterSpecification(categoryId, brandId);
            var totalItems = await _productRepository.CountAsync(specProducts);

            var specProductsPaginated = new CatalogFilterSpecification(categoryId, brandId, (pageId - 1) * Constants.ITEMS_PER_PAGE, Constants.ITEMS_PER_PAGE);
            var products = await _productRepository.GetAllAsync(specProductsPaginated);

            var vm = new HomeViewModel()
            {
                PaginationInfo = new PaginationInfoViewModel
                {
                    PageId = pageId,
                    TotalItems = totalItems,
                    ItemsOnPage = products.Count()
                },
                BrandId = brandId,
                CategoryId = categoryId,
                CatologItems = products.Select(x => new CatalogItemViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    PictureUri = x.PictureUri ?? "noimage.jpg"
                }
                ).ToList(),
                Brands = (await _brandRepository.GetAllAsync()).Select(x =>
                 new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Categories = (await _categoryRepository.GetAllAsync()).Select(x =>
                 new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return vm;
        }
    }
}
