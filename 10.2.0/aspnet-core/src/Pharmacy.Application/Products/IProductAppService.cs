// Pharmacy.Application/Products/IProductAppService.cs
using Abp.Application.Services;
using Pharmacy.Products.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto>       CreateAsync(CreateProductDto input);
        Task<ProductDto>       UpdateAsync(int id, UpdateProductDto input);
        Task<ProductDto>       GetAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task                   DeleteAsync(int id);
    }
}