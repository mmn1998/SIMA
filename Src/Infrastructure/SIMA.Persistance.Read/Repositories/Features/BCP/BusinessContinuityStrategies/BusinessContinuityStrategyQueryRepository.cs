using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityStrategies;

public class BusinessContinuityStrategyQueryRepository : IBusinessContinuityStrategyQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public BusinessContinuityStrategyQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
                        select
     BCS.Id, 
     BCS.Title,
     BCS.ExpireDate,
     BCS.CreatedAt,
     I.Id IssueId,
     I.Code IssueCode,
     I.CurrentWorkflowId WorkflowId,
     W.Name WorkFlowName,
     STA.Id CurrentStateId,
     STA.Name CurrentStateName,
     S.Id CurrentStepId,
     S.Name CurrentStepName,
	 (p.FirstName + ' ' + p.LastName) IssueCreatedBy
from BCP.BusinessContinuityStrategy BCS
inner join BCP.BusinessContinuityStrategyIssue BI on BI.BusinessContinuityStrategyId = BCS.Id and BI.ActiveStatusId <>3
inner join IssueManagement.Issue I on I.Id = BI.IssueId  and I.ActiveStatusId <>3
inner join Project.WorkFlow W on W.Id = I.CurrentWorkflowId and W.ActiveStatusId <>3
inner join Project.Step S on S.Id = I.CurrenStepId and S.ActiveStatusId <>3
left join Project.State STA on STA.Id = I.CurrentStateId and STA.ActiveStatusId <>3
inner join Basic.ActiveStatus A on a.ID = BCS.ActiveStatusId and BCS.ActiveStatusId<>3
inner join Authentication.Users U on U.Id = BCS.CreatedBy and U.ActiveStatusId<>3
inner join Authentication.Profile P On P.Id = U.ProfileID and p.ActiveStatusId<>3
where BCS.ActiveStatusId <>3
";
    }

    public async Task<Result<IEnumerable<GetAllBusinessContinuityStrategiesQueryResult>>> GetAll(GetAllBusinessContinuityStrategiesQuery request)
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
        var response = await multi.ReadAsync<GetAllBusinessContinuityStrategiesQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetBusinessContinuityStrategyQueryResult> GetById(GetBusinessContinuityStrategyQuery request)
    {
        var query = @"
select
BCS.Id
,BCS.Title
,BCS.Description
,BCS.StrategyTypeId
,ST.Name StrategyName
,BCS.Code
,BCS.Description
,BCS.ExpireDate
,BCS.ReviewDate
,BCS.CreatedAt
from BCP.BusinessContinuityStrategy BCS
inner join BCP.StrategyType ST on ST.Id = BCS.StrategyTypeId and ST.ActiveStatusId<>3
inner join Basic.ActiveStatus A on a.ID = BCS.ActiveStatusId and BCS.ActiveStatusId<>3
where BCS.Id = @Id



---------- businessContinuityStratgyDocument
select
BCSD.Id,
D.Id DocumentId,
d.Name DocumentFileName,
d.DocumentTypeId,
dt.Name DocumentTypeName,
d.FileExtensionId,
de.Name DocumentExtensionName,
s.Name AttachNtepName,
BCSD.CreatedAt,
(P.FirstName + ' ' + p.LastName) CreatedBy
from BCP.BusinessContinuityStrategy BCS
inner join BCP.BusinessContinuityStrategyDocument BCSD on BCSD.BusinessContinuityStategyId = BCS.Id and BCSD.ActiveStatusId<>3
inner join DMS.Documents D on BCSD.DocumentId = D.Id and D.ActiveStatusId<>3
inner join DMS.DocumentType DT on DT.Id = D.DocumentTypeId and DT.ActiveStatusId<>3
inner join DMS.DocumentExtension DE on DE.Id = D.FileExtensionId and DE.ActiveStatusId<>3
left join Project.Step S on S.Id = D.AttachStepId and s.ActiveStatusID<>3
join Authentication.Users U on U.Id = BCS.CreatedBy and U.ActiveStatusId<>3
left join Authentication.Profile P on P.Id = U.ProfileId and P.ActiveStatusId<>3
where BCS.Id = @Id
order by D.CreatedAt desc
---------- businessContinuityStratgyResponsible
select
BCSR.Id,
R.Name,
r.RiskCategoryId,
RT.Name RiskCategoryName,
BCSR.CreatedAt
from BCP.BusinessContinuityStrategyRisk BCSR
inner join RiskManagement.Risk R on R.Id =  BCSR.RiskId
inner join RiskManagement.RiskCategory RT on  RT.Id = R.RiskCategoryId
where BCS.Id = @Id


";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var multi = await connection.QueryMultipleAsync(query, new { request.Id });
        var result = await multi.ReadFirstOrDefaultAsync<GetBusinessContinuityStrategyQueryResult>() ?? throw SimaResultException.NotFound;

        result.BusinessContinuityStrategyDocumentList = await multi.ReadAsync<GetBusinessContinuityStratgyDocumentQueryResult>();
        result.BusinessContinuityStratgyRiskQueryResult = await multi.ReadAsync<GetBusinessContinuityStratgyRiskQueryResult>();

//ToDo Add 
        return result;

    }
}