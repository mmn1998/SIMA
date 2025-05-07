using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.TrustyDrafts;

public interface ITrustyDraftQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> GetAll(GetAllTrustyDraftsQuery request);
    Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> GetAllMy(GetAllMyTrustyDraftsQuery request);
    Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> GetAllRequested(GetAllTrustyDraftRequested request);
    Task<Result<IEnumerable<GetAllDraftForPaymentResult>>> GetAllDraftForPayment(GetAllDraftForPayment request);
    Task<Result<IEnumerable<GetAllReconcilliationResult>>> GetAllReconcilliation(GetAllReconcilliation request);
    Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> GetAllFrorEachDepartment(GetAllFrorEachDepartment request);
    Task<GetTrustyDraftQueryResult> GetById(long id);
    Task<GetTrustyDraftInqueryQueryResult> GetByIdForInquery(long id);
    Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> GetAllByBrokerId(GetAllTrustyDraftByBrokerQuery request);
    Task<string?> GetLastBranchLetterNumber();
    Task<long> GetBranchIdByUserId(long userId);
    Task<long> GetBrokerTypeIdFromInquiryRequestId(long inquiryRequestId);
}