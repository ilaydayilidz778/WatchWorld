using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class CatalogFilterSpecification : Specification<Product>
    {
        public CatalogFilterSpecification(int? categoryId, int? brandId)
        {
            if (categoryId.HasValue)
                Query.Where(x => x.CategoryId == categoryId);

            if (brandId.HasValue)
                Query.Where(x => x.BrandId == brandId);
        }
    }
}
