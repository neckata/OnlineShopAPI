using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {

        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        public List<ProductViewModel> Get()
        {
            var data = new List<ProductViewModel>
            {
                new ProductViewModel
                {
                    Id = new Guid(),
                    Name = "Food"
                },
                new ProductViewModel
                {
                    Id = new Guid(),
                    Name = "Water"
                },
                new ProductViewModel
                {
                    Id = new Guid(),
                    Name = "Clothes"
                }
            };

            return data;
        }

        /// <summary>
        /// Returns detailed information for a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        public ProductViewModel Get(Guid id)
        {
            var data = new ProductViewModel
            {
                Id = new Guid(),
                Name = "Clothes"
            };

            return data;
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Status Reponse</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Adds a products
        /// </summary>
        /// <param name="model"></param>
        /// <returns>New Product</returns>
        [HttpPost]
        public Guid Add(ProductViewModel model)
        {
            return new Guid();
        }

        /// <summary>
        /// Updates a products
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="model">Product Model</param>
        /// <returns>Status Response</returns>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, ProductViewModel model)
        {
            return Ok();
        }
    }
}
