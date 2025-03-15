using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Back_UpMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Back_UpMethods;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.Back_UpMethods;

public class BackupMethodQueryHandler : IQueryHandler<GetBackupMethodQuery, Result<GetBackupMethodQueryResult>>,
    IQueryHandler<GetAllBackupMethodsQuery, Result<IEnumerable<GetBackupMethodQueryResult>>>
{
    private readonly IBackupMethodQueryRepository _repository;

    public BackupMethodQueryHandler(IBackupMethodQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBackupMethodQueryResult>> Handle(GetBackupMethodQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }
    public async Task<Result<IEnumerable<GetBackupMethodQueryResult>>> Handle(GetAllBackupMethodsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}