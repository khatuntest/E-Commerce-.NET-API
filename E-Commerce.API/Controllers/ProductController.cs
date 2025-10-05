using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.API.Filters;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [ServiceFilter(typeof(ValidationFilter))]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IUnitOfWork unitOfWork , IMapper mapper , IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetById(int Id)
        {
            var productRepo = _unitOfWork.Repository<Product>();
            var product = productRepo.GetAsync(Id);
            return Ok(_mapper.Map<Product , ProductToReturnDto>(await product));
        }

        [HttpGet]
        public async Task<ActionResult<ProductToReturnDto>> GetAll()
        {
            var productsRepo = _unitOfWork.Repository<Product>();
            var products = await productsRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<Product> ,IEnumerable<ProductToReturnDto>>(products));
        }

        [HttpGet("Test")]
        public async Task<IActionResult> TestException()
        {
           await _productService.ThrowTestException();
            return Ok("Not reach to this line");
        }

        [HttpGet("GetProductService")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            var (products, totalCount) = await _productService.GetProductsAsync(parameters);

            return Ok(new
            {
                TotalCount = totalCount,
                Data = products
            });
        }

    }
}
