﻿using AutoMapper;
using Ecom.Core.DTos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Shared;
using Ecom.WebApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet("GetAllProducts")]     
        public IActionResult GetAllProducts([FromQuery] ProductParams productParams)
        {
            var products = unitOfWork.Product.GetAll(productParams);
            try
            {
                if(products.Item1!=null)
                  return Ok(new Core.Shared.Paganition<ProductDto>( products.Item1, productParams.PageNumber,productParams.PageSize,products.count));
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }
            return BadRequest("there is No products ");
        }
        [HttpGet("GetAllProductsWithCategories")]
        public IActionResult GetAllProductsWithCategories()
        {
            var products = unitOfWork.Product.GetAllAsync(x=>x.Category).Result;
            try
            {
                if (products != null)

                    return Ok(mapper.Map<List<ProductDto>>(products));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return BadRequest("there is No products ");
        }
        [HttpGet("GetProductById/{Id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ApiExpection),400)]
        public IActionResult GetProductById( int Id)
        {
            var products = unitOfWork.Product.GetByIdAsync(Id,e=>e.Category).Result;
       

                if (products != null)
                    return Ok(mapper.Map<ProductDto>(products));

                return NotFound(new ApiExpection(400));
        }
        [HttpPost("AddProduct")]

        public IActionResult AddProduct([FromForm] CreateProductDto createProductDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   // Product product=mapper.Map<Product>(createProductDto);
                    var result = unitOfWork.Product.AddProduct(createProductDto).Result;
                    return result? Ok(createProductDto): BadRequest("you have problem");
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("you Not able to add New product");
        }

        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id,UpdateProductDto updateProductDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = unitOfWork.Product.UpdateProduct(id, updateProductDto).Result;
                    if (result) { return Ok(mapper.Map<ProductDto>(updateProductDto)); }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest($"you can't update this product with id={id}");
        }
        [HttpDelete("DeleteProductById/{Id}")]
        public IActionResult DeleteProductById(int Id)
        {
            if (Id != 0)
            {
                try
                {
                    var exitingProduct=unitOfWork.Product.GetById(Id).Result; 
                    if(exitingProduct != null)
                    {
                        var result = unitOfWork.Product.DeleteElement(exitingProduct);
                        return Ok($"product with id={Id} is deleted ");
                    }
                   
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            return BadRequest($"product with id={Id} is Not Found ");
        }
    }
}
