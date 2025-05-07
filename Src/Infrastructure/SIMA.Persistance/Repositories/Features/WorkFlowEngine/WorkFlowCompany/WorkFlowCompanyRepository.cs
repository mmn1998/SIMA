using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.ValueObject;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.WorkFlowCompany
{
    public class WorkFlowCompanyRepository : Repository<Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities.WorkFlowCompany>, IWorkFlowCompanyRepository
    {
        private readonly SIMADBContext _context;
        public WorkFlowCompanyRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities.WorkFlowCompany> GetById(long id)
        {
            return await _context.WorkFlowCompanies.FirstOrDefaultAsync(x => x.Id == new WorkFlowCompanyId(id));
        }
    }
}
