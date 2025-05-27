using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.LicenseStatuses;

namespace SIMA.Application.Query.Features.AssetsAndConfigurations.LicenseStatuses;

public class LicenseStatusQueryHandler : IQueryHandler<GetLicenseStatusQuery, Result<GetLicenseStatusQueryResult>>,
    IQueryHandler<GetAllLicenseStatusesQuery, Result<IEnumerable<GetLicenseStatusQueryResult>>>
{
    private readonly ILicenseStatusQueryRepository _repository;

    public LicenseStatusQueryHandler(ILicenseStatusQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetLicenseStatusQueryResult>> Handle(GetLicenseStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetLicenseStatusQueryResult>>> Handle(GetAllLicenseStatusesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}