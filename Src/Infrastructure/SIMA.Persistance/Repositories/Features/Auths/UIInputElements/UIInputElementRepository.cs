using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Contracts;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Entities;
using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.UIInputElements;

public class UIInputElementRepository : Repository<UIInputElement>, IUIInputElementRepository
{
    private readonly SIMADBContext _context;

    public UIInputElementRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UIInputElement> GetById(UIInputElementId Id)
    {
        var entity = await _context.UIInputElements.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}