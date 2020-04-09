using System;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6
        ;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(value, MaxPageSize);
        }
        
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public ProductWithBrandAndType.SortOptions Sort { get; set; } = ProductWithBrandAndType.SortOptions.NameAsc;
    }
}