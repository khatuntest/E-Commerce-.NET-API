using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.API.Filters;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [ServiceFilter(typeof(ValidationFilter))]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ILogger<ProductController> logger;

        public ProductController(IUnitOfWork _unitOfWork , IMapper _mapper ,ILogger<ProductController> _logger, IProductService _productService)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            productService = _productService;
            logger = _logger;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetById([FromRoute]int Id)
        {
            try
            {
                var productRepo = unitOfWork.Repository<Product>();
                var spec = new ProductSpecification(Id);
                var product = await productRepo.GetAsync(spec);
                if (product == null)
                {
                    logger.LogWarning($"Product with Id: {Id} Not Found");
                    return NotFound("No product found");
                }
                var mapProduct = mapper.Map<ProductToReturnDto>(product);
                return Ok(mapProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while fetching product with Id {Id}");
                return StatusCode(500, " Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<ProductToReturnDto>> GetAll()
        {
            try
            {
                var productsRepo = unitOfWork.Repository<Product>();
                var spc = new ProductSpecification();
                var products = await productsRepo.GetAllAsync(spc);
                if (products == null || !products.Any())
                {
                    logger.LogWarning("No products found in database");
                    return NotFound("No products found");
                }
                var mapProducts = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
                return Ok(mapProducts);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Error while fetching Products from Database ");
                return StatusCode(500, " Internal Server Error");
            }
        }

        [HttpGet("GetProductWithSpecificForm")]
        public async Task<ActionResult<ProductToReturnDto>> GetAllWithFilter([FromQuery]ProductQueryParametersDtos parameters)
        {
            try
            {
                var Products = await productService.GetAllWithFilter(parameters);
                if (Products == null || !Products.Any())
                {
                    logger.LogWarning("No products found in database");
                    return NotFound("No products found");
                }
                var mapProducts = mapper.Map<IEnumerable<ProductToReturnDto>>(Products);
                return Ok(mapProducts);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while fetching Products from Database ");
                return StatusCode(500, " Internal Server Error");
            }

        }

       [HttpGet("Test")]
        public async Task<IActionResult> TestException()
        {
           await productService.ThrowTestException();
            return Ok("Not reach to this line");
        }

        
        

    }
}
