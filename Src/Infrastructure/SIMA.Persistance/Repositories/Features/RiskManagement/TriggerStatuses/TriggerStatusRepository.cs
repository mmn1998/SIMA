using Microsoft.EntityFrameworkCore;
using SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Entities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.RiskManagement.TriggerStatuses;

public class TriggerStatusRepository : Repository<TriggerStatus>, ITriggerStatusRepository
{
    private readonly SIMADBContext _context;

    public TriggerStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }


    public Task<Result<IEnumerable<GetTriggerStatusesQueryResult>>> GetAll(GetAllTriggerStatusesQuery request)
    {
        throw new NotImplementedException();
    }

    public async Task<TriggerStatus> GetById(TriggerStatusId id)
    {
        var entity = await _context.TriggerStatuses.FirstOrDefaultAsync(x => x.Id == id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}