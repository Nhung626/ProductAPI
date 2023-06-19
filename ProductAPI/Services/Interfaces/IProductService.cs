
using ProductAPI.Dtos;
using ProductAPI.Enities;

namespace ProductAPI.Services.Interfaces{
    public interface IProductService{
        PagedResult<Product> GetAll(string keyword, float? minPrice, float? maxPrice, int page, int pageSize);
        void AddProduct(CreateProductDto input);
        Product GetProductById(int id);
        Product UpdateProduct(UpdateProductDto input);
        bool DeleteProduct(int id);
    }
}