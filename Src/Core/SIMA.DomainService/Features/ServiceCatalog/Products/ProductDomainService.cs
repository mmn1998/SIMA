using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.ServiceCatalog.Products;

public class ProductDomainService : IProductDomainService
{
    private readonly SIMADBContext _context;

    public ProductDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<string?> GetLastCode()
    {
        var entity = await _context.Product.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        return entity?.Code;
    }

    public async Task<bool> IsCodeUnique(string code, ProductId? id = null)
    {
        bool result = false;
        if (id == null) result = !await _context.Product.AnyAsync(x => x.Code == code);
        else result = !await _context.Product.AnyAsync(x => x.Code == code && x.Id != id);
        return result;
    }
}
