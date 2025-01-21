using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.CriticalActivities;

public class CriticalActivitiyQueryRepository : ICriticalActivitiyQueryRepository
{
    private readonly string _connectionString;
    private readonly ISimaIdentity _simaIdentity;

    public CriticalActivitiyQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
    {
        _connectionString = configuration.GetConnectionString();
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<IEnumerable<GetAllCriticalActivitiesQueryResult>>> GetAll(GetAllCriticalActivitiesQuery request)
    {
        var response = Enumerable.Empty<GetAllCriticalActivitiesQueryResult>();
        string mainQuery = @"
SELECT CA.Id,
	CA.Name,
	CA.Code,
	CA.ActiveStatusId,
	A.Name ActiveStatus,
	ca.TechnicalSupervisorDepartmentId
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
	  ,d.Name TechnicalSupervisorDepartmentName
	  ,d.code TechnicalSupervisorDepartmentCode
      ,(p.FirstName + ' ' + p.LastName) CreatedBy
	  ,CA.CreatedAt
	  ,i.Description
  FROM ServiceCatalog.CriticalActivity CA
  inner join IssueManagement.Issue I on I.Id = CA.IssueId and I.MainAggregateId = 6 And i.ActiveStatusId<>3
  left join Basic.ActiveStatus A on A.ID = CA.ActiveStatusId
  left join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  left JOIN Authentication.Users U on CA.CreatedBy = U.Id and U.ActiveStatusId<>3
  left JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
  left join Organization.Department D on d.Id =CA.TechnicalSupervisorDepartmentId and d.ActiveStatusId<>3
  left join Project.Step Step on I.CurrenStepId = Step.Id
  left join Project.State ST on I.CurrentStateId = ST.Id
  left join Project.Project project on project.Id = W.ProjectID
  left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
  left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId
  where CA.ActiveStatusId<>3
";
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
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                response = await multi.ReadAsync<GetAllCriticalActivitiesQueryResult>();
            }
        }
        return Result.Ok(response);
    }
    public async Task<Result<GetCriticalActivityQueryResult>> GetDetail(long id, long issueId)
    {
        try
        {
            var response = new GetCriticalActivityQueryResult();
            string query = @"
                            select
                            CA.Id,
                            CA.Name,
                            CA.Code,
                            CA.ActiveStatusId,
                            case when CA.CreatedBy = @userId then '1' else '0' end IsEditable,
                            A.Name ActiveStatus,
                            st.UIPropertyBoxTitle,
                            ca.TechnicalSupervisorDepartmentId,
                            (p.FirstName + ' ' + P.LastName) CreatedBy,
                            ca.CreatedAt,
                            d.Name TechnicalSupervisorDepartmentName,
                            d.code TechnicalSupervisorDepartmentCode,
                            i.Description
                            from ServiceCatalog.CriticalActivity CA
                            INNER JOIN Authentication.Users U on CA.CreatedBy = U.Id and U.ActiveStatusId<>3
                            INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
                            INNER JOIN Basic.ActiveStatus A on A.ID = CA.ActiveStatusId
                            LEFT JOIN Organization.Department D on D.Id = CA.TechnicalSupervisorDepartmentId and D.ActiveStatusId<>3
                            inner join IssueManagement.Issue I on i.SourceId = ca.Id
                            inner join Project.Step st on st.Id = i.CurrenStepId
                            where ca.ActiveStatusId<>3 and ca.Id = @Id


                            -------- relatedService

                                select
                                CAS.Id,
                                S.Id ServiceId,
                                S.Name,
                                s.Code,
                                s.Description,
                                SC.Id ServiceCategoryId,
                                SC.Name ServiceCategoryName,
                                SC.Code ServiceCategoryCode,
                                s.TechnicalSupervisorDepartmentId,
                                d.Name TechnicalSupervisorDepartmentName,
                                d.code TechnicalSupervisorDepartmentCode
                                from ServiceCatalog.CriticalActivity CA
                                inner join ServiceCatalog.CriticalActivityService CAS on CAS.CriticalActivityId = ca.Id and CAS.ActiveStatusId<>3
                                inner join ServiceCatalog.Service S on CAS.ServiceId = S.Id and s.ActiveStatusId<>3
                                Left join ServiceCatalog.ServiceCategory SC on SC.Id = S.ServiceCategoryId and SC.ActiveStatusId<>3
                                Left join Organization.Department D on D.Id = S.TechnicalSupervisorDepartmentId and D.ActiveStatusId<>3
                                where CA.Id = @Id and ca.ActiveStatusId<>3

                            --------- asset

                                select
                                A.Id,
                                A.Title,
                                A.SerialNumber,
                                A.Model,
                                A.Manufacturer,
                                A.ManufactureDate,
                                A.OwnershipDate,
                                A.InServiceDate,
                                A.ExpireDate,
                                A.RetiredDate,
                                A.Description,
                                A.OwnershipPrepaymentValue,
                                A.OwnershipPaymentValue,
                                A.HasConfidentialInformation,
                                (p.FirstName + ' ' + p.LastName) OwnerFullName,
                                S.Id OwnerId,
                                A.SupplierId,
                                Sup.Name SupplierName,
                                Sup.Code SupplierCode,
                                A.BusinessCriticalityId,
                                bc.Name BusinessCriticalityName,
                                bc.Code BusinessCriticalityCode,
                                a.AssetTechnicalStatusId,
                                ATS.Name AssetTechnicalStatusName,
                                ATS.Code AssetTechnicalStatusCode,
                                A.WarehouseId,
                                W.Name WarehouseName,
                                w.Code WarehouseCode,
                                A.AssetPhysicalStatusId,
                                APS.Name AssetPhysicalStatusName,
                                APS.Code AssetPhysicalStatusCode,
                                A.OwnershipTypeId,
                                OT.Name OwnershipTypeName,
                                OT.Code OwnershipTypeCode,
                                a.PhysicalLocationId,
                                L.Name PhysicalLocationName,
                                l.Code PhysicalLocationCode
                                from ServiceCatalog.CriticalActivity CA
                                inner join ServiceCatalog.CriticalActivityAsset CAA on CAA.CriticalActivityId = ca.Id and CAA.ActiveStatusId<>3
                                inner join AssetAndConfiguration.Asset A on A.Id = CAA.AssetId and A.ActiveStatusId<>3
                                left join Organization.Staff S on s.Id = a.OwnerId and s.ActiveStatusId<>3
                                left join Authentication.Profile P on p.id = s.ProfileId and p.ActiveStatusId<>3
                                left join Basic.Supplier Sup on Sup.Id = a.SupplierId and Sup.ActiveStatusId<>3
                                left join AssetAndConfiguration.BusinessCriticality BC on BC.Id = A.BusinessCriticalityId and BC.ActiveStatusId<>3
                                left join AssetAndConfiguration.AssetTechnicalStatus ATS on ATS.Id = A.AssetTechnicalStatusId and ATS.ActiveStatusId<>3
                                left join AssetAndConfiguration.AssetPhysicalStatus APS on APS.Id = A.AssetPhysicalStatusId and APS.ActiveStatusId<>3
                                left join Authentication.Warehouse W on W.Id = A.WarehouseId and W.ActiveStatusId<>3
                                left join Authentication.OwnershipType OT on OT.Id = A.OwnershipTypeId and OT.ActiveStatusId<>3
                                LEFT JOIN Basic.Location L on L.Id = a.PhysicalLocationId and l.ActiveStatusId<>3
                                where CA.Id = @Id and ca.ActiveStatusId<>3


                            --------- ConfigurationItem
                                select
                                CAC.Id,
                                CI.Code,
                                CI.Description,
                                (p.FirstName + ' ' + p.LastName) OwnerFullName,
                                S.Id OwnerId,
                                CI.SupplierId,
                                Sup.Name SupplierName,
                                Sup.Code SupplierCode,
                                ci.ConfigurationItemTypeId,
                                cit.Name ConfigurationItemTypeName,
                                cit.Code ConfigurationItemTypeCode,
                                ci.ConfigurationItemStatusId,
                                CIS.Name ConfigurationItemStatusName,
                                CIS.Code ConfigurationItemStatusCode,
                                CI.LicenseTypeId,
                                LT.Name LicenseTypeName,
                                lt.Code LicenseTypeCode,
                                ci.LicenseSupplierId,
                                LSup.Name LicenseSupplierName,
                                LSup.Code LicenseSupplierCode,
                                ci.CompanyBuildingLocationId,
                                L.Name CompanyBuildingLocationName,
                                L.Code CompanyBuildingLocationCode
                                from ServiceCatalog.CriticalActivity CA
                                inner join ServiceCatalog.CriticalActivityConfigurationItem CAC on CAC.CriticalActivityId = ca.Id and CAC.ActiveStatusId<>3
                                inner join AssetAndConfiguration.ConfigurationItem CI on CI.Id = CAC.ConfigurationItemId and CI.ActiveStatusId<>3
                                left join Organization.Staff S on s.Id = ci.OwnerId and s.ActiveStatusId<>3
                                left join Authentication.Profile P on p.id = s.ProfileId and p.ActiveStatusId<>3
                                left join Basic.Supplier Sup on Sup.Id = ci.SupplierId and Sup.ActiveStatusId<>3
                                left join AssetAndConfiguration.ConfigurationItemType CIT on cit.Id = ci.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
                                left join AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = ci.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
                                left join AssetAndConfiguration.LicenseType LT on LT.Id = ci.LicenseTypeId and LT.ActiveStatusId<>3
                                left join Basic.Supplier LSup on LSup.Id = ci.LicenseSupplierId and lsup.ActiveStatusId<>3
                                left join Basic.Location L on l.Id = ci.CompanyBuildingLocationId and l.ActiveStatusId<>3
                                where CA.Id = @Id and ca.ActiveStatusId<>3

                            ------------risks
                                select
                                R.Id RiskId,
                                R.Name RiskName
                                from ServiceCatalog.CriticalActivity CA
                                inner join ServiceCatalog.CriticalActivityRisk CAR on CAR.CriticalActivityId = ca.Id and CAR.ActiveStatusId<>3
                                inner join RiskManagement.Risk R on r.Id = CAR.RiskId and R.ActiveStatusId<>3
                                where CA.Id = @Id and ca.ActiveStatusId<>3


                            -----------assignedStaff
                                select
                                cas.StaffId,
                                (p.FirstName + ' ' + p.LastName) StaffFullName,
                                CAS.ResponsilbeTypeId,
                                RT.Name ResponsibleTypeName,
                                C.Id CompanyId,
                                C.Name CompanyName,
                                D.Id DepartmentId,
                                D.Name DepartmentName
                                from ServiceCatalog.CriticalActivity CA
                                inner join ServiceCatalog.CriticalActivityAssignStaff CAS on CAS.CriticalActivityId = ca.Id and CAS.ActiveStatusId<>3
                                inner join Organization.Staff S on s.Id = CAS.StaffId and s.ActiveStatusId<>3
                                inner join Authentication.Profile P on P.Id = S.ProfileId and P.ActiveStatusId<>3
                                inner join Authentication.Users U on U.ProfileID = P.Id and U.ActiveStatusId<>3
                                inner join Organization.Company C on C.Id = U.CompanyId and C.ActiveStatusId<>3
                                inner join Organization.Position PO on PO.Id = S.PositionId and Po.ActiveStatusId <> 3
                                inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
                                inner join Basic.ResponsibleType RT on RT.Id = CAS.ResponsilbeTypeId and rt.ActiveStatusId<>3
                                where CA.Id = @Id and ca.ActiveStatusId<>3

                            ----------executionPlans
                                select distinct
                                CAEP.WeekDay,
                                CAEP.ServiceAvalibilityStartTime,
                                CAEP.ServiceAvalibilityEndTime
                                from ServiceCatalog.CriticalActivity CA
                                inner join ServiceCatalog.CriticalActivityExecutionPlan CAEP on CAEP.CriticalActivityId = ca.Id and CAEP.ActiveStatusId<>3
                                where CA.Id = @Id and ca.ActiveStatusId<>3

 

";
            using (var connection = new SqlConnection(_connectionString))
            {

                await connection.OpenAsync();
                using (var multi = await connection.QueryMultipleAsync(query, new { Id = id, issueId = issueId, userId = _simaIdentity.UserId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
                {
                    response = await multi.ReadFirstOrDefaultAsync<GetCriticalActivityQueryResult>() ?? throw SimaResultException.NotFound;
                    response.NullCheck();
                    response.RelatedServiceList = await multi.ReadAsync<GetCriticalActivityRelatedServiceResult>();
                    response.AssetList = await multi.ReadAsync<GetCriticalActivityAssetQueryResult>();
                    response.ConfigurationItemList = await multi.ReadAsync<GetCriticalActivityConfigurationItemQueryResult>();
                    response.RiskList = await multi.ReadAsync<GetCriticalActivityRiskQueryResult>();
                    response.AssignedStaffList = await multi.ReadAsync<GetCriticalActivityAssignedStaffQueryResult>();
                    response.ExecutionPlanList = await multi.ReadAsync<GetCriticalActivityExecutionPlansQueryResult>();

                }
            }
            return Result.Ok(response);
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }
}

