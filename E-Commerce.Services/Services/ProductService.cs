
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
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
        private readonly ILogger<ProductService> logger;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(ILogger<ProductService>  _logger , IUnitOfWork _unitOfWork) 
        { 
            logger = _logger;
            unitOfWork = _unitOfWork;
        }

        public async Task<Product> CreateProduct()
        {
            var product = new Product
            {
                Name = "Test Product",
                Description = "This is a test product",
                Price = 100,
                PictureUrl = "test.jpg",
                ProductBrandId = 1,
                ProductTypeId = 1
            };

            try
            {
                logger.LogInformation("Starting test creation of product: {ProductName}", product.Name);
                await unitOfWork.Repository<Product>().Add(product);
                unitOfWork.Complete();

                logger.LogInformation("Test product created successfully with Id: {ProductId}", product.Id);

                return product;


            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error occurred while creating test product: {ProductName}", product.Name);
                throw;
            }

        }


        public async Task<IEnumerable<Product>> GetAllWithFilter (ProductQueryParametersDtos parameters)
        {
            try
            {
                var productRepo = unitOfWork.Repository<Product>();
                var spc = new ProductSpecification(parameters);
                var Products = await productRepo.GetAllAsyncWithFilter(spc);
                if (Products == null || !Products.Any())
                {
                    logger.LogWarning("No products found in database");
                    return Enumerable.Empty<Product>();
                }
               return Products;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while fetching Products from Database ");
                throw;
            }

        }

        public async Task ThrowTestException()
        {
            await Task.Delay(10);
            logger.LogInformation("To Throw Test exception");
            new Exception("Throw Test Exception");

        }
    }
}
