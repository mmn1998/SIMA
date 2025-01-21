using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Risks;

public class RiskQueryRepository : IRiskQueryRepository
{
    private readonly string _connectionString;
    public RiskQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<Result<IEnumerable<GetAllRisksQueryResult>>> GetAll(GetAllRisksQuery request)
    {
        var mainQuery = @"

select 
R.Id,
r.Code,
r.Name,
R.RiskTypeId,
Rt.Name RiskTypeName,
a.Name ActiveStatus,
r.Description,
r.CreatedAt
,I.Id IssueId
,I.Code IssueCode
,W.Id WorkflowId
,W.Code WorkflowCode
,i.MainAggregateId
,I.IssuePriorityId
,IPP.Name IssuePriorityName
,I.IssueTypeId
,IT.Name IssueTypeName
,i.CurrenStepId
,Step.Name CurrenStepName
,i.CurrentStateId
,St.Name CurrenStateName
,Step.HasDocument
,i.DueDate
,i.Weight IssueWeight
,(p.FirstName + ' ' + p.LastName) CreatedBy
from RiskManagement.Risk R
inner join RiskManagement.RiskType RT on RT.Id = R.RiskTypeId and rt.ActiveStatusId<>3
  Inner join RiskManagement.RiskRelatedIssue RRI on RRI.RiskId = R.Id and RRI.ActiveStatusId<>3
  inner join IssueManagement.Issue I on I.Id = RRI.IssueId and I.MainAggregateId = 4 And i.ActiveStatusId<>3
  left join Basic.ActiveStatus A on A.ID = R.ActiveStatusId
  left join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  left JOIN Authentication.Users U on R.CreatedBy = U.Id and U.ActiveStatusId<>3
  left JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
  left join Project.Step Step on I.CurrenStepId = Step.Id
  left join Project.State ST on I.CurrentStateId = ST.Id
  left join Project.Project project on project.Id = W.ProjectID
  left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
  left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId
Where R.ActiveStatusId<>3
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);

        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetAllRisksQueryResult>();
        return Result.Ok(response, request, count);

    }

    public async Task<GetRiskQueryResult> GetById(long id)
    {
        var response = new GetRiskQueryResult();
        var query = @"


select 
R.Id,
r.Code,
r.Name,
R.RiskTypeId,
Rt.Name RiskTypeName,
a.Name ActiveStatus,
r.Description,
r.CreatedAt
from RiskManagement.Risk R
inner join RiskManagement.RiskType RT on RT.Id = R.RiskTypeId and rt.ActiveStatusId<>3
inner join Basic.ActiveStatus A on A.ID = R.ActiveStatusId and r.ActiveStatusId<>3
Where R.ActiveStatusId<>3 AND R.Id = @Id

------------ CorrectiveActionList
select 
ca.Id,
ca.ActionDescription
from RiskManagement.Risk R
inner join RiskManagement.CorrectiveAction CA on CA.RiskId = r.Id and ca.ActiveStatusId<>3
where R.Id = @Id and R.ActiveStatusId<>3
------------ PreventiveActionList
select 
pa.Id,
pa.ActionDescription
from RiskManagement.Risk R
inner join RiskManagement.PreventiveAction PA on PA.RiskId = r.Id and pa.ActiveStatusId<>3
where R.Id = @Id and R.ActiveStatusId<>3

----------- EffectedAssetList

select 
EA.Id,
EA.ALE,
ea.AssetId,
a.Description AssetName,
a.Model AssetCode,
ea.ALE,
ea.AV,
EA.SLE,
EA.ALE
from
RiskManagement.Risk R
inner join RiskManagement.EffectedAsset EA on EA.RiskId = r.Id and EA.ActiveStatusId<>3
inner join AssetAndConfiguration.Asset A on A.Id = EA.AssetId and a.ActiveStatusId<>3
where R.Id = @Id and R.ActiveStatusId<>3


----------- VulnerabilityList
select
v.Id,
v.EffectedAssetId,
v.Description
from RiskManagement.Risk R
inner join RiskManagement.EffectedAsset EA on EA.RiskId = r.Id and EA.ActiveStatusId<>3
inner join RiskManagement.Vulnerability V on V.EffectedAssetId = EA.Id and V.ActiveStatusId<>3
where R.Id = @Id and R.ActiveStatusId<>3

----------- ServiceRiskImpactList

select
SR.Id,
s.Name ServiceName,
S.Code ServiceCode,
SR.ServiceId

from RiskManagement.Risk R
inner join ServiceCatalog.ServiceRisk SR on SR.RiskId = R.Id and R.ActiveStatusId<>3
inner join ServiceCatalog.Service S on SR.ServiceId = S.Id and S.ActiveStatusId<>3
where R.Id = @Id and R.ActiveStatusId<>3

----------- RiskImpactList
select
SRI.ServiceRiskId,
SRI.ImpactScaleId,
I.Name ImpactScaleName,
SRI.RiskImpactId,
RI.Name RiskImpactName
from RiskManagement.Risk R
inner join ServiceCatalog.ServiceRisk SR on SR.RiskId = R.Id and R.ActiveStatusId<>3
inner join RiskManagement.ServiceRiskImpact SRI on SRI.ServiceRiskId = SR.Id and SRI.ActiveStatusId<>3
inner join RiskManagement.ImpactScale I on I.Id = SRI.ImpactScaleId and I.ActiveStatusId<>3
inner join RiskManagement.RiskImpact RI on RI.Id = SRI.RiskImpactId and RI.ActiveStatusId<>3
where R.Id = @Id and R.ActiveStatusId<>3
  ----------- ThreatList

  select
  t.ThreatTypeId,
  tt.Name ThreatTypeName,
  t.RiskPossibilityId,
  RP.Name RiskPossibilityName,
  t.Description
  from RiskManagement.Risk R
  inner join RiskManagement.Threat T on T.RiskId = R.Id and T.ActiveStatusId<>3
  inner join RiskManagement.RiskPossibility RP on Rp.Id = T.RiskPossibilityId and RP.ActiveStatusId<>3
  inner join RiskManagement.ThreatType TT on TT.Id = t.ThreatTypeId and TT.ActiveStatusId<>3
  where R.Id = @Id and R.ActiveStatusId<>3
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var multi = await connection.QueryMultipleAsync(query, new
        {
            Id = id
        });
        response = await multi.ReadFirstOrDefaultAsync<GetRiskQueryResult>() ?? throw SimaResultException.NotFound;
        response.CorrectiveActionList = await multi.ReadAsync<GetCorrectiveActionQueryResult>();
        response.PreventiveActionList = await multi.ReadAsync<GetPreventiveActionQueryResult>();
        response.EffectedAssetList = await multi.ReadAsync<GetEffectedAssetQueryResult>();
        var vulnerabilities = await multi.ReadAsync<GetVulnerabilityQueryResult>();
        foreach (var item in response.EffectedAssetList)
        {
            item.VulnerabilityList = vulnerabilities.Where(x => x.EffectedAssetId == item.Id);
        }
        response.ServiceRiskImpactList = await multi.ReadAsync<GetServiceRiskImpactQueryResult>();
        var riskImpacts = await multi.ReadAsync<GetRiskImpactListQueryResult>();
        foreach (var item in response.ServiceRiskImpactList)
        {
            item.RiskImpactList = riskImpacts.Where(x => x.ServiceRiskId == item.Id);
        }
        response.ThreatList = await multi.ReadAsync<GetThreatQueryResult>();
        return response;
    }
}

