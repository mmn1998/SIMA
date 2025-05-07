using SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;

public interface IPaymentTypeReadRepository : IQueryRepository
{
    Task<GetPaymentTypeQueryResult> GetById(long id);
    Task<Result<IEnumerable<GetPaymentTypeQueryResult>>> GetAll(GetAllPaymentTypesQuery request);
}
