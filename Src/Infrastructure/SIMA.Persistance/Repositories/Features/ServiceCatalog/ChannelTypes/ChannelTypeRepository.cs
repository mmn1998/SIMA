using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.ChannelTypes;

public class ChannelTypeRepository : Repository<ChannelType>, IChannelTypeRepository
{
    private readonly SIMADBContext _context;

    public ChannelTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ChannelType> GetById(ChannelTypeId Id)
    {
        var entity = await _context.ChannelTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}