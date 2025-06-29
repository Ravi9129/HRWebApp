using AutoMapper;
using HRWebApp.Data;
using HRWebApp.DTOs;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.AddedBy)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDetailDTO>>(products);
        }

        public async Task<ProductDetailDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.AddedBy)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            return _mapper.Map<ProductDetailDTO>(product);
        }

        public async Task CreateProductAsync(CreateProductDTO productDTO, string userId)
        {
            var product = _mapper.Map<Product>(productDTO);
            product.AddedById = userId;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int id, UpdateProductDTO productDTO)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return;

            _mapper.Map(productDTO, product);
            product.LastModifiedDate = DateTime.UtcNow;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<int> GetProductCountAsync()
        {
            return await _context.Products.CountAsync(p => !p.IsDeleted);
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.AddedBy)
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDetailDTO>>(products);
        }

        public async Task SoftDeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return;

            product.IsDeleted = true;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task RestoreProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return;

            product.IsDeleted = false;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
