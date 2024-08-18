using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.ServiceCustomerTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ServiceCustomerTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.CustomerTypes;

public class CustomerTypeRepository : Repository<CustomerType>, ICustomerTypeRepository
{
    private readonly SIMADBContext _context;

    public CustomerTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CustomerType> GetById(CustomerTypeId Id)
    {
        var entity = await _context.ServiceCustomerTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}