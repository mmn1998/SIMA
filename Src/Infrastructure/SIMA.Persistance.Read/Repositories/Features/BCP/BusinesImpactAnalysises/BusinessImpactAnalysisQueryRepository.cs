using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BusinesImpactAnalysises;

public class BusinessImpactAnalysisQueryRepository : IBusinessImpactAnalysisQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public BusinessImpactAnalysisQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
select
BIA.Id,
BIA.ServiceId,
s.Name ServiceName,
BIA.ImportanceDegreeId,
ImP.Name ImportanceDegreeName,
BIA.RestartReason,
BIA.ServicePriorityId,
SP.Name ServicePriorityName,
BIA.BackupPeriodId,
BP.Name BackupPeriodName,
BIA.CreatedAt,
I.Id IssueId,
I.Code IssueCode,
    W.Id  WorkFlowId,
    W.Name  WorkFlowName,
    ST.Id CurrentStateId,
    St.Name CurrentStateName,
    Step.Id CurrentStepId,
    Step.Name CurrentStepName,
    I.CreatedAt IssueCreatedAt,
    (p.FirstName + ' ' + p.LastName) IssueCreatedBy
from BCP.BusinessImpactAnalysis BIA
inner join ServiceCatalog.Service S on S.Id = BIA.ServiceId and s.ActiveStatusId<>3
inner join ServiceCatalog.ServicePriority SP on Sp.Id = BIA.ServicePriorityId and sp.ActiveStatusId<>3
inner join Basic.ActiveStatus A on BIA.ActiveStatusId<>3 and a.ID = BIA.ActiveStatusId
inner join bcp.BackupPeriod BP on BP.Id = BIA.BackupPeriodId and BP.ActiveStatusId<>3
inner join BCP.ImportanceDegree ImP on ImP.Id = BIA.ImportanceDegreeId and ImP.ActiveStatusId<>3
join BCP.BusinessImpactAnalysisIssue BCPI on BIA.Id = BCPI.BusinessImpactAnalysisId and BCPI.ActiveStatusId<> 3
join IssueManagement.Issue I on I.Id = BCPI.IssueId and I.ActiveStatusId<> 3
join Project.WorkFlow W on W.Id = I.CurrentWorkflowId and W.ActiveStatusId<> 3
left join Project.State St on St.Id = I.CurrentStateId  and St.ActiveStatusId<> 3
left join Project.Step Step on S.id = I.CurrenStepId and S.ActiveStatusId<> 3
join Authentication.Users U on U.Id = BIA.CreatedBy
join Authentication.Profile P on P.Id = u.ProfileID
where BIA.ActiveStatusId<>3 AND (@ServiceId is null OR S.Id = @ServiceId)
";
    }

    public async Task<Result<IEnumerable<GetAllBusinessImpactAnalysisesQueryResult>>> GetAll(GetAllBusinessImpactAnalysisesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
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
            dynaimcParameters.Item2.Add("ServiceId", request.ServiceId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetAllBusinessImpactAnalysisesQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetBusinessImpactAnalysisQueryResult> GetById(GetBusinessImpactAnalysisQuery request)
    {
        var query = @"
select
BIA.Id,
BIA.ServiceId,
s.Name ServiceName,
BIA.ImportanceDegreeId,
ImP.Name ImportanceDegreeName,
BIA.RestartReason,
BIA.ServicePriorityId,
SP.Name ServicePriorityName,
BIA.BackupPeriodId,
BP.Name BackupPeriodName,
BIA.CreatedAt
from BCP.BusinessImpactAnalysis BIA
inner join ServiceCatalog.Service S on S.Id = BIA.ServiceId and s.ActiveStatusId<>3
inner join ServiceCatalog.ServicePriority SP on Sp.Id = BIA.ServicePriorityId and sp.ActiveStatusId<>3
inner join Basic.ActiveStatus A on BIA.ActiveStatusId<>3 and a.ID = BIA.ActiveStatusId
inner join bcp.BackupPeriod BP on BP.Id = BIA.BackupPeriodId and BP.ActiveStatusId<>3
inner join BCP.ImportanceDegree ImP on ImP.Id = BIA.ImportanceDegreeId and ImP.ActiveStatusId<>3
where BIA.Id = @Id

------- businessImpactAnalysisAsset
select
BIAA.Id,
a.Description AssetName,
a.Model AssetCode,
BIAA.CreatedAt
from BCP.BusinessImpactAnalysis BIA
inner join BCP.BusinessImpactAnalysisAsset BIAA on BIAA.BusinessImpactAnalysisId = BIA.Id and BIAA.ActiveStatusId<>3
inner join AssetAndConfiguration.Asset A on a.Id = BIAA.AssetId and a.ActiveStatusId<>3
where BIA.Id = @Id
------- businessImpactAnalysisStaff
select
BIAS.Id,
BIAS.CreatedAt,
(p.FirstName + ' ' + p.LastName) FullName,
s.PositionId,
Po.Name PositionName,
Po.DepartmentId,
D.Name DepartmentName
from BCP.BusinessImpactAnalysis BIA
inner join BCP.BusinessImpactAnalysisStaff BIAS on BIAS.BusinessImpactAnalysisId = BIA.Id  and BIAS.ActiveStatusId<>3
inner join Organization.Staff S on s.Id = BIAS.StaffId and s.ActiveStatusId<>3
inner join Organization.Position Po on Po.Id = s.PositionId and po.ActiveStatusId<>3
inner join Organization.Department D on D.Id = Po.DepartmentId and d.ActiveStatusId<>3
inner join Authentication.Profile P on P.Id = s.ProfileId and p.ActiveStatusId<>3
where BIA.Id = @Id
------- businessImpactAnalysisDocument
select
BIAD.Id,
D.Id DocumentId,
d.Name DocumentFileName,
d.DocumentTypeId,
dt.Name DocumentTypeName,
d.FileExtensionId,
de.Name DocumentExtensionName,
s.Name AttachNtepName,
BIAD.CreatedAt,
(P.FirstName + ' ' + p.LastName) CreatedBy
from BCP.BusinessImpactAnalysis BIA
inner join BCP.BusinessImpactAnalysisDocument BIAD on BIAD.BusinessImpactAnalysisId = BIA.Id and BIAD.ActiveStatusId<>3
inner join DMS.Documents D on d.Id = BIAD.DocumentId and d.ActiveStatusId<>3
inner join DMS.DocumentType DT on DT.Id = d.DocumentTypeId and dt.ActiveStatusId<>3
inner join DMS.DocumentExtension DE on DE.Id = D.FileExtensionId and DE.ActiveStatusId<>3
left join Project.Step S on S.Id = D.AttachStepId and s.ActiveStatusID<>3
 join Authentication.Users U on U.Id = BIA.CreatedBy and U.ActiveStatusId<>3
left join Authentication.Profile P on P.Id = U.ProfileId and P.ActiveStatusId<>3
where BIA.Id = @Id
order by D.CreatedAt desc
------- businessImpactAnalysisIssue
select
BIAI.Id,
i.Code,
i.Description,
BIAI.CreatedAt
from BCP.BusinessImpactAnalysis BIA
inner join BCP.BusinessImpactAnalysisIssue BIAI on BIAI.BusinessImpactAnalysisId = BIA.Id and BIAI.ActiveStatusId<>3
inner join IssueManagement.Issue I on I.Id = BIAI.IssueId and i.ActiveStatusId<>3
where BIA.Id = @Id
------- businessImpactAnalysisDisasterOrigin
select
BIADO.Id,
BIADO.OriginId,
O.Name OriginName,
BIADO.ConsequenceId,
c.Name ConsequenceName,
BIADO.RecoveryPointObjectiveId,
RPO.Name RecoveryPointObjectiveName,
RPO.TimeMeasurementId,
tm.Name TimeMeasurementName,
BIADO.RTO,
BIADO.WRT,
BIADO.MTD,
BIADO.CreatedAt
from BCP.BusinessImpactAnalysis BIA
inner join BCP.BusinessImpactAnalysisDisasterOrigin BIADO on BIADO.BusinessImpactAnalysisId = BIA.Id and BIADO.ActiveStatusId<>3
inner join BCP.Consequence C on c.Id = BIADO.ConsequenceId and c.ActiveStatusId<>3
inner join BCP.RecoveryPointObjective RPO on RPO.Id = BIADO.RecoveryPointObjectiveId and RPO.ActiveStatusId<>3
inner join BCP.Origin O on O.Id = BIADO.OriginId and O.ActiveStatusId<>3
inner join Basic.TimeMeasurement TM on TM.Id = RPO.TimeMeasurementId and tm.ActiveStatusId<>3
where BIA.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var multi = await connection.QueryMultipleAsync(query, new { request.Id });
        var result = await multi.ReadFirstOrDefaultAsync<GetBusinessImpactAnalysisQueryResult>() ?? throw SimaResultException.NotFound;
        result.BusinessImpactAnalysisAssetList = await multi.ReadAsync<GetBusinessImpactAnalysisAssetQueryResult>();
        result.BusinessImpactAnalysisStaffList = await multi.ReadAsync<GetBusinessImpactAnalysisStaffQueryResult>();
        result.BusinessImpactAnalysisDocumemntList = await multi.ReadAsync<GetBusinessImpactAnalysisDocumentQueryResult>();
        result.BusinessImpactAnalysisIssueList = await multi.ReadAsync<GetBusinessImpactAnalysisIssueQueryResult>();
        result.BusinessImpactAnalysisDisasterOriginList = await multi.ReadAsync<GetBusinessImpactAnalysisDisasterOriginQueryResult>();
        return result;

    }
}