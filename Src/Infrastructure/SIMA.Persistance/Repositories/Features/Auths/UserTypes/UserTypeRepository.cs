using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.Auths.UserTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.UserTypes;

public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
{
    private readonly SIMADBContext _context;

    public UserTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserType> GetById(UserTypeId Id)
    {
        var entity = await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}