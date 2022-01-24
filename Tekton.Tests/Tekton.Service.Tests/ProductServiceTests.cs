using Mapster;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tekton.Data.Entities;
using Tekton.Infrasttructure.Repositories;
using Tekton.Service;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Xunit;

namespace Tekton.Tests.Tekton.Service.Tests
{
    public class ProductServiceTests
    {
        private readonly IProductService _productServices;
        private readonly IRepository<Product> _productRepo;
        public ProductServiceTests()
        {
            _productRepo = Substitute.For<IRepository<Product>>();
            _productServices = new ProductService(_productRepo);
        }

        [Fact]
        public async Task Get_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var product = new Product()
            {
                Id = id,
                Master = "Master",
                Detail = "detail"
            };

            _productRepo.Get(Arg.Any<Guid>()).Returns(product);

            var result = await _productServices.Get(id);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProductDto>();
            result.ShouldBeEquivalentTo(product.Adapt<ProductDto>());
        }
        [Fact]
        public async Task Get_should_return_null()
        {
            var id = Guid.NewGuid();

            _productRepo.Get(Arg.Any<Guid>()).Returns((Product)null);

            var result = await _productServices.Get(id);

            result.ShouldBeNull();
        }
        [Fact]
        public async Task Get_should_throw_exception()
        {
            var id = Guid.NewGuid();

            _productRepo.When(x => x.Get(Arg.Any<Guid>())).Do(x => { throw new Exception(); });

            await Should.ThrowAsync<Exception>(() => _productServices.Get(id));
        }
        [Fact]
        public async Task Insert_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var productAdd = new ProductAddDto()
            {
                Master = "Master",
                Detail = "detail"
            };
            var product = productAdd.Adapt<Product>();
            product.Id = id;

            _productRepo.Add(Arg.Any<Product>()).Returns(product);

            var result = await _productServices.Insert(productAdd);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProductDto>();
            result.ShouldBeEquivalentTo(product.Adapt<ProductDto>());
        }
        [Fact]
        public async Task Post_should_throw_exception()
        {
            var id = Guid.NewGuid();

            _productRepo.When(x => x.Add(Arg.Any<Product>())).Do(x => { throw new Exception(); });

            await Should.ThrowAsync<Exception>(() => _productServices.Insert(new ProductAddDto()));
        }
        [Fact]
        public async Task Update_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var productUpdate = new ProductDto()
            {
                Id = id,
                Master = "Master",
                Detail = "detail"
            };

            _productRepo.Get(Arg.Any<Guid>()).Returns(productUpdate.Adapt<Product>());
            _productRepo.Update(Arg.Any<Product>()).Returns(productUpdate.Adapt<Product>());


            var result = await _productServices.Update(id, productUpdate);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProductDto>();
            result.ShouldBeEquivalentTo(productUpdate.Adapt<ProductDto>());
        }
        [Fact]
        public async Task Update_should_return_null()
        {
            var id = Guid.NewGuid();

            _productRepo.Get(Arg.Any<Guid>()).Returns((Product)null);

            var result = await _productServices.Update(id, new ProductDto());

            result.ShouldBeNull();
        }
        [Fact]
        public async Task Update_should_throw_exception()
        {
            var id = Guid.NewGuid();

            _productRepo.When(x => x.Get(Arg.Any<Guid>())).Do(x => { throw new Exception(); });

            await Should.ThrowAsync<Exception>(() => _productServices.Update(id, new ProductDto()));
        }
    }
}
