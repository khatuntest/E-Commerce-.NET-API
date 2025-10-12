using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
using E_Commerce.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class TypeController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<TypeController> logger;

        public TypeController(IUnitOfWork _unitOfWork , IMapper _mapper , ILogger<TypeController> _logger)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            logger = _logger;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TypeToReturnDto>> GetTypeById(int Id)
        {
            try
            {
                var TypeRepo = unitOfWork.Repository<Types>();
                var spc = new BaseSpecification<Types>(t => t.Id == Id); 
                var type =await TypeRepo.GetAsync(spc);
                if (type == null)
                {
                    logger.LogWarning($"Type with Id: {Id} Not Found");
                    return NotFound("No Type found");
                }
                var mapType = mapper.Map<TypeToReturnDto>(type);
                return Ok(mapType);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while fetching Type with Id {Id}");
                return StatusCode(500, " Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<TypeToReturnDto>> GetAllTypes()
        {
            try
            {
                var typeRepo = unitOfWork.Repository<Types>();
                var spc = new BaseSpecification<Types>();
                var types = await typeRepo.GetAllAsync(spc);
                if (types == null || !types.Any())
                {
                    logger.LogWarning("No Types found in database");
                    return NotFound("No Types found");
                }
                var mapTypes = mapper.Map<IEnumerable<TypeToReturnDto>>(types);
                return Ok(mapTypes);
            }
            catch(Exception ex)
            {
                logger.LogError(ex , $"Error while fetching Types from Database ");
                return StatusCode(500, " Internal Server Error");
            }
        }

        [HttpGet("{Id}/Products")]
        public async Task<ActionResult<TypeToReturnDto>> GetProductsByTypeId(int Id)
        {
            try
            {
                var typeRepo = unitOfWork.Repository<Types>();
                var spc = new BaseSpecification<Types>(t => t.Id == Id);
                var type = await typeRepo.GetAsync(spc);
                if (type == null)
                {
                    logger.LogWarning($"Type with Id: {Id} Not Found");
                    return NotFound("No Type found");
                }
                var Products = mapper.Map<IEnumerable<ProductToReturnDto>>(type.Products);
                return Ok(Products);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error while fetching Products from Database ");
                return StatusCode(500, " Internal Server Error");
            }
        }
    }
}
