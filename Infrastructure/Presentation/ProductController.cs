using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    // Api Controller
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationParameters Productspec)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(Productspec);
            if (products is null) return BadRequest();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            if (product is null) return NotFound();
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            if(brands is null) return BadRequest();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            if (types is null) return BadRequest();
            return Ok(types);
        }
    }
}
