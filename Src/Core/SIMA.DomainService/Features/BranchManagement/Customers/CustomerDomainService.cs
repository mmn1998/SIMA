using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.BranchManagement.Customers
{
    public class CustomerDomainService : ICustomerDomainService
    {
        private readonly SIMADBContext _context;

        public CustomerDomainService(SIMADBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCodeUnique(string code, long Id)
        {
            if (Id > 0)
                return await _context.Customers.AnyAsync(x => x.CustomerNumber == code && x.Id != new CustomerId(Id));
            else
                return await _context.Customers.AnyAsync(x => x.CustomerNumber == code);
        }
    }
}
