using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.Models;
using OnlineShopServices.Product;
using OnlineShopServices.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        public async Task<List<ProductViewModel>> Get()
        {
            var response = await _productService.GetAllAsync();

            var products = response.Data.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            }).ToList(); ;

            return products;
        }

        /// <summary>
        /// Returns detailed information for a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(Guid id)
        {
            var response = await _productService.GetByIdAsync(id);

            var product = new ProductViewModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                Price = response.Data.Price
            };

            return product;
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Status Reponse</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _productService.DeleteAsync(id);

            if (response.Type != ResponseType.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Adds a product
        /// </summary>
        /// <param name="model"></param>
        /// <returns>New Product</returns>
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            var response = await _productService.AddAsync(new OnlineShopDal.Entities.Product
            {
                Name = model.Name,
                Price = model.Price
            });

            if (response.Type != ResponseType.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="model">Product Model</param>
        /// <returns>Status Response</returns>
        [HttpPut]
        public async Task<IActionResult> Update(ProductViewModel model)
        {
            var response = await _productService.UpdateAsync(new OnlineShopDal.Entities.Product
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price
            });

            if (response.Type != ResponseType.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
