using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProductAPI.Dtos;
using ProductAPI.Services.Interfaces;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public IActionResult Create(CreateProductDto input)
        {
            _productService.AddProduct(input);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateProductDto input)
        {
            _productService.UpdateProduct(input);
            return Ok();
        }

        [HttpGet("get-all")]
        public IActionResult GetAll(string keyword, float? minPrice, float? maxPrice, int page = 1, int pageSize = 10)
        {
            return Ok(_productService.GetAll(keyword, minPrice, maxPrice, page, pageSize));
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById([Range(1, int.MaxValue, ErrorMessage = "Id phải lớn hơn 0")] int id)
        {
            return Ok(_productService.GetProductById(id));
        }
    }
}