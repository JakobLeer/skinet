using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<ProductBrand> _productBrandRepo;
        private readonly IRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IRepository<Product> productRepo,
            IRepository<ProductBrand> productBrandRepo,
            IRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Paginator<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var products = await _productRepo.ListBySpecAsync(new ProductWithBrandAndType(productSpecParams)).ConfigureAwait(false);
            int count = await _productRepo.CountBySpecAsync(new ProductCount(productSpecParams)).ConfigureAwait(false);
            var dtos = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var paginatedList = new Paginator<ProductToReturnDto>(
                productSpecParams.PageIndex,
                productSpecParams.PageSize,
                count,
                dtos);
            return Ok(paginatedList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndType(id);
            var product = await _productRepo.GetBySpecAsync(spec).ConfigureAwait(false);

            if (product == null) return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

            var productDto = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(productDto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandRepo.ListAllAsync().ConfigureAwait(false);
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _productTypeRepo.ListAllAsync().ConfigureAwait(false);
            return Ok(types);
        }
    }
}