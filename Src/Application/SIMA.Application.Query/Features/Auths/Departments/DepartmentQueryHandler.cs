using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Departments;

namespace SIMA.Application.Query.Features.Auths.Departments;

public class DepartmentQueryHandler : IQueryHandler<GetDepartmentQuery, Result<GetDepartmentQueryResult>>, IQueryHandler<GetAllDepartmentsQuery, Result<List<GetDepartmentQueryResult>>>
{
    private readonly IDepartmentQueryRepository _repository;
    private readonly IMapper _mapper;

    public DepartmentQueryHandler(IDepartmentQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Result<List<GetDepartmentQueryResult>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
    }

    public async Task<Result<GetDepartmentQueryResult>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindById(request.Id);
        var result = _mapper.Map<GetDepartmentQueryResult>(entity);
        return Result.Ok(result);
    }
}
