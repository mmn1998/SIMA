using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Application.Query.Contract.Features.BCP.Consequences;
using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanQueryRepository : IBusinessContinuityPlanQueryRepository
{
    private readonly string _connectionString;
    public BusinessContinuityPlanQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetBusinessContinuityPlanQueryResult> GetByIdAndVersionNumber(GetBusinessContinuityPlanByVersionQuery request)
    {
            try
        {
            var response = new GetBusinessContinuityPlanQueryResult();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string query = @"
                     Select 
                        BCP.Id,
                        BCP.Code,
                        BCP.Title,
                        BCP.Scope
                        from BCP.BusinessContinuityPlan BCP
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber

                        ----businessContinuityStratgyIssue
                        Select 
                        I.Id,
                        I.Code,
                        I.Description,
                        I.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanIssue BCPI on BCP.Id = BCPI.BusinessContinuityPlanId and BCPI.ActiveStatusId<> 3
                        join IssueManagement.Issue I on I.Id = BCPI.IssueId and I.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber

                        ----BusinessContinuityPlanVersioning
                        Select 
                        BCPV.Id,
                        BCPV.VersionNumber,
                        BCPV.ReleaseDate,
                        BCPV.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber

                        ----businessContinuityPlanStratgy
                        Select 
                        BCS.Id ,
                        BCS.Title,
                        BCS.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanStratgy BCPS on BCPV.Id = BCPS.BusinessContinuityPlanVersioningId and BCPS.ActiveStatusId<> 3
                        join BCP.BusinessContinuityStrategy BCS on BCPS.BusinessContinuityStratgyId = BCS.Id and BCS.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber


                        ----businessContinuityPlanService
                        Select 
                        S.Id,
                        S.Name,
                        S.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanService BCPS on BCPS.BusinessContinuityPlanVersioningId = BCPV.Id and BCPS.ActiveStatusId<> 3
                        join ServiceCatalog.Service S on S.Id = BCPS.ServiceId and s.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber

                        ----businessContinuityPlanRisk
                        Select 
                        R.Id,
                        R.Description,
                        R.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanRisk BCPR on BCPR.BusinessContinuityPlanVersioningId = BCPV.Id and BCPR.ActiveStatusId<> 3
                        join RiskManagement.Risk R on R.Id = BCPR.RiskId and R.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber

                        ----businessContinuityPlanCriticalActivity
                        Select 
                        CA.Id,
                        CA.Name,
                        CA.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanCriticalActivity BCPCA on BCPCA.BusinessContinuityPlanVersioningId = BCPV.Id and BCPCA.ActiveStatusId<> 3
                        join ServiceCatalog.CriticalActivity CA  on CA.Id = BCPCA.CriticalActivityId and CA.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber

                        ----businessContinuityPlanAssumption
                        Select 
                        BCPA.Id,
                        BCPA.Title,
                        BCPA.Code,
                        BCPA.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3 
                        join BCP.BusinessContinuityPlanAssumption BCPA on BCPA.BusinessContinuityPlanVersioningId = BCPV.Id and BCPA.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber


                        ----businessContinuityPlanRelatedStaff
                         Select 
                             S.Id,
                             Pro.FirstName,
                             Pro.LastName,
                             P.Id PositionId,
                             p.Name PositionName,
                             D.Id DepartmentId,
                             D.Name DepartmentName,
                             C.Name CompanyName,
                             C.Id CompanyId,
                             S.CreatedAt
                             from BCP.BusinessContinuityPlan BCP
                              join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                              join BCP.BusinessContinuityPlanRelatedStaff BCPRS on BCPRS.BusinessContinuityPlanVersioningId = BCPV.Id and BCPRS.ActiveStatusId<> 3
                              join Organization.Staff S on BCPRS.StaffId =  S.Id and S.ActiveStatusId<> 3
                              join Organization.Position P on S.PositionId = p.Id and P.ActiveStatusId<> 3
                              join Organization.Department D on P.DepartmentId = D.Id and D.ActiveStatusId<> 3
                              join Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId <> 3
                             join Authentication.Profile Pro on Pro.Id = S.ProfileId and Pro.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber


                        ----businessContinuityPlanResponsible
                        Select 
                        BCPR.Id,
                        Pro.FirstName,
                        Pro.LastName,
                        PR.Id PlanResponsibilityId,
                        PR.Name planResponsibilityName,
                        BCPR.IsForBackup,
                        P.Id PositionId,
                        p.Name PositionName,
                        D.Id DepartmentId,
                        D.Name DepartmentName,
                        S.CreatedAt,
                        C.Id CompanyId,
                        C.Name CompanyName
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanResponsible BCPR on BCPR.BusinessContinuityPlanVersioningId = BCPV.Id  and BCPR.ActiveStatusId<> 3
                        join Organization.Staff S on BCPR.StaffId =  S.Id  and S.ActiveStatusId<> 3
                        join Authentication.Profile Pro on Pro.Id = S.ProfileId   and Pro.ActiveStatusId<> 3
                        join Organization.Position P on S.PositionId = p.Id  and P.ActiveStatusId<> 3
                        join Organization.Department D on P.DepartmentId = D.Id  and D.ActiveStatusId<> 3
                        join Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId<>3
                        join BCP.PlanResponsibility PR on PR.Id = BCPR.PlanResponsibilityId  and PR.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id and BCP.VersionNumber = @VersionNumber


        ";

            using var multi = await connection.QueryMultipleAsync(query, new { Id = request.BusinessContinuityPlanId , request.VersionNumber });
            response = await multi.ReadFirstOrDefaultAsync<GetBusinessContinuityPlanQueryResult>() ?? throw SimaResultException.NotFound;
            response.BusinessContinuityStratgyIssueList = await multi.ReadAsync<GetBusinessContinuityStratgyIssue>();
            response.BusinesscontinuityplanVersionList = await multi.ReadAsync<GetBusinesscontinuityplanversioning>();
            response.BusinesscontinuityplanStratgyList = await multi.ReadAsync<GetBusinesscontinuityplanstratgy>();
            response.BusinesscontinuityplanServiceList = await multi.ReadAsync<GetBusinesscontinuityplanservice>();
            response.BusinesscontinuityplanRiskList = await multi.ReadAsync<GetBusinesscontinuityplanrisk>();
            response.BusinesscontinuityplanCriticalActivityList = await multi.ReadAsync<GetBusinesscontinuityplancriticalactivity>();
            response.BusinesscontinuityplanAssumptionList = await multi.ReadAsync<GetBusinesscontinuityplanassumption>();
            response.BusinesscontinuityplanRelatedstaffList = await multi.ReadAsync<GetBusinesscontinuityplanrelatedstaff>();
            response.BusinesscontinuityplanResponsibleList = await multi.ReadAsync<GetBusinesscontinuityplanresponsible>();

            response.NullCheck();
            return response;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Result<IEnumerable<GetAllBusinessContinuityPlansQueryResult>>> GetAll(GetAllBusinessContinuityPlansQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var mainQuery = @"
Select 
    BCP.Id,
    BCP.Code,
    BCP.Title,
    BCP.Scope,
    BCPV.VersionNumber,
    BCPV.ReleaseDate,
    BCP.CreatedAt,
    I.Id IssueId,
    I.Code IssueCode,
    W.Id  WorkFlowId,
    W.Name  WorkFlowName,
    ST.Id CurrentStateId,
    St.Name CurrentStateName,
    S.Id CurrentStepId,
    S.Name CurrentStepName,
    I.CreatedAt IssueCreatedAt,
    (p.FirstName + ' ' + p.LastName) IssueCreatedBy,
	Com.Name CompanyName
from BCP.BusinessContinuityPlan BCP join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
join BCP.BusinessContinuityPlanIssue BCPI on BCP.Id = BCPI.BusinessContinuityPlanId and BCPI.ActiveStatusId<> 3
join IssueManagement.Issue I on I.Id = BCPI.IssueId and I.ActiveStatusId<> 3
join Project.WorkFlow W on W.Id = I.CurrentWorkflowId and W.ActiveStatusId<> 3
left join Project.State St on St.Id = I.CurrentStateId  and St.ActiveStatusId<> 3
join Project.Step S on S.id = I.CurrenStepId and S.ActiveStatusId<> 3
join Authentication.Users U on U.Id = BCP.CreatedBy
join Authentication.Profile P on P.Id = u.ProfileID
join Organization.Company Com on Com.Id = U.CompanyId
where BCP.ActiveStatusId <> 3
							";
        var queryCount = $@"
                             WITH Query as(	{mainQuery}	)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

        string query = $@"WITH Query as(
							 {mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetAllBusinessContinuityPlansQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetBusinessContinuityPlanQueryResult> GetById(GetBusinessContinuityPlanQuery request)
    {
        try
        {
            var response = new GetBusinessContinuityPlanQueryResult();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string query = @"
                     Select 
                        BCP.Id,
                        BCP.Code,
                        BCP.Title,
                        BCP.Scope
                        from BCP.BusinessContinuityPlan BCP
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id

                        ----businessContinuityStratgyIssue
                        Select 
                        I.Id,
                        I.Code,
                        I.Description,
                        I.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanIssue BCPI on BCP.Id = BCPI.BusinessContinuityPlanId and BCPI.ActiveStatusId<> 3
                        join IssueManagement.Issue I on I.Id = BCPI.IssueId and I.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id

                        ----BusinessContinuityPlanVersioning
                        Select 
                        BCPV.Id,
                        BCPV.VersionNumber,
                        BCPV.ReleaseDate,
                        BCPV.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id

                        ----businessContinuityPlanStratgy
                        Select 
                        BCS.Id ,
                        BCS.Title,
                        BCS.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanStratgy BCPS on BCPV.Id = BCPS.BusinessContinuityPlanVersioningId and BCPS.ActiveStatusId<> 3
                        join BCP.BusinessContinuityStrategy BCS on BCPS.BusinessContinuityStratgyId = BCS.Id and BCS.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id


                        ----businessContinuityPlanService
                        Select 
                        S.Id,
                        S.Name,
                        S.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanService BCPS on BCPS.BusinessContinuityPlanVersioningId = BCPV.Id and BCPS.ActiveStatusId<> 3
                        join ServiceCatalog.Service S on S.Id = BCPS.ServiceId and s.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id

                        ----businessContinuityPlanRisk
                        Select 
                        R.Id,
                        R.Description,
                        R.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanRisk BCPR on BCPR.BusinessContinuityPlanVersioningId = BCPV.Id and BCPR.ActiveStatusId<> 3
                        join RiskManagement.Risk R on R.Id = BCPR.RiskId and R.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id

                        ----businessContinuityPlanCriticalActivity
                        Select 
                        CA.Id,
                        CA.Name,
                        CA.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanCriticalActivity BCPCA on BCPCA.BusinessContinuityPlanVersioningId = BCPV.Id and BCPCA.ActiveStatusId<> 3
                        join ServiceCatalog.CriticalActivity CA  on CA.Id = BCPCA.CriticalActivityId and CA.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id

                        ----businessContinuityPlanAssumption
                        Select 
                        BCPA.Id,
                        BCPA.Title,
                        BCPA.Code,
                        BCPA.CreatedAt
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3 
                        join BCP.BusinessContinuityPlanAssumption BCPA on BCPA.BusinessContinuityPlanVersioningId = BCPV.Id and BCPA.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id


                        ----businessContinuityPlanRelatedStaff
                         Select 
                             S.Id,
                             Pro.FirstName,
                             Pro.LastName,
                             P.Id PositionId,
                             p.Name PositionName,
                             D.Id DepartmentId,
                             D.Name DepartmentName,
                             C.Name CompanyName,
                             C.Id CompanyId,
                             S.CreatedAt
                             from BCP.BusinessContinuityPlan BCP
                              join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                              join BCP.BusinessContinuityPlanRelatedStaff BCPRS on BCPRS.BusinessContinuityPlanVersioningId = BCPV.Id and BCPRS.ActiveStatusId<> 3
                              join Organization.Staff S on BCPRS.StaffId =  S.Id and S.ActiveStatusId<> 3
                              join Organization.Position P on S.PositionId = p.Id and P.ActiveStatusId<> 3
                              join Organization.Department D on P.DepartmentId = D.Id and D.ActiveStatusId<> 3
                              join Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId <> 3
                             join Authentication.Profile Pro on Pro.Id = S.ProfileId and Pro.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id


                        ----businessContinuityPlanResponsible
                        Select 
                        BCPR.Id,
                        Pro.FirstName,
                        Pro.LastName,
                        PR.Id PlanResponsibilityId,
                        PR.Name planResponsibilityName,
                        BCPR.IsForBackup,
                        P.Id PositionId,
                        p.Name PositionName,
                        D.Id DepartmentId,
                        D.Name DepartmentName,
                        S.CreatedAt,
                        C.Id CompanyId,
                        C.Name CompanyName
                        from BCP.BusinessContinuityPlan BCP
                        join BCP.BusinessContinuityPlanVersioning BCPV on BCP.Id = BCPV.BusinessContinuityPlanId and BCPV.ActiveStatusId<> 3
                        join BCP.BusinessContinuityPlanResponsible BCPR on BCPR.BusinessContinuityPlanVersioningId = BCPV.Id  and BCPR.ActiveStatusId<> 3
                        join Organization.Staff S on BCPR.StaffId =  S.Id  and S.ActiveStatusId<> 3
                        join Authentication.Profile Pro on Pro.Id = S.ProfileId   and Pro.ActiveStatusId<> 3
                        join Organization.Position P on S.PositionId = p.Id  and P.ActiveStatusId<> 3
                        join Organization.Department D on P.DepartmentId = D.Id  and D.ActiveStatusId<> 3
                        join Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId<>3
                        join BCP.PlanResponsibility PR on PR.Id = BCPR.PlanResponsibilityId  and PR.ActiveStatusId<> 3
                        where BCP.ActiveStatusId <> 3 and BCP.Id = @Id


        ";

            using var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id });
            response = await multi.ReadFirstOrDefaultAsync<GetBusinessContinuityPlanQueryResult>() ?? throw SimaResultException.NotFound;
            response.BusinessContinuityStratgyIssueList = await multi.ReadAsync<GetBusinessContinuityStratgyIssue>();
            response.BusinesscontinuityplanVersionList = await multi.ReadAsync<GetBusinesscontinuityplanversioning>();
            response.BusinesscontinuityplanStratgyList = await multi.ReadAsync<GetBusinesscontinuityplanstratgy>();
            response.BusinesscontinuityplanServiceList = await multi.ReadAsync<GetBusinesscontinuityplanservice>();
            response.BusinesscontinuityplanRiskList = await multi.ReadAsync<GetBusinesscontinuityplanrisk>();
            response.BusinesscontinuityplanCriticalActivityList = await multi.ReadAsync<GetBusinesscontinuityplancriticalactivity>();
            response.BusinesscontinuityplanAssumptionList = await multi.ReadAsync<GetBusinesscontinuityplanassumption>();
            response.BusinesscontinuityplanRelatedstaffList = await multi.ReadAsync<GetBusinesscontinuityplanrelatedstaff>();
            response.BusinesscontinuityplanResponsibleList = await multi.ReadAsync<GetBusinesscontinuityplanresponsible>();

            response.NullCheck();
            return response;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>> GetPlanAssumptionByPlanId(long planId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"
select BCPV.Id, BCPV.Title Name
from BCP.BusinessContinuityPlanAssumption BCPV
where BCPV.BusinessContinuityPlanVersioningId = @BusinessContinuityPlanId
";
        return await connection.QueryAsync<GetAllPlanVersioningsByPlanIdQueryResult>(query, new { BusinessContinuityPlanId = planId });
    }

    public async Task<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>> GetPlanVersioningByPlanId(long planId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"
select BCPV.Id, BCPV.VersionNumber Name
from BCP.BusinessContinuityPlanVersioning BCPV
where BCPV.BusinessContinuityPlanId = @BusinessContinuityPlanId
";
        return await connection.QueryAsync<GetAllPlanVersioningsByPlanIdQueryResult>(query, new { BusinessContinuityPlanId = planId });
    }
}
