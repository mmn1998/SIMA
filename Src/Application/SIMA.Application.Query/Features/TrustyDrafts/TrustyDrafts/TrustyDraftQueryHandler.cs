using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.TrustyDrafts;

namespace SIMA.Application.Query.Features.TrustyDrafts.TrustyDrafts;

public class TrustyDraftQueryHandler : IQueryHandler<GetAllTrustyDraftsQuery, Result<IEnumerable<GetAllTrustyDraftsQueryResult>>>,
    IQueryHandler<GetAllMyTrustyDraftsQuery, Result<IEnumerable<GetAllTrustyDraftsQueryResult>>>,
    IQueryHandler<GetTrustyDraftQuery, Result<GetTrustyDraftQueryResult>>, IQueryHandler<GetAllTrustyDraftRequested, Result<IEnumerable<GetAllTrustyDraftRequestedResult>>>,
    IQueryHandler<GetAllDraftForPayment, Result<IEnumerable<GetAllDraftForPaymentResult>>>,
    IQueryHandler<GetAllReconcilliation, Result<IEnumerable<GetAllReconcilliationResult>>>,
    IQueryHandler<GetAllFrorEachDepartment, Result<IEnumerable<GetAllTrustyDraftRequestedResult>>>,
    IQueryHandler<GetAllTrustyDraftByBrokerQuery, Result<IEnumerable<GetAllTrustyDraftRequestedResult>>>
{
    private readonly ITrustyDraftQueryRepository _repository;

    public TrustyDraftQueryHandler(ITrustyDraftQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> Handle(GetAllTrustyDraftsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetTrustyDraftQueryResult>> Handle(GetTrustyDraftQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> Handle(GetAllTrustyDraftRequested request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllRequested(request);
    }

    public async Task<Result<IEnumerable<GetAllDraftForPaymentResult>>> Handle(GetAllDraftForPayment request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllDraftForPayment(request);
    }

    public async Task<Result<IEnumerable<GetAllReconcilliationResult>>> Handle(GetAllReconcilliation request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllReconcilliation(request);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> Handle(GetAllTrustyDraftByBrokerQuery request, CancellationToken cancellationToken)
    {
        if (request.BrokerId <= 0)
            request.BrokerId = null;
        return await _repository.GetAllByBrokerId(request);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> Handle(GetAllFrorEachDepartment request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFrorEachDepartment(request);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> Handle(GetAllMyTrustyDraftsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllMy(request);
    }
}
