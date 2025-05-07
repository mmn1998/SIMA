using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.Customers.Contracts
{
    public interface ICustomerDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string customerNumber, long id);
    }
}
