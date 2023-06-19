using System.Net.Http.Headers;
// using System;
// using Microsoft.EntityFrameworkCore;
using ProductAPI.DbContexts;
using ProductAPI.Dtos;
using ProductAPI.Enities;
using ProductAPI.Exceptions;
using ProductAPI.Services.Interfaces;

namespace ProductAPI.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public PagedResult<Product> GetAll(string keyword, float? minPrice, float? maxPrice, int page, int pageSize)
        {
            var query = _dbContext.Products.AsQueryable();

            // Áp dụng bộ lọc theo keyword và khoảng giá
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.ProductName.Contains(keyword));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }
            // query = query.Select(s=> new ProductDto{
            //     Id =  s.Id,
            //     ProductName = s.ProductName,
            //     Status = s.Status,
            //     Price = s.Price
            // });
            // Tổng số sản phẩm
            var totalItems = query.Count();

            // Sắp xếp theo giá từ cao đến thấp và Id giảm dần
            query = query.OrderByDescending(p => p.Price).ThenByDescending(p => p.Id);

            // Chia trang
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            // Lấy danh sách sản phẩm
            var products = query.ToList();

            // Tạo đối tượng PagedResult để trả về kết quả
            var result = new PagedResult<Product>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = products
            };
            return result;
        }

        public Product GetProductById(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                throw new UserFriendlyException($"Không tìm thấy sản phẩm có id = {id}");
            }
            return product;
        }
        public void AddProduct(CreateProductDto input)
        {
            _dbContext.Products.Add(new Product{
                ProductName = input.ProductName,
                Status = input.Status,
                Price = input.Price
            });
            _dbContext.SaveChanges();
        }

        public Product UpdateProduct(UpdateProductDto input)
        {
            var product = _dbContext.Products.Find(input.Id);
            if (product == null)
            {
                throw new UserFriendlyException($"Không tìm thấy sản phẩm có id = {input.Id}");

            }

            product.ProductName = input.ProductName;
            product.Status = input.Status;
            product.Price = input.Price;

            _dbContext.SaveChanges();

            return product;
        }

        public bool DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return false;
            }
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return true;
        }

    }
}