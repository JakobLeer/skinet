using System;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithBrandAndType : BaseSpecification<Product>
    {
        public enum SortOptions
        {
            PriceAsc,
            PriceDesc,
            NameAsc,
            NameDesc,
        }

        public ProductWithBrandAndType(ProductSpecParams productSpecParams)
            : base(p =>
                    (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
                    (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId) &&
                    (string.IsNullOrWhiteSpace(productSpecParams.Name) || p.Name.ToLower().Contains(productSpecParams.Name))
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (productSpecParams.Sort)
            {
                case SortOptions.NameAsc:
                    OrderBy = p => p.Name;
                    break;
                case SortOptions.NameDesc:
                    OrderByDescending = p => p.Name;
                    break;
                case SortOptions.PriceAsc:
                    OrderBy = p => p.Price;
                    break;
                case SortOptions.PriceDesc:
                    OrderByDescending = p => p.Price;
                    break;
            }

            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1),
                        productSpecParams.PageSize);
        }

        public ProductWithBrandAndType(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}