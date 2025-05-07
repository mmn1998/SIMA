using SIMA.Application.Query.Contract.Features.WorkFlowEngine.ApprovalOptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.ApprovalOptions;

namespace SIMA.Application.Query.Features.WorkFlowEngine.ApprovalOptions
{
    public class ApprovalOptionQueryHandler : 
    IQueryHandler<GetAllApprovalOptionsQuery, Result<IEnumerable<GetApprovalOptionQueryResult>>>,
    IQueryHandler<GetApprovalOptionQuery, Result<GetApprovalOptionQueryResult>>
    {
        private readonly IApprovalOptionQueryRepository _repository;

    public ApprovalOptionQueryHandler(IApprovalOptionQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetApprovalOptionQueryResult>>> Handle(GetAllApprovalOptionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetApprovalOptionQueryResult>> Handle(GetApprovalOptionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }
}
}
