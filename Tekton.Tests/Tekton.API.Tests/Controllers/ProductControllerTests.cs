using Mapster;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tekton.API.Controllers;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Tekton.Tests.Extensions;
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
            var product = ProductDtoExtension.GetProductDto(id);

            _productService.Get(Arg.Any<Guid>()).Returns(product);

            var response = await _productController.Get(id);
            var result = response.Result as OkObjectResult;

            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkObjectResult>();
            result.Value.ShouldBeSameAs(product);
        }
        [Fact]
        public async Task Get_should_return_notFound()
        {
            var id = Guid.NewGuid();

            _productService.Get(Arg.Any<Guid>()).Returns((ProductDto)null);

            var response = await _productController.Get(id);

            response.ShouldNotBeNull();
            response.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Post_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var productAdd = ProductDtoExtension.GetProductAddDto();
            var productDto = new ProductAddDto().Adapt<ProductDto>();
            productDto.Id = id;

            _productService.Insert(Arg.Any<ProductAddDto>()).Returns(productDto);

            var response = await _productController.Post(productAdd);
            var result = response.Result as OkObjectResult;

            result.ShouldNotBeNull();
            result.Value.ShouldBeSameAs(productDto);
        }
        [Fact]
        public async Task Update_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var product = ProductDtoExtension.GetProductDto(id);

            _productService.Update(Arg.Any<Guid>(), Arg.Any<ProductDto>()).Returns(product);

            var response = await _productController.Put(id, product);
            var result = response.Result as OkObjectResult;

            result.ShouldNotBeNull();
            result.Value.ShouldBeSameAs(product);
        }
        [Fact]
        public async Task Update_should_return_notFound()
        {
            var id = Guid.NewGuid();
            var product = new ProductDto() { Id = id };

            _productService.Update(Arg.Any<Guid>(), Arg.Any<ProductDto>()).Returns((ProductDto)null);

            var result = await _productController.Put(id, product);

            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task Update_should_return_BadRequest()
        {
            var id = Guid.NewGuid();
            var product = new ProductDto();

            _productService.Update(Arg.Any<Guid>(), Arg.Any<ProductDto>()).Returns((ProductDto)null);

            var result = await _productController.Put(id, product);

            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<BadRequestResult>();
        }
    }
}
