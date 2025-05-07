using SIMA.Application.Query.Contract.Features.BCP.Back_UpPeriods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.Back_UpPeriods;

namespace SIMA.Application.Query.Features.BCP.Back_UpPeriods;

public class BackupPeriodQueryHandler : IQueryHandler<GetBackupPeriodQuery, Result<GetBackupPeriodQueryResult>>,
    IQueryHandler<GetAllBackupPeriodsQuery, Result<IEnumerable<GetBackupPeriodQueryResult>>>
{
    private readonly IBackupPeriodQueryRepository _repository;

    public BackupPeriodQueryHandler(IBackupPeriodQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBackupPeriodQueryResult>> Handle(GetBackupPeriodQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetBackupPeriodQueryResult>>> Handle(GetAllBackupPeriodsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}