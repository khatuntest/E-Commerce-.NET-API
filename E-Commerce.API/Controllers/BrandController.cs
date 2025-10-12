using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Commerce.API.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<BrandController> logger;

        public BrandController(IUnitOfWork _unitOfWork , IMapper _mapper , ILogger<BrandController> _logger)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            logger = _logger;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BrandToReturnDto>> GetBrandById(int Id)
        {
            try
            {
                var brandRepo = unitOfWork.Repository<Brand>();
                var spc = new BaseSpecification<Brand>(p => p.Id == Id);
                var brand = await brandRepo.GetAsync(spc);
                if (brand == null)
                {
                    logger.LogWarning($"Brand with Id: {Id} Not Found");
                    return NotFound("No Brand found");
                }
                var mapBrand = mapper.Map<BrandToReturnDto>(brand);
                return Ok(mapBrand);
            }catch (Exception ex)
            {
                logger.LogError(ex, $"Error while fetching Brand with Id {Id}");
                return StatusCode(500, " Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<BrandToReturnDto>> GetAllBrands()
        {
            try
            {
                var brandRepo = unitOfWork.Repository<Brand>();
                var spc = new BaseSpecification<Brand>();
                var brands = await brandRepo.GetAllAsync(spc);
                if (brands == null || !brands.Any())
                {
                    logger.LogWarning("No Brands found in database");
                    return NotFound("No Brands found");
                }
                var mapBrands = mapper.Map<IEnumerable<BrandToReturnDto>>(brands);

                return Ok(mapBrands);
            }catch(Exception ex)
            {
                logger.LogError(ex , $"Error while fetching Brands from Database ");
                return StatusCode(500, " Internal Server Error");
            }
        }

        [HttpGet("{id}/Products")]
        public async Task<ActionResult<BrandToReturnDto>> GetAllProductsByBrandId(int id)
        {
            try
            {
                var brandRepo = unitOfWork.Repository<Brand>();
                var spc = new BaseSpecification<Brand>(b => b.Id == id);
                var brand = await brandRepo.GetAsync(spc);
                if (brand == null)
                {
                    logger.LogWarning($"Brand with Id: {id} Not Found");
                    return NotFound("No Brand found");
                }

                var productsOfBrand = mapper.Map<IEnumerable<ProductToReturnDto>>(brand.Products);

                return Ok(productsOfBrand);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error while fetching Products from Database ");
                return StatusCode(500, " Internal Server Error");
            }

        }
    }
}
