using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
