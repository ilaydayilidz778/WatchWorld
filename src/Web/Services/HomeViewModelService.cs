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
        public async Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId, int? brandId)
        {
            var specProducts = new CatalogFilterSpecification(categoryId, brandId);
            var products = await _productRepository.GetAllAsync(specProducts);  

            var vm = new HomeViewModel()
            {
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
                Brands = (await _productRepository.GetAllAsync()).Select(x =>
                 new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Categories = (await _categoryRepository.GetAllAsync()).Select(x =>
                 new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return vm;
        }
    }
}
