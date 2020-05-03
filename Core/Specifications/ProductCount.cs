using Core.Entities;

namespace Core.Specifications
{
    public class ProductCount : BaseSpecification<Product>
    {
        public ProductCount(ProductSpecParams productSpecParams)
            : base(p =>
                    (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
                    (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId) &&
                    (string.IsNullOrWhiteSpace(productSpecParams.Name) || p.Name.ToLower().Contains(productSpecParams.Name))
            )
        {
        }
    }
}