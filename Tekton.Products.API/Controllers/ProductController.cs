using Microsoft.AspNetCore.Mvc;
using Tekton.API.Filters;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;

namespace Tekton.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ModelValidation]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
        {
            var product = await _productService.Get(id);

            if(product == null) { return NotFound(); }

            return Ok(product);
        }

        // POST api/<Product>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromBody] ProductAddDto product)
        {
            return Ok(await _productService.Insert(product));
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Put(Guid id, [FromBody] ProductDto product)
        {
            if (id != product.Id) { return BadRequest(); }

            var updatedProduct = await _productService.Update(id, product);

            if (updatedProduct == null) { return NotFound(); }

            return Ok(updatedProduct);
        }
    }
}
