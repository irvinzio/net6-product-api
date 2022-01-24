using Microsoft.AspNetCore.Mvc;
using Tekton.Service.Dto;
using Tekton.Service.Interfaces;

namespace Tekton.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.Get(id);

            if(product == null) { return NotFound(); }

            return Ok(product);
        }

        // POST api/<Product>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto product)
        {
            return Ok(await _productService.Insert(product));
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProductDto product)
        {
            var updatedProduct = await _productService.Update(id, product);

            if (updatedProduct == null) { return NotFound(); }

            return Ok(updatedProduct);
        }
    }
}
