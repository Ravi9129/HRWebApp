using HRWebApp.DTOs;

namespace HRWebApp.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDetailDTO>> GetAllProductsAsync();
        Task<ProductDetailDTO> GetProductByIdAsync(int id);
        Task CreateProductAsync(CreateProductDTO productDTO, string userId);
        Task UpdateProductAsync(int id, UpdateProductDTO productDTO);
        Task DeleteProductAsync(int id);
        Task<bool> ProductExistsAsync(int id);
        Task<int> GetProductCountAsync();
        Task<IEnumerable<ProductDetailDTO>> GetProductsByCategoryAsync(int categoryId);
        Task SoftDeleteProductAsync(int id);
        Task RestoreProductAsync(int id);
    }
}
