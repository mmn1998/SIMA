using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.PaymentTypes;

public class PaymentTypeRepository : Repository<PaymentType>, IPaymentTypeRepository
{
    private readonly SIMADBContext _context;

    public PaymentTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PaymentType> GetById(long id)
    {
        var strongTypeId = new PaymentTypeId(id);
        var entity = await _context.PaymentTypes.FirstOrDefaultAsync(pt => pt.Id == strongTypeId);
        entity.NullCheck();
        return entity;
    }
}
