using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.InquiryRequests;

public class InquiryRequestQueryRepository : IInquiryRequestQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public InquiryRequestQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
select 
    IR.Id InquiryRequestId,
    IR.BeneficiaryName,
    IR.ReferenceNumber,
    IR.DraftOrderNumber,
    IR.ProformaNumber,
    IR.Description RequestDescription,
    IR.BranchId,
    Br.Name BranchName,
    Br.Code BranchCode,
    C.Name CustomerName,
    C.Id CustomerId,
    PT.Name PaymentTypeName,
    PT.Id PaymentTypeId,
    IR.CreatedAt RequestCreatedAt,
    P.FirstName + ' ' + P.LastName RequestCreatedBy,
    IRes.Id InquiryResponseId,
    IRes.BrokerInquiryStatusId,
    BIS.Name BrokerInquiryStatusName,
    IRes.BrokerId,
    B2.Name BrokerName,
    IRes.WageRateId,
    WR.Name WageRateName,
    IRes.CalculatedWage,
    IRes.ExcessWage,
    IRes.CreatedAt ResponseCreatedAt,
    P2.FirstName + ' ' + P2.LastName ResponseCreatedBy,
    IR.CreatedAt,
    IRes.ValidityPeriod,
    IRes.Description ResponseDescription,
	IRC.Amount,
	IRC.CurrencyTypeId,
	CT.Name CurrencyTypeName,
	IR.DraftOriginId,
	DO.Name DraftOriginName,
	IR.DraftOrderDate,
	IR.ProformaDate,
	IR.ProformaAmount,
	IR.ProformaCurrencyTypeId,
	CT2.Name ProformaCurrencyTypeName,
    TD.DraftNumber
from TrustyDraft.InquiryRequest IR
LEFT JOIN TrustyDraft.TrustyDraft TD on TD.InquiryRequestId = IR.Id and TD.ActiveStatusId<>3
join Bank.Customer C on C.Id = IR.CustomerId and C.ActiveStatusId<>3
join Bank.PaymentType PT on PT.Id = IR.PaymentTypeId and PT.ActiveStatusId<>3
join Authentication.Users U on U.Id = IR.CreatedBy  and U.ActiveStatusId <> 3
join Authentication.Profile P on P.Id = U.ProfileID
join Bank.Branch Br on Br.Id = IR.BranchId and Br.ActiveStatusId<>3
LEFT JOIN TrustyDraft.InquiryResponse IRes on IRes.InquiryRequestId = IR.Id and IRes.ActiveStatusId<>3
Left join TrustyDraft.BrokerInquiryStatus BIS on BIS.Id = IRes.BrokerInquiryStatusId and BIS.ActiveStatusId<>3
left join Bank.Broker B2 on B2.Id = IRes.BrokerId and B2.ActiveStatusId <> 3
left join TrustyDraft.WageRate WR on WR.Id = IRes.WageRateId and WR.ActiveStatusId <> 3
Left join Authentication.Users U2 on U2.Id = IRes.CreatedBy  and U2.ActiveStatusId <> 3
Left join Authentication.Profile P2 on P2.Id = U2.ProfileID
LEFT JOIN TrustyDraft.InquiryRequestCurrency IRC on IRC.Id = IRes.InquiryRequestCurrencyId and IR.ActiveStatusId<>3 and IRC.ActiveStatusId<>3
LEFT JOIN Bank.CurrencyType CT on CT.Id = IRC.CurrencyTypeId and CT.ActiveStatusId<>3
LEFT JOIN Bank.CurrencyType CT2 on CT2.Id = IR.ProformaCurrencyTypeId and CT2.ActiveStatusId<>3
LEFT JOIN TrustyDraft.DraftOrigin DO on DO.Id = IR.DraftOriginId and DO.ActiveStatusId<>3
WHERE IR.ActiveStatusId<>3 AND (isnull(dbo.FN_GetBranchIdByUserId(@UserId),0) = 0 OR dbo.FN_GetBranchIdByUserId(@UserId) = td.BranchId)
";
    }

    public async Task<Result<IEnumerable<GetInquiryRequestQueryResult>>> GetAll(GetAllInquiryRequestsQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetInquiryRequestQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetInquiryRequestQueryResult> GetById(GetInquiryRequestQuery request)
    {
        try
        {
            var query = $@"
          {_mainQuery} AND IR.[Id] = @Id

select
         BCSD.Id,
d.Name DocumentFileName,
d.DocumentTypeId,
D.Id DocumentId,
dt.Name DocumentTypeName,
d.FileExtensionId,
de.Name DocumentExtensionName,
s.Name AttachNtepName,
BCSD.CreatedAt
  from TrustyDraft.InquiryRequest TD
inner join TrustyDraft.InquiryRequestDocument BCSD on BCSD.InquiryRequestId = TD.Id and BCSD.ActiveStatusId<>3
inner join DMS.Documents D on BCSD.DocumentId = D.Id and D.ActiveStatusId<>3
inner join DMS.DocumentType DT on DT.Id = D.DocumentTypeId and DT.ActiveStatusId<>3
inner join DMS.DocumentExtension DE on DE.Id = D.FileExtensionId and DE.ActiveStatusId<>3
left join Project.Step S on S.Id = D.AttachStepId and s.ActiveStatusID<>3
where TD.Id = @Id AND TD.ActiveStatusId<>3
order by D.CreatedAt desc

select 
CT.Id CurrencyTypeId,
CT.Name CurrencyTypeName,
IRC.Amount,
IRC.Id
from TrustyDraft.InquiryRequest IR
inner join TrustyDraft.InquiryRequestCurrency IRC on IR.Id = IRC.InquiryRequestId and IRC.ActiveStatusId<>3
inner join Bank.CurrencyType CT on CT.Id = IRC.CurrencyTypeId and CT.ActiveStatusId<>3
where IR.Id = @Id AND IR.ActiveStatusId<>3
";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id });
            var result = await multi.ReadFirstOrDefaultAsync<GetInquiryRequestQueryResult>();
            result.NullCheck();
            result.InquiryRquestDocumentList = await multi.ReadAsync<GetInquiryRequestDocumentQueryResult>();
            result.InquiryRquestCurrencyList = await multi.ReadAsync<GetInquiryRequestCurrencyQueryResult>();
            return result ?? throw SimaResultException.NotFound;
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}
