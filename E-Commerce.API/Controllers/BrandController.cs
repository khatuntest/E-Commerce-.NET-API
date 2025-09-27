using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BrandToReturnDto>> GetBrandById(int Id)
        {
            var brandRepo = _unitOfWork.Repository<Brand>();
            var brand = brandRepo.GetAsync(Id);
            return Ok(_mapper.Map<Brand , BrandToReturnDto>(await brand));
        }

        [HttpGet]
        public async Task<ActionResult<BrandToReturnDto>> GetAllBrands()
        {
            var brandRepo = _unitOfWork.Repository<Brand>();
            var brands = await brandRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<Brand> ,IEnumerable<BrandToReturnDto>>(brands));
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<BrandToReturnDto>> GetAllProductsByBrandId(int id)
        {
            var brandRepo = _unitOfWork.Repository<Brand>();
            var brand = await  brandRepo.GetAsync(id);
        
          
            var productsOfBrand = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(brand.Products);

            return Ok(productsOfBrand);

        }
    }
}
