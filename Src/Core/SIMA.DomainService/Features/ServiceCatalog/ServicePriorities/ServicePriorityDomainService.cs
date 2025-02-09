using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.DomainService.Features.ServiceCatalog.ServicePriorities;

public class ServicePriorityDomainService : IServicePriorityDomainService
{
    private readonly SIMADBContext _context;

    public ServicePriorityDomainService(SIMADBContext context)
    {
        _context = context;
    }
    public async Task<bool> IsCodeUnique(string code, ServicePriorityId? Id = null)
    {
        bool result = false;
        if (Id == null) result = !await _context.ServicePriorities.AnyAsync(x => x.Code == code);
        else result = !await _context.ServicePriorities.AnyAsync(x => x.Code == code && x.Id != Id);
        return result;
    }

    public async Task<bool> IsOrderingUnique(int ordering, ServicePriorityId? Id = null)
    {
        bool result = false;
        if (Id == null)
        {
            result = !await _context.ServicePriorities.AnyAsync(x => x.Ordering == ordering);
            if (await _context.ServicePriorities.AnyAsync(x => x.Ordering == ordering && x.ActiveStatusId != (long)ActiveStatusEnum.Active))
                throw new SimaResultException(CodeMessges._400Code, Messages.OrderingIsDeactivteError);
        }
        else
        {
            if (await _context.ServicePriorities.AnyAsync(x => x.Ordering == ordering && x.Id != Id && x.ActiveStatusId != (long)ActiveStatusEnum.Active))
                throw new SimaResultException(CodeMessges._400Code, Messages.OrderingIsDeactivteError);
            result = !await _context.ServicePriorities.AnyAsync(x => x.Ordering == ordering && x.Id != Id);
        }
        return result;
    }
}
