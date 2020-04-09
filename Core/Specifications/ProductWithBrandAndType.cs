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

        public ProductWithBrandAndType(SortOptions sort = SortOptions.NameAsc)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (sort)
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
        }

        public ProductWithBrandAndType(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}