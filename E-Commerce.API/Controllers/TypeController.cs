using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class TypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypeController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TypeToReturnDto>> GetTypeById(int Id)
        {
            var typeRepo = _unitOfWork.Repository<Types>();
            var type = typeRepo.GetAsync(Id);
            return Ok(_mapper.Map<Types , TypeToReturnDto>(await type));
        }

        [HttpGet]
        public async Task<ActionResult<TypeToReturnDto>> GetAllTypes()
        {
            var typeRepo = _unitOfWork.Repository<Types>();
            var types = typeRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<Types> , IEnumerable<TypeToReturnDto>>(await types));
        }

        [HttpGet("{Id}/products")]
        public async Task<ActionResult<TypeToReturnDto>> GetProductsByTypeId(int Id)
        {
            var typeRepo = _unitOfWork.Repository<Types>();
            var type = await typeRepo.GetAsync(Id);
            var Products = _mapper.Map<IEnumerable<Product> ,IEnumerable<ProductToReturnDto>>(type.Products);
            return Ok(Products);
        }
    }
}
