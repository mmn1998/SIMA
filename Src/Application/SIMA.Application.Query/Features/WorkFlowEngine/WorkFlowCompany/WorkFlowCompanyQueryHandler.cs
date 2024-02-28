using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowCompany;

namespace SIMA.Application.Query.Features.WorkFlowEngine.WorkFlowCompany;

public class WorkFlowCompanyQueryHandler : IQueryHandler<GetAllWorkFlowCompanyQuery, Result<List<GetWorkFlowCompanyQueryResult>>>, IQueryHandler<GetWorkFlowCompanyQuery, Result<GetWorkFlowCompanyQueryResult>>
{
    private readonly IWorkFlowCompanyQueryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkFlowCompanyQueryHandler(IWorkFlowCompanyQueryRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetWorkFlowCompanyQueryResult>> Handle(GetWorkFlowCompanyQuery request, CancellationToken cancellationToken)
    {
        var actor = await _repository.FindById(request.Id);
        return Result.Ok(actor);
    }
    public async Task<Result<List<GetWorkFlowCompanyQueryResult>>> Handle(GetAllWorkFlowCompanyQuery request, CancellationToken cancellationToken)
    {
        var entites = await _repository.GetAll();
        return Result.Ok(entites);
    }
}
