using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.LoanTypes;

public class LoanTypeDomainService : ILoanTypeDomainService
{
    private readonly SIMADBContext _context;

    public LoanTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, LoanTypeId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.LoanTypes.AnyAsync(x => x.Code == code);
        else result = !await _context.LoanTypes.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }
}