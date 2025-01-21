using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Contracts
{
    public interface IFinancialActionTypeRepository : IRepository<FinancialActionType>
    {
        Task<FinancialActionType> GetById(long id);
    }
}
