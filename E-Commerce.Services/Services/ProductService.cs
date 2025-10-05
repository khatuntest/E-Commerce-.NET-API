using E_Commerce.API.Dtos;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Services.Interfaces;
using E_Commerce.Services.Specifications;
using E_Commerce.Infrastructure;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(ILogger<ProductService>  logger , IUnitOfWork unitOfWork) 
        { 
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<(IReadOnlyList<Product> Products, int TotalCount)> 
            GetProductsAsync(ProductQueryParameters parameters)
        {
            _logger.LogInformation("Fetching Product with specification ");

            int pageNumber = parameters.PageNumber ?? 1;
            int pageSize = parameters.PageSize ?? 10;
            decimal minPrice = 0;
            decimal maxPrice = decimal.MaxValue;

            var spc = new ProductSpecification(parameters.Search, minPrice, maxPrice, parameters.Sort)
            {
                Skip = (pageNumber - 1) * pageSize,
                Take = pageSize
            };

            var allProducts = await _unitOfWork.Repository<Product>().GetAllAsync();
            var totalItems = allProducts.Count();
            var products = allProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            return (products, totalItems);

        }

        public async Task ThrowTestException()
        {
            await Task.Delay(10);
            _logger.LogInformation("To Throw Test exception");
            new Exception("Throw Test Exception");

        }
    }
}
