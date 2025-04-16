﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) :ControllerBase
    {
        // Endpoint : Public non-static method

        // Sort : nameasc [default]
        // Sort : namedasc
        // Sort : priceasc
        // Sort : pricedesc

        [HttpGet] //GET: /api/products 
        public async Task<IActionResult>GetAllProducts([FromQuery]ProductSpecificationsParamters specParams)
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync(specParams);
            if (result is null) return BadRequest(); // 400

            return Ok(result); // 200
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetProductById (int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        [HttpGet ("brands")]
        public async Task <IActionResult> GetAllBrands()
        {
            var result = await serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }


    }
}
