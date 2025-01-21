using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;

public interface IPaymentTypeRepository : IRepository<PaymentType>
{
    Task<PaymentType> GetById(long id);
    Task<PaymentType> GetByCode(string code);
}
