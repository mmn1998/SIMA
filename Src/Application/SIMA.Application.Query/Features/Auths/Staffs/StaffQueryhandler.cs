using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Staffs;

namespace SIMA.Application.Query.Features.Auths.Staffs;

public class StaffQueryhandler : IQueryHandler<GetStaffQuery, Result<GetStaffQueryResult>>,
    IQueryHandler<GetAllStaffQuery, Result<IEnumerable<GetStaffQueryResult>>>,
    IQueryHandler<GetStaffByDepartmentQuery, Result<IEnumerable<GetStaffQueryResult>>>
{
    private readonly IStaffQueryRepository _repository;

    public StaffQueryhandler(IStaffQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetStaffQueryResult>> Handle(GetStaffQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }
    public async Task<Result<IEnumerable<GetStaffQueryResult>>> Handle(GetAllStaffQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetStaffQueryResult>>> Handle(GetStaffByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllByDepartmentId(request.DepartmentId);
        return Result.Ok(result);
    }
}
