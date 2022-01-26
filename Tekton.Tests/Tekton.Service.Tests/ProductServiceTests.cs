using LazyCache;
using Mapster;
using NSubstitute;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tektok.Infrastructure.Repositories.MockApi;
using Tekton.Data.Entities;
using Tekton.Infrastructure.Repositories;
using Tekton.Service;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;
using Xunit;
using Tekton.Tests.Extensions;
using LazyCache.Testing.NSubstitute;
using Tekton.Service.Extensions;
using AutoFixture;
using Microsoft.Extensions.Caching.Memory;

namespace Tekton.Tests.Tekton.Service.Tests
{
    public class ProductServiceTests
    {
        private readonly IProductService _productServices;
        private readonly IRepository<Product> _productRepo;
        private readonly IMockApiRepository _mockApiRepo;
        private readonly IAppCache _cache;
        public ProductServiceTests()
        {
            _productRepo = Substitute.For<IRepository<Product>>();
            _mockApiRepo = Substitute.For<IMockApiRepository>();

            _cache = Create.MockedCachingService();

            _productServices = new ProductService(_productRepo, _mockApiRepo, _cache);
        }

        [Fact]
        public async Task Get_should_return_product_by_id()
        {
            var id = Guid.NewGuid();
            var product = Extensions.ProductExtension.GetProduct(id);
            var mockApiModel = MockApiModelExtension.GetMockApiModel();

            _productRepo.Get(Arg.Any<Guid>()).Returns(product);
            _mockApiRepo.Get(Arg.Any<int>()).Returns(mockApiModel);

            _cache.GetOrAddAsync(Arg.Any<string>(), Arg.Any<Func<ICacheEntry, Task<MockApiProductModel>>>()).Returns(mockApiModel);


            var result = await _productServices.Get(id);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProductDto>();
            result.ShouldBeEquivalentTo(product.ToDto(mockApiModel));
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
            var productAdd = ProductDtoExtension.GetProductAddDto();
            var mockApiModel = MockApiModelExtension.GetMockApiModel();

            var product = productAdd.Adapt<Product>();
            product.Id = id;

            _productRepo.Add(Arg.Any<Product>()).Returns(product);
            _mockApiRepo.Add(Arg.Any<MockApiProductModel>()).Returns(mockApiModel);

            var result = await _productServices.Insert(productAdd);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProductDto>();
            result.ShouldBeEquivalentTo(product.ToDto(mockApiModel));
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
            var productUpdate =  ProductDtoExtension.GetProductDto(id);
            var mockApiModel = MockApiModelExtension.GetMockApiModel();

            _productRepo.Get(Arg.Any<Guid>()).Returns(productUpdate.Adapt<Product>());
            _productRepo.Update(Arg.Any<Product>()).Returns(productUpdate.Adapt<Product>());
            _mockApiRepo.Update(Arg.Any<MockApiProductModel>()).Returns(mockApiModel);


            var result = await _productServices.Update(id, productUpdate);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProductDto>();
            result.ShouldBeEquivalentTo(productUpdate.Adapt<Product>().ToDto(mockApiModel));
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
