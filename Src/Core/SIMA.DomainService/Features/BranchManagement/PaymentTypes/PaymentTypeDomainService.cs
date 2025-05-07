using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.PaymentTypes;

public class PaymentTypeDomainService : IPaymentTypeDomainService
{
    private readonly SIMADBContext _context;

    public PaymentTypeDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, long Id)
    {
        if (Id > 0)
            return await _context.PaymentTypes.AnyAsync(x => x.Code == code && x.Id != new PaymentTypeId(Id));
        else
            return await _context.PaymentTypes.AnyAsync(x => x.Code == code);


    }
}
