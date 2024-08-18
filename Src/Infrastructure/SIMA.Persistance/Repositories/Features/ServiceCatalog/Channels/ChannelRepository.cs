using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.ServiceCatalog.Channels;

public class ChannelRepository : Repository<Channel>, IChannelRepository
{
    private readonly SIMADBContext _context;

    public ChannelRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Channel> GetById(ChannelId Id)
    {
        var entity = await _context.Channels
            .Include(x => x.ProductChannels)
            .Include(x => x.ChannelAccessPoints)
            .Include(x => x.ChannelUserTypes)
            .Include(x => x.ChannelResponsibles)
            .FirstOrDefaultAsync(x => x.Id == Id)
            ;
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}