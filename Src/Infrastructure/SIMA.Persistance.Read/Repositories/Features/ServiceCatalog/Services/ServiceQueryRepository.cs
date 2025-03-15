using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Services;

public class ServiceQueryRepository : IServiceQueryRepository
{
    private readonly string _connectionString;
    private readonly ISimaIdentity _simaIdentity;

    public ServiceQueryRepository(IConfiguration configuration, ISimaIdentity simaIdentity)
    {
        _connectionString = configuration.GetConnectionString();
        _simaIdentity = simaIdentity;
    }

    public async Task<GetServiceQueryResult> GetByCode(string code, long issueId)
    {
	        try
        {
            var userId = _simaIdentity.UserId;
            var groupIds = _simaIdentity.GroupId;
            var roleIds = _simaIdentity.RoleIds;

            var response = new GetServiceQueryResult();
            string query = @"
                           SELECT S.[Id]
      ,S.[Name]
      ,S.[Code]
      ,S.[ServiceCategoryId]
      ,SC.Name ServiceCategoryName
      ,SC.Code ServiceCategoryCode
      ,SC.ParentId ServiceCategoryParentId
      ,S.[IsCriticalService]
      ,S.[Description]
      ,S.[ServiceStatusId]
      ,S.ServiceDataFlowDiagram  WorkflowFileContent
      ,S.[FeedbackUrl]
      ,S.[ActiveStatusId]
      ,S.[CreatedAt]
      ,S.[IsInternalService]
      ,S.[ServiceCost]
      ,A.[Name] ActiveStatus
      ,C.Name CompanyName
      ,C.Id CompanyId
      ,S.[TechnicalSupervisorDepartmentId]
      ,d.Name TechnicalSupervisorDepartmentName
      ,d.Code TechnicalSupervisorDepartmentCode
      ,s.IsInternalService
      ,ss.Name ServiceStatusName
      ,sp.Name ServicePriorityName
      ,sp.Id ServicePriorityId
      ,(p.FirstName + ' ' + P.LastName) CreatedBy
	  ,ST2.Id ServiceTypeId
	  ,ST2.Name ServiceTypeName
  FROM [ServiceCatalog].[Service] S
  Inner join Basic.ActiveStatus A on A.ID = S.ActiveStatusId
  inner join IssueManagement.Issue I on I.SourceId = S.Id and i.ActiveStatusId<>3
  inner join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  INNER JOIN Authentication.Users U on S.CreatedBy = U.Id and U.ActiveStatusId<>3
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
  left join ServiceCatalog.ServiceCategory SC on SC.Id = S.ServiceCategoryId and SC.ActiveStatusID<>3
  left join Organization.Department D on d.Id =s.TechnicalSupervisorDepartmentId and d.ActiveStatusId<>3
  left join Organization.Company C on D.CompanyId = C.Id  and C.ActiveStatusId<>3
  left join ServiceCatalog.ServiceStatus SS on SS.Id =s.ServiceStatusId and ss.ActiveStatusId<>3
  left join ServiceCatalog.ServicePriority SP on sp.Id =s.ServicePriorityId and sp.ActiveStatusId<>3
  left join ServiceCatalog.ServiceType ST2 on ST2.Id = S.ServiceTypeId and ST2.ActiveStatusId<>3
  where s.Code = @Code and s.ActiveStatusId<>3;
	  

                        ------------ customerTypes
                            SELECT 
	                            sc.ServiceCustomerTypeId CustomerTypeId
	                            ,CT.Name CustomerTypeName
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceCustomer SC on s.Id = SC.ServiceId and sc.ActiveStatusId<>3
                              inner join Basic.CustomerType CT on SC.ServiceCustomerTypeId = CT.Id and ct.ActiveStatusId<>3
                              where s.Code = @Code and s.ActiveStatusId<>3;
                        ------------ userTypes
                            SELECT 
	                            su.ServiceUserTypeId UserTypeId
	                            ,UT.Name UserTypeName
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceUser SU on s.Id = Su.ServiceId and su.ActiveStatusId<>3
                              inner join Basic.UserType UT on Su.ServiceUserTypeId = UT.Id and UT.ActiveStatusId<>3
                              where s.Code = @Code and s.ActiveStatusId<>3;
                        ------------ related services
                            SELECT 
	                        rs.Id,
	                        rs.Name,
	                        rs.Code,
	                        rs.Description,
	                        rs.ServiceCategoryId,
	                        RSC.Name ServiceCategoryName,
	                        RSC.Code ServiceCategoryCode,
	                        rs.ServiceStatusId,
	                        RSS.Name ServiceStatusName,
	                        RSS.Code ServiceStatusCode,
	                        RS.ServicePriorityId,
	                        RSP.Name ServicePriorityName,
	                        rsp.Code ServicePriorityCode,
	                        rs.TechnicalSupervisorDepartmentId,
	                        d.Name TechnicalSupervisorDepartmentName,
	                        d.Code TechnicalSupervisorDepartmentCode
                          FROM [ServiceCatalog].[Service] S
                          inner join ServiceCatalog.PreRequisiteServices PRS on s.Id = PRS.ServiceId and PRS.ActiveStatusId<>3
                          inner join ServiceCatalog.Service RS on rs.Id = PRS.PreRequiredServiceId and rs.ActiveStatusId<>3
                          left join ServiceCatalog.ServiceCategory RSC on RSC.Id = RS.ServiceCategoryId and RSC.ActiveStatusId<>3
                          left join Organization.Department D on RS.TechnicalSupervisorDepartmentId = D.Id and d.ActiveStatusId<>3
                          left join ServiceCatalog.ServiceStatus RSS on RSS.Id = RS.ServiceStatusId and RSS.ActiveStatusId<>3
                          left join ServiceCatalog.ServicePriority RSP on RSP.Id = RS.ServicePriorityId and rsp.ActiveStatusId<>3
                        where s.Code = @Code and s.ActiveStatusId<>3;
                        ------------ providers
                            SELECT 
	                            C.Id ProviderId
	                            ,c.Name ProviderName
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceProvider SP on s.Id = SP.ServiceId and SP.ActiveStatusId<>3
                              inner join Organization.Company C on C.Id = SP.CompanyId and C.ActiveStatusId<>3
                              where s.Code = @Code and s.ActiveStatusId<>3;
                        ------------ risks
                            SELECT 
	                            R.Name RiskName
	                            ,R.Id RiskId
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceRisk SR on s.Id = sr.ServiceId and sr.ActiveStatusId<>3
                              inner join RiskManagement.Risk R on R.Id = sr.RiskId and R.ActiveStatusId<>3
                              where s.Code = @Code and s.ActiveStatusId<>3;
                        ------------ Asset
                            select
                        A.Id,
                        A.SerialNumber,
                        A.Model,
                        A.Manufacturer,
                        A.ManufactureDate,
                        A.OwnershipDate,
                        A.InServiceDate,
                        A.ExpireDate,
                        A.RetiredDate,
                        A.Description,
                        A.Title,
                        A.OwnershipPrepaymentValue,
                        A.OwnershipPaymentValue,
                        A.HasConfidentialInformation,
                        (p.FirstName + ' ' + p.LastName) OwnerFullName,
                        SS.Id OwnerId,
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
                        FROM [ServiceCatalog].[Service] S
                        inner join ServiceCatalog.ServiceAsset SA on s.Id = SA.ServiceId and SA.ActiveStatusId<>3
                          inner join AssetAndConfiguration.Asset A on A.Id = SA.AssetId and A.ActiveStatusId<>3
                        left join Organization.Staff SS on s.Id = a.OwnerId and s.ActiveStatusId<>3
                        left join Authentication.Profile P on p.id = ss.ProfileId and p.ActiveStatusId<>3
                        left join Basic.Supplier Sup on Sup.Id = a.SupplierId and Sup.ActiveStatusId<>3
                        left join AssetAndConfiguration.BusinessCriticality BC on BC.Id = A.BusinessCriticalityId and BC.ActiveStatusId<>3
                        left join AssetAndConfiguration.AssetTechnicalStatus ATS on ATS.Id = A.AssetTechnicalStatusId and ATS.ActiveStatusId<>3
                        left join AssetAndConfiguration.AssetPhysicalStatus APS on APS.Id = A.AssetPhysicalStatusId and APS.ActiveStatusId<>3
                        left join Authentication.Warehouse W on W.Id = A.WarehouseId and W.ActiveStatusId<>3
                        left join Authentication.OwnershipType OT on OT.Id = A.OwnershipTypeId and OT.ActiveStatusId<>3
                        LEFT JOIN Basic.Location L on L.Id = a.PhysicalLocationId and l.ActiveStatusId<>3
                              where s.Code = @Code and s.ActiveStatusId<>3;
                        ------------ ConfigurationItem
                            select
                                SC.Id,
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
                                FROM [ServiceCatalog].[Service] S
                                  inner join ServiceCatalog.ServiceConfigurationItem SC on s.Id = SC.ServiceId and SC.ActiveStatusId<>3
                                  inner join AssetAndConfiguration.ConfigurationItem CI on CI.Id = SC.ConfigurationItemId and CI.ActiveStatusId<>3
                                left join Organization.Staff SS on s.Id = ci.OwnerId and s.ActiveStatusId<>3
                                left join Authentication.Profile P on p.id = ss.ProfileId and p.ActiveStatusId<>3
                                left join Basic.Supplier Sup on Sup.Id = ci.SupplierId and Sup.ActiveStatusId<>3
                                left join AssetAndConfiguration.ConfigurationItemType CIT on cit.Id = ci.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
                                left join AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = ci.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
                                left join AssetAndConfiguration.LicenseType LT on LT.Id = ci.LicenseTypeId and LT.ActiveStatusId<>3
                                left join Basic.Supplier LSup on LSup.Id = ci.LicenseSupplierId and lsup.ActiveStatusId<>3
                                left join Basic.Location L on l.Id = ci.CompanyBuildingLocationId and l.ActiveStatusId<>3
                                where s.Code = @Code and s.ActiveStatusId<>3;


                        ------------ Product
                            SELECT 
	                            p.Id,
	                            p.Name,
	                            p.Code,
	                            p.Scope,
	                            p.Description,
	                            p.ServiceStatusId,
	                            ss.Name ServiceStatusName,
	                            ss.Code ServiceStatusCode,
	                            p.ProviderCompanyId,
	                            c.Name ProviderCompanyName,
	                            c.Code ProviderCompanyCode,
	                            p.InServiceDate
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceChanel SC on s.Id = sc.ServiceId and sc.ActiveStatusId<>3
                              inner join ServiceCatalog.ProductChannel PC on pc.ChannelId = sc.ChannelId and pc.ActiveStatusId<>3
                              inner join ServiceCatalog.Product P on pc.ProductId = p.Id and P.ActiveStatusId<>3
                              left join ServiceCatalog.ServiceStatus SS on ss.Id = p.ServiceStatusId and ss.ActiveStatusId<>3
                              left join Organization.Company C on c.Id = p.ProviderCompanyId and c.ActiveStatusId<>3
                              where s.Code = @Code and s.ActiveStatusId<>3;

                        ------------ Channel
                            SELECT 
	                            c.Id,
	                            c.Name,
	                            c.Code,
	                            c.Scope,
	                            c.Description,
	                            c.ServiceStatusId,
	                            ss.Name ServiceStatusName,
	                            ss.Code ServiceStatusCode,
	                            c.InServiceDate
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceChanel SC on s.Id = sc.ServiceId and sc.ActiveStatusId<>3
                              inner join ServiceCatalog.Channel C on c.Id = sc.ChannelId and c.ActiveStatusId<>3
                              left join ServiceCatalog.ServiceStatus SS on ss.Id = c.ServiceStatusId and ss.ActiveStatusId<>3
                              where s.Code = @Code and s.ActiveStatusId<>3;

                        -------- assign staff
                                select distinct
                                    ASA.StaffId,
                                    (p.FirstName + ' ' + p.LastName) StaffFullName,
                                    ASA.ResponsibleTypeId,
                                    RT.Name ResponsibleTypeName,
                                    C.Id CompanyId,
                                    C.Name CompanyName,
                                    D.Id DepartmentId,
                                    D.Name DepartmentName,
	                                C.Id CompanyId,
	                                C.Name CompanyName,
	                                ASA.BranchId,
	                                Br.Name BranchName
                                    from ServiceCatalog.Service S
                                    inner join ServiceCatalog.ServiceAssignedStaff ASA on ASA.ServiceId = s.Id and ASA.ActiveStatusId<>3
                                    inner join Organization.Staff Ss on ss.Id = ASA.StaffId and ss.ActiveStatusId<>3
                                    inner join Authentication.Profile P on P.Id = Ss.ProfileId and P.ActiveStatusId<>3
                                    inner join Organization.Position PO on PO.Id = SS.PositionId and Po.ActiveStatusId <> 3
                                    inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
                                    inner join Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId<>3
                                    inner join Basic.ResponsibleType RT on RT.Id = ASA.ResponsibleTypeId and rt.ActiveStatusId<>3
	                                LEFT join Bank.Branch Br on Br.Id = ASA.BranchId and Br.ActiveStatusId<>3
                                    where s.Code = @Code and s.ActiveStatusId<>3;
                        -------- service availablity

                                select distinct
                                SA.WeekDay,
                                sa.ServiceAvalibilityStartTime,
                                sa.ServiceAvalibilityEndTime
                                from ServiceCatalog.Service S
                                inner join ServiceCatalog.ServiceAvalibility SA on SA.ServiceId = s.Id and SA.ActiveStatusId<>3
                                where s.Code = @Code and s.ActiveStatusId<>3;
                        ";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var multi = await connection.QueryMultipleAsync(query,
                new { code = code, issueId = issueId, userId = _simaIdentity.UserId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId });
            response = await multi.ReadFirstOrDefaultAsync<GetServiceQueryResult>() ?? throw SimaResultException.NotFound;
            response.NullCheck();
            response.ServiceCustomerList = await multi.ReadAsync<GetServiceCustomerQueryResult>();
            response.ServiceUserList = await multi.ReadAsync<GetServiceUserQueryResult>();
            response.ServicePrerequisiteList = await multi.ReadAsync<GetServicePrerequisiteQueryResult>();
            response.ServiceProviderList = await multi.ReadAsync<GetServiceProviderQueryResult>();
            response.ServiceRiskList = await multi.ReadAsync<GetServiceRiskQueryResult>();
            response.ServiceAssetList = await multi.ReadAsync<GetServiceAssetQueryResult>();
            response.ServiceConfigurationItemList = await multi.ReadAsync<GetServiceConfigurationItemQueryResult>();
            response.ServiceProductList = await multi.ReadAsync<GetServiceProductQueryResult>();
            response.ServiceChannelList = await multi.ReadAsync<GetServiceChannelQueryResult>();
            response.ServiceAssignedSttafList = await multi.ReadAsync<GetServiceAssignedStaffQueryResult>();
            response.ServiceAvalibilityList = await multi.ReadAsync<GetServiceAvalibilityQueryResult>();
            return response;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<Result<IEnumerable<GetAllServicesQueryResult>>> GetAll(GetAllServicesQuery request)
    {
        var mainQuery = @"
SELECT S.[Id]
      ,S.[Name]
      ,S.[Code]
      ,S.[ServiceCategoryId]
	  ,SC.Name ServiceCategoryName
	  ,SC.Code ServiceCategoryCode
	  ,SC.ParentId ServiceCategoryParentId
      ,S.[IsCriticalService]
      ,S.[Description]
      ,S.[ServiceStatusId]
	  ,W.FileContent WorkflowFileContent
      ,S.[FeedbackUrl]
      ,S.[ActiveStatusId]
      ,S.[CreatedAt]
      ,A.[Name] ActiveStatus
      ,S.[TechnicalSupervisorDepartmentId]
	  ,d.Name TechnicalSupervisorDepartmentName
	  ,d.Code TechnicalSupervisorDepartmentCode
	  ,s.IsInternalService
	  ,ss.Name ServiceStatusName
	  ,sp.Name ServicePriorityName
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
	  ,ST2.Id ServiceTypeId
	  ,ST2.Name ServiceTypeName
  FROM [ServiceCatalog].[Service] S
  Inner join ServiceCatalog.ServiceRelatedIssue SRI on SRI.ServiceId = s.Id and SRI.ActiveStatusId<>3
  inner join IssueManagement.Issue I on I.Id = SRI.IssueId and I.MainAggregateId = 1 And i.ActiveStatusId<>3
  left join Basic.ActiveStatus A on A.ID = S.ActiveStatusId
  left join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  left JOIN Authentication.Users U on S.CreatedBy = U.Id and U.ActiveStatusId<>3
  left JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
  left join ServiceCatalog.ServiceCategory SC on SC.Id = S.ServiceCategoryId and SC.ActiveStatusID<>3
  left join Organization.Department D on d.Id =s.TechnicalSupervisorDepartmentId and d.ActiveStatusId<>3
  left join ServiceCatalog.ServiceStatus SS on SS.Id =s.ServiceStatusId and ss.ActiveStatusId<>3
  left join ServiceCatalog.ServicePriority SP on sp.Id =s.ServicePriorityId and sp.ActiveStatusId<>3
  left join Project.Step Step on I.CurrenStepId = Step.Id
  left join Project.WorkFlowActorStep WS on S.Id = WS.StepID
  left join Project.State ST on I.CurrentStateId = ST.Id
  left join Project.Project project on project.Id = W.ProjectID
  left join [IssueManagement].[IssueType] IT on  IT.Id=I.IssueTypeId 
  left join [IssueManagement].[IssuePriority] IPP on  IPP.Id=I.IssuePriorityId 
  left join ServiceCatalog.ServiceType ST2 on ST2.Id = S.ServiceTypeId and ST2.ActiveStatusId<>3 
  where s.ActiveStatusId<>3
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
                var response = await multi.ReadAsync<GetAllServicesQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetServiceQueryResult> GetById(long id, long issueId)
    {


        try
        {
            var userId = _simaIdentity.UserId;
            var groupIds = _simaIdentity.GroupId;
            var roleIds = _simaIdentity.RoleIds;

            var response = new GetServiceQueryResult();
            string query = @"
                        SELECT S.[Id]
      ,S.[Name]
      ,S.[Code]
      ,S.[ServiceCategoryId]
      ,SC.Name ServiceCategoryName
      ,SC.Code ServiceCategoryCode
      ,SC.ParentId ServiceCategoryParentId
      ,S.[IsCriticalService]
      ,S.[Description]
      ,S.[ServiceStatusId]
      ,S.ServiceDataFlowDiagram
      ,S.ContinuousImprovement
      ,S.[FeedbackUrl]
      ,S.[ActiveStatusId]
      ,S.[CreatedAt]
      ,S.[IsInternalService]
      ,S.[ServiceCost]
      ,A.[Name] ActiveStatus
      ,C.Name CompanyName
      ,C.Id CompanyId
      ,S.[TechnicalSupervisorDepartmentId]
      ,d.Name TechnicalSupervisorDepartmentName
      ,d.Code TechnicalSupervisorDepartmentCode
      ,s.IsInternalService
      ,ss.Name ServiceStatusName
      ,sp.Name ServicePriorityName
      ,sp.Id ServicePriorityId
      ,(p.FirstName + ' ' + P.LastName) CreatedBy
	  ,ST2.Id ServiceTypeId
	  ,ST2.Name ServiceTypeName
  FROM [ServiceCatalog].[Service] S
  Inner join Basic.ActiveStatus A on A.ID = S.ActiveStatusId
  inner join IssueManagement.Issue I on I.SourceId = S.Id and i.ActiveStatusId<>3
  inner join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  INNER JOIN Authentication.Users U on S.CreatedBy = U.Id and U.ActiveStatusId<>3
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
  left join ServiceCatalog.ServiceCategory SC on SC.Id = S.ServiceCategoryId and SC.ActiveStatusID<>3
  left join Organization.Department D on d.Id =s.TechnicalSupervisorDepartmentId and d.ActiveStatusId<>3
  left join Organization.Company C on D.CompanyId = C.Id  and C.ActiveStatusId<>3
  left join ServiceCatalog.ServiceStatus SS on SS.Id =s.ServiceStatusId and ss.ActiveStatusId<>3
  left join ServiceCatalog.ServicePriority SP on sp.Id =s.ServicePriorityId and sp.ActiveStatusId<>3
  left join ServiceCatalog.ServiceType ST2 on ST2.Id = S.ServiceTypeId and ST2.ActiveStatusId<>3
  where s.Id = @Id and s.ActiveStatusId<>3;
	  

                        ------------ customerTypes
                            SELECT 
	                            sc.ServiceCustomerTypeId CustomerTypeId
	                            ,CT.Name CustomerTypeName
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceCustomer SC on s.Id = SC.ServiceId and sc.ActiveStatusId<>3
                              inner join Basic.CustomerType CT on SC.ServiceCustomerTypeId = CT.Id and ct.ActiveStatusId<>3
                              where s.Id = @Id and s.ActiveStatusId<>3;
                        ------------ userTypes
                            SELECT 
	                            su.ServiceUserTypeId UserTypeId
	                            ,UT.Name UserTypeName
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceUser SU on s.Id = Su.ServiceId and su.ActiveStatusId<>3
                              inner join Basic.UserType UT on Su.ServiceUserTypeId = UT.Id and UT.ActiveStatusId<>3
                              where s.Id = @Id and s.ActiveStatusId<>3;
                        ------------ related services
                            SELECT 
	                        rs.Id,
	                        rs.Name,
	                        rs.Code,
	                        rs.Description,
	                        rs.ServiceCategoryId,
	                        RSC.Name ServiceCategoryName,
	                        RSC.Code ServiceCategoryCode,
	                        rs.ServiceStatusId,
	                        RSS.Name ServiceStatusName,
	                        RSS.Code ServiceStatusCode,
	                        RS.ServicePriorityId,
	                        RSP.Name ServicePriorityName,
	                        rsp.Code ServicePriorityCode,
	                        rs.TechnicalSupervisorDepartmentId,
	                        d.Name TechnicalSupervisorDepartmentName,
	                        d.Code TechnicalSupervisorDepartmentCode
                          FROM [ServiceCatalog].[Service] S
                          inner join ServiceCatalog.PreRequisiteServices PRS on s.Id = PRS.ServiceId and PRS.ActiveStatusId<>3
                          inner join ServiceCatalog.Service RS on rs.Id = PRS.PreRequiredServiceId and rs.ActiveStatusId<>3
                          left join ServiceCatalog.ServiceCategory RSC on RSC.Id = RS.ServiceCategoryId and RSC.ActiveStatusId<>3
                          left join Organization.Department D on RS.TechnicalSupervisorDepartmentId = D.Id and d.ActiveStatusId<>3
                          left join ServiceCatalog.ServiceStatus RSS on RSS.Id = RS.ServiceStatusId and RSS.ActiveStatusId<>3
                          left join ServiceCatalog.ServicePriority RSP on RSP.Id = RS.ServicePriorityId and rsp.ActiveStatusId<>3
                        where s.Id = @Id and s.ActiveStatusId<>3;
                        ------------ providers
                            SELECT 
	                            C.Id ProviderId
	                            ,c.Name ProviderName
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceProvider SP on s.Id = SP.ServiceId and SP.ActiveStatusId<>3
                              inner join Organization.Company C on C.Id = SP.CompanyId and C.ActiveStatusId<>3
                              where s.Id = @Id and s.ActiveStatusId<>3;
                        ------------ risks
                            SELECT 
	                            R.Name RiskName
	                            ,R.Id RiskId
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceRisk SR on s.Id = sr.ServiceId and sr.ActiveStatusId<>3
                              inner join RiskManagement.Risk R on R.Id = sr.RiskId and R.ActiveStatusId<>3
                              where s.Id = @Id and s.ActiveStatusId<>3;
                        ------------ Asset
                            select
                        A.Id,
                        A.SerialNumber,
                        A.Model,
                        A.Manufacturer,
                        A.ManufactureDate,
                        A.OwnershipDate,
                        A.InServiceDate,
                        A.ExpireDate,
                        A.RetiredDate,
                        A.Description,
                        A.Title,
                        A.OwnershipPrepaymentValue,
                        A.OwnershipPaymentValue,
                        A.HasConfidentialInformation,
                        (p.FirstName + ' ' + p.LastName) OwnerFullName,
                        SS.Id OwnerId,
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
                        FROM [ServiceCatalog].[Service] S
                        inner join ServiceCatalog.ServiceAsset SA on s.Id = SA.ServiceId and SA.ActiveStatusId<>3
                          inner join AssetAndConfiguration.Asset A on A.Id = SA.AssetId and A.ActiveStatusId<>3
                        left join Organization.Staff SS on s.Id = a.OwnerId and s.ActiveStatusId<>3
                        left join Authentication.Profile P on p.id = ss.ProfileId and p.ActiveStatusId<>3
                        left join Basic.Supplier Sup on Sup.Id = a.SupplierId and Sup.ActiveStatusId<>3
                        left join AssetAndConfiguration.BusinessCriticality BC on BC.Id = A.BusinessCriticalityId and BC.ActiveStatusId<>3
                        left join AssetAndConfiguration.AssetTechnicalStatus ATS on ATS.Id = A.AssetTechnicalStatusId and ATS.ActiveStatusId<>3
                        left join AssetAndConfiguration.AssetPhysicalStatus APS on APS.Id = A.AssetPhysicalStatusId and APS.ActiveStatusId<>3
                        left join Authentication.Warehouse W on W.Id = A.WarehouseId and W.ActiveStatusId<>3
                        left join Authentication.OwnershipType OT on OT.Id = A.OwnershipTypeId and OT.ActiveStatusId<>3
                        LEFT JOIN Basic.Location L on L.Id = a.PhysicalLocationId and l.ActiveStatusId<>3
                              where s.Id = @Id and s.ActiveStatusId<>3;
                        ------------ ConfigurationItem
                            select
                                SC.Id,
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
                                FROM [ServiceCatalog].[Service] S
                                  inner join ServiceCatalog.ServiceConfigurationItem SC on s.Id = SC.ServiceId and SC.ActiveStatusId<>3
                                  inner join AssetAndConfiguration.ConfigurationItem CI on CI.Id = SC.ConfigurationItemId and CI.ActiveStatusId<>3
                                left join Organization.Staff SS on s.Id = ci.OwnerId and s.ActiveStatusId<>3
                                left join Authentication.Profile P on p.id = ss.ProfileId and p.ActiveStatusId<>3
                                left join Basic.Supplier Sup on Sup.Id = ci.SupplierId and Sup.ActiveStatusId<>3
                                left join AssetAndConfiguration.ConfigurationItemType CIT on cit.Id = ci.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
                                left join AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = ci.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
                                left join AssetAndConfiguration.LicenseType LT on LT.Id = ci.LicenseTypeId and LT.ActiveStatusId<>3
                                left join Basic.Supplier LSup on LSup.Id = ci.LicenseSupplierId and lsup.ActiveStatusId<>3
                                left join Basic.Location L on l.Id = ci.CompanyBuildingLocationId and l.ActiveStatusId<>3
                                where s.Id = @Id and s.ActiveStatusId<>3;


                        ------------ Product
                            SELECT 
	                            p.Id,
	                            p.Name,
	                            p.Code,
	                            p.Scope,
	                            p.Description,
	                            p.ServiceStatusId,
	                            ss.Name ServiceStatusName,
	                            ss.Code ServiceStatusCode,
	                            p.ProviderCompanyId,
	                            c.Name ProviderCompanyName,
	                            c.Code ProviderCompanyCode,
	                            p.InServiceDate
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceChanel SC on s.Id = sc.ServiceId and sc.ActiveStatusId<>3
                              inner join ServiceCatalog.ProductChannel PC on pc.ChannelId = sc.ChannelId and pc.ActiveStatusId<>3
                              inner join ServiceCatalog.Product P on pc.ProductId = p.Id and P.ActiveStatusId<>3
                              left join ServiceCatalog.ServiceStatus SS on ss.Id = p.ServiceStatusId and ss.ActiveStatusId<>3
                              left join Organization.Company C on c.Id = p.ProviderCompanyId and c.ActiveStatusId<>3
                              where s.Id = @Id and s.ActiveStatusId<>3;

                        ------------ Channel
                            SELECT 
	                            c.Id,
	                            c.Name,
	                            c.Code,
	                            c.Scope,
	                            c.Description,
	                            c.ServiceStatusId,
	                            ss.Name ServiceStatusName,
	                            ss.Code ServiceStatusCode,
	                            c.InServiceDate
                              FROM [ServiceCatalog].[Service] S
                              inner join ServiceCatalog.ServiceChanel SC on s.Id = sc.ServiceId and sc.ActiveStatusId<>3
                              inner join ServiceCatalog.Channel C on c.Id = sc.ChannelId and c.ActiveStatusId<>3
                              left join ServiceCatalog.ServiceStatus SS on ss.Id = c.ServiceStatusId and ss.ActiveStatusId<>3
                              where s.Id = @Id and s.ActiveStatusId<>3;

                        -------- assign staff
                                select distinct
                                    ASA.StaffId,
                                    (p.FirstName + ' ' + p.LastName) StaffFullName,
                                    ASA.ResponsibleTypeId,
                                    RT.Name ResponsibleTypeName,
                                    C.Id CompanyId,
                                    C.Name CompanyName,
                                    D.Id DepartmentId,
                                    D.Name DepartmentName,
	                                C.Id CompanyId,
	                                C.Name CompanyName,
	                                ASA.BranchId,
	                                Br.Name BranchName
                                    from ServiceCatalog.Service S
                                    inner join ServiceCatalog.ServiceAssignedStaff ASA on ASA.ServiceId = s.Id and ASA.ActiveStatusId<>3
                                    inner join Organization.Staff Ss on ss.Id = ASA.StaffId and ss.ActiveStatusId<>3
                                    inner join Authentication.Profile P on P.Id = Ss.ProfileId and P.ActiveStatusId<>3
                                    inner join Organization.Position PO on PO.Id = SS.PositionId and Po.ActiveStatusId <> 3
                                    inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
                                    inner join Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId<>3
                                    inner join Basic.ResponsibleType RT on RT.Id = ASA.ResponsibleTypeId and rt.ActiveStatusId<>3
	                                LEFT join Bank.Branch Br on Br.Id = ASA.BranchId and Br.ActiveStatusId<>3
                                    where s.Id = @Id and s.ActiveStatusId<>3;
                        -------- service availablity

                                select distinct
                                SA.WeekDay,
                                sa.ServiceAvalibilityStartTime,
                                sa.ServiceAvalibilityEndTime
                                from ServiceCatalog.Service S
                                inner join ServiceCatalog.ServiceAvalibility SA on SA.ServiceId = s.Id and SA.ActiveStatusId<>3
                                where s.Id = @Id and s.ActiveStatusId<>3;

------------ organizationProject
    SELECT 
        OP.Id
        ,OP.Name
      FROM [ServiceCatalog].[Service] S
	  inner join ServiceCatalog.ServiceOrganizationalProject SOP on SOP.ServiceId = S.Id and SOP.ActiveStatusId<>3
      inner join ServiceCatalog.OrganizationalProject OP on s.Id = SOP.OrganizationalProjectId and OP.ActiveStatusId<>3
      where s.Id = @Id and s.ActiveStatusId<>3;
                        ";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var multi = await connection.QueryMultipleAsync(query,
                new { Id = id, issueId = issueId, userId = _simaIdentity.UserId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId });
            response = await multi.ReadFirstOrDefaultAsync<GetServiceQueryResult>() ?? throw SimaResultException.NotFound;
            response.NullCheck();
            response.ServiceCustomerList = await multi.ReadAsync<GetServiceCustomerQueryResult>();
            response.ServiceUserList = await multi.ReadAsync<GetServiceUserQueryResult>();
            response.ServicePrerequisiteList = await multi.ReadAsync<GetServicePrerequisiteQueryResult>();
            response.ServiceProviderList = await multi.ReadAsync<GetServiceProviderQueryResult>();
            response.ServiceRiskList = await multi.ReadAsync<GetServiceRiskQueryResult>();
            response.ServiceAssetList = await multi.ReadAsync<GetServiceAssetQueryResult>();
            response.ServiceConfigurationItemList = await multi.ReadAsync<GetServiceConfigurationItemQueryResult>();
            response.ServiceProductList = await multi.ReadAsync<GetServiceProductQueryResult>();
            response.ServiceChannelList = await multi.ReadAsync<GetServiceChannelQueryResult>();
            response.ServiceAssignedSttafList = await multi.ReadAsync<GetServiceAssignedStaffQueryResult>();
            response.ServiceAvalibilityList = await multi.ReadAsync<GetServiceAvalibilityQueryResult>();
            response.OrganizationProjectList = await multi.ReadAsync<GetServiceOrganizationProjectQueryResult>();
            return response;
        }
        catch (Exception ex)
        {

            throw;
        }


    }

    public async Task<string> GetLastCode()
    {
        var query = @"
select top 1 Code from ServiceCatalog.Service
order by CreatedAt desc
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<string>(query) ?? throw SimaResultException.NotFound;
    }
}
