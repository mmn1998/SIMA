using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Contracts;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class ApiMethodActionRepository : Repository<ApiMethodAction>, IApiMethodActionRepository
{
    private readonly SIMADBContext _context;

    public ApiMethodActionRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ApiMethodAction> GetById(ApiMethodActionId Id)
    {
        var entity = await _context.ApiMethodActions.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}