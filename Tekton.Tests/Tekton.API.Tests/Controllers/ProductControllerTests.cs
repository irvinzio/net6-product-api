using Mapster;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tekton.API.Controllers;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Xunit;

namespace Tekton.Tests.Tekton.API.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly IProductService _productService;
        private readonly ProductController _productController;
        public ProductControllerTests()
        {
            _productService = Substitute.For<IProductService>();
            _productController = new ProductController(_productService);
        }

        [Fact]
        public async Task Get_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var product = new ProductDto()
            {
                Id = id,
                Master = "Master",
                Detail = "detail"
            };

            _productService.Get(Arg.Any<Guid>()).Returns(product);

            var result = await _productController.Get(id) as  OkObjectResult;

            result.ShouldNotBeNull();
            result.Value.ShouldBeSameAs(product);
        }
        [Fact]
        public async Task Get_should_return_notFound()
        {
            var id = Guid.NewGuid();

            _productService.Get(Arg.Any<Guid>()).Returns((ProductDto)null);

            var result = await _productController.Get(id);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Post_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var productAdd = new ProductAddDto()
            {
                Master = "Master",
                Detail = "detail"
            };
            var productDto = new ProductAddDto().Adapt<ProductDto>();
            productDto.Id = id;

            _productService.Insert(Arg.Any<ProductAddDto>()).Returns(productDto);

            var result = await _productController.Post(productAdd) as OkObjectResult;

            result.ShouldNotBeNull();
            result.Value.ShouldBeSameAs(productDto);
        }
        [Fact]
        public async Task Update_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var product = new ProductDto()
            {
                Id = id,
                Master = "Master",
                Detail = "detail"
            };

            _productService.Update(Arg.Any<Guid>(),Arg.Any<ProductDto>()).Returns(product);

            var result = await _productController.Put(id,product) as OkObjectResult;

            result.ShouldNotBeNull();
            result.Value.ShouldBeSameAs(product);
        }
        [Fact]
        public async Task Update_should_return_notFound()
        {
            var id = Guid.NewGuid();
            var product = new ProductDto();

            _productService.Get(Arg.Any<Guid>()).Returns((ProductDto)null);

            var result = await _productController.Put(id, product);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
