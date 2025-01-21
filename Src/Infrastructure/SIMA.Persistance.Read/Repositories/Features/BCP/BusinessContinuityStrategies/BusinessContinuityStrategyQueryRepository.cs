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
     BCS.Code,
     BCS.StrategyTypeId,
     ST.Name StrategyType‌Name,
     BCS.Title,
     BCS.Description,
     BCS.ExpireDate,
     BCS.ReviewDate,
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
inner join BCP.StrategyType ST on ST.Id = BCS.StrategyTypeId and ST.ActiveStatusId<>3
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

---------- businessContinuityStratgyObjective
select
BCSO.Id,
BCSO.Title,
BCSO.CreatedAt
from BCP.BusinessContinuityStrategy BCS
inner join BCP.BusinessContinuityStrategyObjective BCSO on BCSO.BusinessContinuityStategyId = BCS.Id and BCSO.ActiveStatusId<>3
where BCS.Id = @Id
---------- businessContinuityStratgySolution
select
BCSS.Id,
BCSS.Title,
BCSS.CreatedAt
from BCP.BusinessContinuityStrategy BCS
inner join BCP.BusinessContinuityStratgySolution BCSS on BCSS.BusinessContinuityStratgyId = BCS.Id and BCSS.ActiveStatusId<>3
where BCS.Id = @Id
---------- businessContinuityStratgyIssue
select
BCSI.Id,
i.Code,
i.Description,
BCSI.CreatedAt
from BCP.BusinessContinuityStrategy BCS
inner join BCP.BusinessContinuityStrategyIssue BCSI on BCSI.BusinessContinuityStrategyId = BCS.Id and BCSI.ActiveStatusId<>3
inner join IssueManagement.Issue I on I.Id = BCSI.IssueId and I.ActiveStatusId<>3
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
BCSR.CreatedAt,
BCSR.IsForBackup,
BCSR.PlanResponsibilityId,
PR.Name PlanResponsibilityName,
s.PositionId,
Po.Name PositionName,
Po.DepartmentId,
d.Name DepartmentName,
(P.FirstName + ' ' + P.LastName) FullName,
U.CompanyId,
Com.Name CompanyName
from BCP.BusinessContinuityStrategy BCS
inner join BCP.BusinessContinuityStratgyResponsible BCSR on BCSR.BusinessContinuityStrategyId = BCS.Id and BCSR.ActiveStatusId<>3
inner join BCP.PlanResponsibility PR on PR.Id = BCSR.PlanResponsibilityId and PR.ActiveStatusId<>3
inner join Organization.Staff S on s.Id = BCSR.StaffId and s.ActiveStatusId<>3
inner join Authentication.Profile P on P.Id = S.ProfileId and P.ActiveStatusId<>3
inner join Authentication.Users U on U.ProfileID = P.Id and U.ActiveStatusId<>3
inner join Organization.Company Com on Com.Id = U.CompanyId and Com.ActiveStatusId<>3
inner join Organization.Position Po on Po.Id = s.PositionId and po.ActiveStatusId<>3
inner join Organization.Department D on d.Id = po.DepartmentId and d.ActiveStatusId<>3
where BCS.Id = @Id

";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var multi = await connection.QueryMultipleAsync(query, new { request.Id });
        var result = await multi.ReadFirstOrDefaultAsync<GetBusinessContinuityStrategyQueryResult>() ?? throw SimaResultException.NotFound;
        result.BusinessContinuityStrategyObjectiveList = await multi.ReadAsync<GetBusinessContinuityStratgyObjectiveQueryResult>();
        result.BusinessContinuityStrategySolutionList = await multi.ReadAsync<GetBusinessContinuityStratgySolutionQueryResult>();
        result.BusinessContinuityStrategyRelatedIssueList = await multi.ReadAsync<GetBusinessContinuityStratgyRelatedIssuQueryResult>();
        result.BusinessContinuityStrategyDocumentList = await multi.ReadAsync<GetBusinessContinuityStratgyDocumentQueryResult>();
        result.BusinessContinuityStrategyResponsibleList = await multi.ReadAsync<GetBusinessContinuityStratgyResponsibleQueryResult>();
        return result;

    }
}