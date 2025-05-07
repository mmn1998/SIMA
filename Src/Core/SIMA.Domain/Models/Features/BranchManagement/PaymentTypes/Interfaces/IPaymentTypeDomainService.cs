using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;

public interface IPaymentTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long Id);

}
