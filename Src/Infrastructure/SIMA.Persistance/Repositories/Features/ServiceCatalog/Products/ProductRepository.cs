
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.Products;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly SIMADBContext _context;

    public ProductRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product> GetById(ProductId Id)
    {
        var entity = await _context.Product.Include(x=>x.ProductChannels).Include(x=>x.ProductResponsibles).FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}
