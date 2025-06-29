using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;

namespace HRWebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public ProductService(IProductRepository productRepository, IMapper mapper, IAuditService auditService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<ProductDetailDTO> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task CreateProductAsync(CreateProductDTO productDTO, string userId)
        {
            await _productRepository.CreateProductAsync(productDTO, userId);
            await _auditService.LogActionAsync(userId, "Create", "Product",
                $"New product created: {productDTO.Name}");
        }

        public async Task UpdateProductAsync(int id, UpdateProductDTO productDTO)
        {
            await _productRepository.UpdateProductAsync(id, productDTO);
            await _auditService.LogActionAsync("System", "Update", "Product", id.ToString());
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            await _auditService.LogActionAsync("System", "Delete", "Product", id.ToString());
        }

        public async Task<int> GetProductCountAsync()
        {
            return await _productRepository.GetProductCountAsync();
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task SoftDeleteProductAsync(int id)
        {
            await _productRepository.SoftDeleteProductAsync(id);
            await _auditService.LogActionAsync("System", "SoftDelete", "Product", id.ToString());
        }

        public async Task RestoreProductAsync(int id)
        {
            await _productRepository.RestoreProductAsync(id);
            await _auditService.LogActionAsync("System", "Restore", "Product", id.ToString());
        }
    }
}
