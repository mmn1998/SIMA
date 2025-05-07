using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class ResponsibleTypeRepository : Repository<ResponsibleType>, IResponsibleTypeRepository
{
    private readonly SIMADBContext _context;

    public ResponsibleTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ResponsibleType> GetById(long id)
    {
        var entity = await _context.ResponsibleTypes.FirstOrDefaultAsync(x => x.Id == new ResponsibleTypeId(id));
        if (entity is null) throw new SimaResultException(CodeMessges._100054Code, Messages.NotFound);
        return entity;
    }
}
