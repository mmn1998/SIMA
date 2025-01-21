using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.AccountTypes;

public class AccountTypeRepository : Repository<AccountType>, IAccountTypeRepository
{
    private readonly SIMADBContext _context;

    public AccountTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AccountType> GetByCode(string code)
    {
        var entity = await _context.AccountTypes.FirstOrDefaultAsync(x => x.Code == code);
        return entity;
    }

    public async Task<AccountType> GetById(AccountTypeId Id)
    {
        var entity = await _context.AccountTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}