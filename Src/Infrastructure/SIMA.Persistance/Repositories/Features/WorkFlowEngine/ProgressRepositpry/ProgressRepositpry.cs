using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Interface;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.WorkFlowEngine.ProgressRepositpry
{
    public class ProgressRepositpry : Repository<Progress>, IProgressRepository
    {
        private readonly SIMADBContext _context;
        public ProgressRepositpry(SIMADBContext context) : base(context)
        {
            _context = context;
        }
    }
}
