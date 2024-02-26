using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Interface
{
    public interface IWorkFlowCompanyRepository : IRepository<Entities.WorkFlowCompany>
    {
        Task<Entities.WorkFlowCompany> GetById(long id);
    }
}
