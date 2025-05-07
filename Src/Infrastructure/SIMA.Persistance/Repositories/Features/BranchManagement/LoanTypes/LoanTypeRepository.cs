using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.LoanTypes;

public class LoanTypeRepository : Repository<LoanType>, ILoanTypeRepository
{
    private readonly SIMADBContext _context;

    public LoanTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<LoanType> GetById(LoanTypeId Id)
    {
        var entity = await _context.LoanTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<LoanType> GetByCode(string Code)
    {
        var entity = await _context.LoanTypes.FirstOrDefaultAsync(x => x.Code == Code);
        return entity;
    }
}