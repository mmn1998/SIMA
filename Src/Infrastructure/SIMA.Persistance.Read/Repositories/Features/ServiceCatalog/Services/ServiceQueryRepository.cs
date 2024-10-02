using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;
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
    public async Task<Result<IEnumerable<GetServiceQueryResult>>> GetAll(GetAllServicesQuery request)
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
      ,S.[InServiceDate]
	  ,W.FileContent WorkflowFileContent
      ,S.[FeedbackUrl]
      ,S.[ActiveStatusId]
      ,S.[CreatedAt]
      ,A.[Name] ActiveStatus
      ,S.[TechnicalSupervisorDepartmentId]
	  ,d.Name TechnicalSupervisorDepartmentName
	  ,d.Code TechnicalSupervisorDepartmentCode
	  ,s.IsCriticalService
	  ,s.IsInternalService
	  ,ss.Name ServiceStatusName
	  ,sp.Name ServicePriorityName
	  ,case when S.CreatedBy = @userId then '1' else '0' end IsEditable
	  ,(p.FirstName + ' ' + P.LastName) CreatedBy
	  ,st.UIPropertyBoxTitle
  FROM [ServiceCatalog].[Service] S
  Inner join Basic.ActiveStatus A on A.ID = S.ActiveStatusId
  inner join IssueManagement.Issue I on I.SourceId = S.Id and i.ActiveStatusId<>3
  inner join Project.Step st on st.Id = i.CurrenStepId
  inner join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  INNER JOIN Authentication.Users U on S.CreatedBy = U.Id and U.ActiveStatusId<>3
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
  left join ServiceCatalog.ServiceCategory SC on SC.Id = S.ServiceCategoryId and SC.ActiveStatusID<>3
  left join Organization.Department D on d.Id =s.TechnicalSupervisorDepartmentId and d.ActiveStatusId<>3
  left join ServiceCatalog.ServiceStatus SS on SS.Id =s.ServiceStatusId and ss.ActiveStatusId<>3
  left join ServiceCatalog.ServicePriority SP on sp.Id =s.ServicePriorityId and sp.ActiveStatusId<>3
  where s.ActiveStatusId<>3;
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

        string relatedEntitiesQuery = @"
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
left join Logistics.Supplier Sup on Sup.Id = a.SupplierId and Sup.ActiveStatusId<>3
left join AssetAndConfiguration.BusinessCriticality BC on BC.Id = A.BusinessCriticalityId and BC.ActiveStatusId<>3
left join AssetAndConfiguration.AssetTechnicalStatusConfiguration ATS on ATS.Id = A.AssetTechnicalStatusId and ATS.ActiveStatusId<>3
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
        left join Logistics.Supplier Sup on Sup.Id = ci.SupplierId and Sup.ActiveStatusId<>3
        left join AssetAndConfiguration.ConfigurationItemType CIT on cit.Id = ci.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
        left join AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = ci.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
        left join AssetAndConfiguration.LicenseType LT on LT.Id = ci.LicenseTypeId and LT.ActiveStatusId<>3
        left join Logistics.Supplier LSup on LSup.Id = ci.LicenseSupplierId and lsup.ActiveStatusId<>3
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
        select
        ASA.StaffId,
        (p.FirstName + ' ' + p.LastName) StaffFullName,
        ASA.ResponsilbeTypeId,
        RT.Name ResponsibleTypeName
        from ServiceCatalog.Service S
        inner join ServiceCatalog.ServiceAssignedStaff ASA on ASA.ServiceId = s.Id and ASA.ActiveStatusId<>3
        inner join Organization.Staff Ss on s.Id = ASA.StaffId and s.ActiveStatusId<>3
        inner join Authentication.Profile P on P.Id = Ss.ProfileId and P.ActiveStatusId<>3
        inner join Basic.ResponsibleType RT on RT.Id = CAS.ResponsilbeTypeId and rt.ActiveStatusId<>3
        where s.Id = @Id and s.ActiveStatusId<>3;
-------- service availablity

        select
        SA.WeekDay,
        sa.ServiceAvalibilityStartTime,
        sa.ServiceAvalibilityEndTime
        from ServiceCatalog.Service S
        inner join ServiceCatalog.ServiceAvalibility SA on SA.ServiceId = s.Id and SA.ActiveStatusId<>3
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();


            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetServiceQueryResult>();
                foreach (var result in response)
                {
                    using (var multiForRelated = await connection.QueryMultipleAsync(relatedEntitiesQuery, new { Id = result.Id }))
                    {
                        result.ServiceCustomerList = await multi.ReadAsync<GetServiceCustomerQueryResult>();
                        result.ServiceUserList = await multi.ReadAsync<GetServiceUserQueryResult>();
                        result.ServicePrerequisiteList = await multi.ReadAsync<GetServicePrerequisiteQueryResult>();
                        result.ServiceProviderList = await multi.ReadAsync<GetServiceProviderQueryResult>();
                        result.ServiceRiskList = await multi.ReadAsync<GetServiceRiskQueryResult>();
                        result.ServiceAssetList = await multi.ReadAsync<GetServiceAssetQueryResult>();
                        result.ServiceConfigurationItemList = await multi.ReadAsync<GetServiceConfigurationItemQueryResult>();
                        result.ServiceProductList = await multi.ReadAsync<GetServiceProductQueryResult>();
                        result.ServiceChannelList = await multi.ReadAsync<GetServiceChannelQueryResult>();
                        result.ServiceAssignedSttafList = await multi.ReadAsync<GetServiceAssignedStaffQueryResult>();
                        result.ServiceAvalibilityList = await multi.ReadAsync<GetServiceAvalibilityQueryResult>();
                    }
                }
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetServiceQueryResult> GetById(long id, long issueId)
    {
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
      ,S.[InServiceDate]
	  ,W.FileContent WorkflowFileContent
      ,S.[FeedbackUrl]
      ,S.[ActiveStatusId]
      ,S.[CreatedAt]
      ,A.[Name] ActiveStatus
      ,S.[TechnicalSupervisorDepartmentId]
	  ,d.Name TechnicalSupervisorDepartmentName
	  ,d.Code TechnicalSupervisorDepartmentCode
	  ,s.IsCriticalService
	  ,s.IsInternalService
	  ,ss.Name ServiceStatusName
	  ,sp.Name ServicePriorityName
	  ,case when S.CreatedBy = @userId then '1' else '0' end IsEditable
	  ,(p.FirstName + ' ' + P.LastName) CreatedBy
	  ,st.UIPropertyBoxTitle
  FROM [ServiceCatalog].[Service] S
  Inner join Basic.ActiveStatus A on A.ID = S.ActiveStatusId
  inner join IssueManagement.Issue I on I.SourceId = S.Id and i.ActiveStatusId<>3
  inner join Project.Step st on st.Id = i.CurrenStepId
  inner join Project.WorkFlow W on w.Id = i.CurrentWorkflowId and w.ActiveStatusID<>3
  INNER JOIN Authentication.Users U on S.CreatedBy = U.Id and U.ActiveStatusId<>3
  INNER JOIN Authentication.Profile P on P.Id = U.ProfileID and P.ActiveStatusId<>3
  left join ServiceCatalog.ServiceCategory SC on SC.Id = S.ServiceCategoryId and SC.ActiveStatusID<>3
  left join Organization.Department D on d.Id =s.TechnicalSupervisorDepartmentId and d.ActiveStatusId<>3
  left join ServiceCatalog.ServiceStatus SS on SS.Id =s.ServiceStatusId and ss.ActiveStatusId<>3
  left join ServiceCatalog.ServicePriority SP on sp.Id =s.ServicePriorityId and sp.ActiveStatusId<>3
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
left join Logistics.Supplier Sup on Sup.Id = a.SupplierId and Sup.ActiveStatusId<>3
left join AssetAndConfiguration.BusinessCriticality BC on BC.Id = A.BusinessCriticalityId and BC.ActiveStatusId<>3
left join AssetAndConfiguration.AssetTechnicalStatusConfiguration ATS on ATS.Id = A.AssetTechnicalStatusId and ATS.ActiveStatusId<>3
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
        left join Logistics.Supplier Sup on Sup.Id = ci.SupplierId and Sup.ActiveStatusId<>3
        left join AssetAndConfiguration.ConfigurationItemType CIT on cit.Id = ci.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
        left join AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = ci.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
        left join AssetAndConfiguration.LicenseType LT on LT.Id = ci.LicenseTypeId and LT.ActiveStatusId<>3
        left join Logistics.Supplier LSup on LSup.Id = ci.LicenseSupplierId and lsup.ActiveStatusId<>3
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
        select
        ASA.StaffId,
        (p.FirstName + ' ' + p.LastName) StaffFullName,
        ASA.ResponsilbeTypeId,
        RT.Name ResponsibleTypeName
        from ServiceCatalog.Service S
        inner join ServiceCatalog.ServiceAssignedStaff ASA on ASA.ServiceId = s.Id and ASA.ActiveStatusId<>3
        inner join Organization.Staff Ss on s.Id = ASA.StaffId and s.ActiveStatusId<>3
        inner join Authentication.Profile P on P.Id = Ss.ProfileId and P.ActiveStatusId<>3
        inner join Basic.ResponsibleType RT on RT.Id = CAS.ResponsilbeTypeId and rt.ActiveStatusId<>3
        where s.Id = @Id and s.ActiveStatusId<>3;
-------- service availablity

        select
        SA.WeekDay,
        sa.ServiceAvalibilityStartTime,
        sa.ServiceAvalibilityEndTime
        from ServiceCatalog.Service S
        inner join ServiceCatalog.ServiceAvalibility SA on SA.ServiceId = s.Id and SA.ActiveStatusId<>3


 ------ IssueInformation
     select
       I.Id
       ,I.Code
       ,I.CurrentWorkflowId WorkflowId
       ,W.Name WorkflowName
      ,W.FileContent WorkFlowFileContent
       ,I.MainAggregateId
       ,I.IssuePriorityId
       ,IP.Name IssuePriorityName
       ,I.IssueWeightCategoryId issueWeightId
       ,IWC.Name issueWeightName
       ,IWC.MaxRange Weight
       ,I.CurrentStateId
       ,State.Name CurrentStateName
       ,I.CurrenStepId CurrentStepId
       ,step.Name CurrentStepName
       ,i.DueDate
         ,Step.HasDocument
         ,Step.DisplayName as StepDisplayName
    ,Step.BpmnId CurrentStepBpmnId
     ServiceCatalog.Service S
     INNER JOIN IssueManagement.Issue I ON I.SourceId = S.Id
     INNER JOIN Project.WorkFlow W ON I.CurrentWorkflowId = W.Id
     INNER JOIN IssueManagement.IssuePriority IP ON IP.Id = I.IssuePriorityId
     INNER JOIN IssueManagement.IssueWeightCategory IWC ON IWC.Id = I.IssueWeightCategoryId
     Left Join Project.State State on State.Id = i.CurrentStateId
     left Join Project.Step Step on Step.Id = i.CurrenStepId
     WHERE CA.Id = @Id and CA.IssueId = @issueId AND CA.ActiveStatusId <> 3 and CA.CreatedBy = @userId

 		     --Comment
 		    select 
             IC.[Id]
             ,IC.[Comment]
             ,IC.[CreatedAt] CommentDate
             ,IC.[CreatedBy]
             ,U.[Username]
             ,(P.FirstName + ' ' + P.LastName) CreatorFullname
             FROM [IssueManagement].[Issue] I
               join [IssueManagement].[IssueComment] IC on I.Id = IC.IssueId
               join [Authentication].[Users] U on Ic.CreatedBy = U.Id
               join [Authentication].[Profile] P on P.Id = U.ProfileID
            Where I.ActiveStatusId != 3 and I.Id = @issueId
            order by IC.CreatedAt desc


--------- IssueApproval
             select 
                  SAO.Id StepApprovalOptionId
                  ,IAP.Description
                  ,Step2.Name StepName
                  ,AO.Name StepApprovalOptionName
                  ,Step2.Id StepId
                  ,WA.Id ActorId
                  ,WA.Name ActorName
                  ,(p2.FirstName + ' ' + P2.LastName) CreatedBy
                  ,(p3.FirstName + ' ' + P3.LastName) ApprovedBy
                  ,IAP.CreatedAt
             ServiceCatalog.Service S
             INNER JOIN IssueManagement.Issue I ON I.SourceId = S.Id
             INNER JOIN IssueManagement.IssueApproval IAP on IAP.IssueId = I.Id
             INNER join Project.StepApprovalOption SAO on SAO.ApprovalOptionId = IAP.Id
             INNER join Project.ApprovalOption AO on AO.Id = SAO.ApprovalOptionId
             INNER join Project.Step Step2 on Step2.Id = SAO.StepId
             INNER join Project.WorkFlowActorStep WAS on WAS.StepID = Step2.Id
             INNER join Project.WorkFlowActor WA on CA.Id = WAS.WorkFlowActorID
             INNER JOIN Authentication.Users U2 on CA.CreatedBy = U2.Id
             INNER JOIN Authentication.Profile P2 on P2.Id = U2.ProfileID
             INNER JOIN Authentication.Users U3 on IAP.ApprovedBy = U3.Id
             INNER JOIN Authentication.Profile P3 on P3.Id = U3.ProfileID
             WHERE CA.Id = @Id AND CA.ActiveStatusId <> 3 and CA.CreatedBy = @userId

------ApprovalOption

            EXEC [Project].[ReturnApprovalList] @IssueId


-------- Progress
        Select distinct
      P.Id CurrentProgressId
      ,FStep.Id ProgressId
      ,s.id 
      ,s.Name
      ,P.HasStoreProcedure HasStoredProcedure
      ,FStep.Name TargetName
      ,s.IsAssigneeForced
      ,wa.IsActorManager
      ,case when FStep.TargetId = 0 then ST.Id else FStep.TargetId end as TargetId
      From  [IssueManagement].[Issue] I
      inner join  [Project].[WorkFlow] W on I.CurrentWorkflowId = W.Id
      inner   join  [Project].[Step] S on I.CurrenStepId = S.Id
      join Project.WorkFlowActorStep WS on S.Id = WS.StepID
      join Project.WorkFlowActor WA on WS.WorkFlowActorID = WA.Id
      left join Project.WorkFlowActorGroup AS WG ON WA.Id = WG.WorkFlowActorID 
      left join Project.WorkFlowActorRole AS WR ON WA.Id = WR.WorkFlowActorID 
      left join  Project.WorkFlowActorUser AS WU ON WA.Id = WU.WorkFlowActorID 
      inner  join  [Project].[Progress] P on S.Id = P.SourceId
      inner   join  [Project].[Step] ST on P.TargetId = ST.Id
      CROSS APPLY [Project].[ReturnNextStepN]   (s.id,w.Id) FStep
      left join  IssueManagement.IssueManager IM on IM.IssueId = I.id
      Where    I.Id = @issueId  and 
                (( (ISNULL(IsDirectManagerOfIssueCreator,0)=0 and  (WU.UserID=@userId or WR.RoleID IN @RoleIds or WG.GroupID IN @GroupIds) )
                                  or (ISNULL(IsDirectManagerOfIssueCreator,0)=1 and IM.UserId=@userId)))
     and I.ActiveStatusId <> 3 and P.ActiveStatusId <> 3 and S.ActiveStatusId <> 3 and ST.ActiveStatusId <> 3

----ProgressStoredProcedureParam

select
                PSP.ProgressId
                ,PSPP.Id ProgressStoredProcedureParamId
                ,PSPP.Name 
                ,PSPP.DisplayName
                ,PSPP.JsonFormat
                ,PSPP.BoundFormat
                ,UI.IsMultiSelect
                ,UI.IsSingleSelect
                ,UI.HasInputInEachRecord
                ,UI.Name UiInputElementName
                ,UI.Id UiInputElementId
                ,PSPP.ApiNameForDataBounding
                ,PSPP.StoredProcedureForDataBounding
                ,PSPP.[TextBoundName]
                ,PSPP.[ValueBoundName]
				,AMA.Name ApiMethodAction
                From  
                        [Project].ProgressStoreProcedure PSP 
                        inner join Project.ProgressStoreProcedureParam PSPP on PSP.Id = pspp.ProgressStoreProcedureId
                        inner join Basic.UIInputElement  UI on UI.Id = pspp.UiInputElementId 
						left join Basic.ApiMethodAction AMA on AMA.Id = PSPP.ApiMethodActionId

                		where Psp.ProgressId In
                		(
                		SELECT FStep.Id 
                        FROM  
						 [IssueManagement].[Issue] I
						 INNER JOIN [Project].[WorkFlow] W ON I.CurrentWorkflowId = W.Id
						 INNER JOIN [Project].[Step] S ON I.CurrenStepId = S.Id
						 CROSS APPLY [Project].[ReturnNextStepN](S.Id, W.Id) FStep
							 WHERE 
							     I.Id = @issueId
							 UNION 
							 SELECT P.Id
							 FROM  
							     [IssueManagement].[Issue] I
							     INNER JOIN [Project].[WorkFlow] W ON I.CurrentWorkflowId = W.Id
							     INNER JOIN [Project].[Step] S ON I.CurrenStepId = S.Id
							     INNER JOIN [Project].[Progress] P ON P.SourceId = S.Id
							 WHERE 
							     I.Id = @issueId
                		)

        -- `StepRequiredDocument

               SELECT 
                    dt.Id DocumentTypeId,
                    dt.Name DocumentTypeName,
                    srd.Count
                FROM  [IssueManagement].[Issue] I
                JOIN  [Project].[Step] S on I.CurrenStepId = S.Id
                left join Project.StepRequiredDocument srd on srd.StepId = s.Id
                left join DMS.DocumentType dt on dt.Id =srd.DocumentTypeId
              Where I.ActiveStatusId != 3 and I.Id =  @issueId and srd.ActiveStatusID <>3
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var multi = await connection.QueryMultipleAsync(query, new { Id = id, issueId = issueId, userId = _simaIdentity.UserId, RoleIds = _simaIdentity.RoleIds, GroupIds = _simaIdentity.GroupId }))
            {
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
                response.IssueInfo = await multi.ReadFirstOrDefaultAsync<IssueInfo>();
                if (response.IssueInfo is not null)
                    response.IssueInfo.IssueCommentList = await multi.ReadAsync<GetIssueCommentQueryResult>();
                response.IssueApprovalList = await multi.ReadAsync<IssueApprovalList>();
                response.RelatedProgressList = await multi.ReadAsync<GetRelatedProgressQueryResult>();
                var storeProcedureParams = await multi.ReadAsync<StoreProcedureParams>();
                var groupedStoreProcedureParams = storeProcedureParams?.GroupBy(x => x.Name);
                response.StepRequiredDocumentList = await multi.ReadAsync<GetStepRequiredDocumentQueryResult>();

                if (response.RelatedProgressList is not null)
                {
                    response.FormParams = groupedStoreProcedureParams?.Select(x => new StoreProcedureParams
                    {
                        Name = x.Key,
                        ProgressStoredProcedureParamId = x.First().ProgressStoredProcedureParamId,
                        StoredProcedureForDataBounding = x.First().StoredProcedureForDataBounding,
                        ApiNameForDataBounding = x.First().ApiNameForDataBounding?.Replace("{Id}", id.ToString()),
                        ApiMethodAction = x.First().ApiMethodAction,
                        DisplayName = x.First().DisplayName,
                        BoundFormat = x.First().BoundFormat,
                        HasInputInEachRecord = x.First().HasInputInEachRecord,
                        IsMultiSelect = x.First().IsMultiSelect,
                        IsSingleSelect = x.First().IsSingleSelect,
                        JsonFormat = x.First().JsonFormat,
                        ProgressId = x.First().ProgressId,
                        UiInputElementId = x.First().UiInputElementId,
                        UiInputElementName = x.First().UiInputElementName,
                        TextBoundName = x.First().TextBoundName,
                        ValueBoundName = x.First().ValueBoundName,
                        DeserializedBoundFormat = !string.IsNullOrEmpty(x.First().BoundFormat) ? JsonConvert.DeserializeObject<IEnumerable<BoundFormatDeserialized>>(x.First().BoundFormat) : null
                    });
                    foreach (var item in response.RelatedProgressList)
                    {
                        if (storeProcedureParams is not null)
                        {
                            var selectedParams = storeProcedureParams.Where(x => x.ProgressId == item.ProgressId).ToList();
                            var currentProject = storeProcedureParams.Where(x => x.ProgressId == item.CurrentProgressId).ToList();
                            if (selectedParams.Count != 0)
                            {
                                item.Params = selectedParams;
                            }
                            if (currentProject.Count != 0 && selectedParams.Count == 0)
                            {
                                item.Params = currentProject;
                            }
                            foreach (var param in item.Params)
                            {
                                if (!string.IsNullOrEmpty(param.BoundFormat))
                                    param.DeserializedBoundFormat = JsonConvert.DeserializeObject<IEnumerable<BoundFormatDeserialized>>(param.BoundFormat);
                            }
                        }
                    }
                }

                /// در تاریخ 10 شهریور 1403، تیم تحلیل نظرش عوض شد و علی رغم داشتن متدی برای دریافت اطلاعات درخواست تدارکات و خرید برای کسی که ثبت کرده، دوباره این قسمت ریفکتور میشود
                #region DisableUploadForCreator
                if (!string.IsNullOrEmpty(response.IsEditable))
                {
                    if (string.Equals("1", response.IsEditable, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (response.IssueInfo is not null)
                            response.IssueInfo.HasDocument = "0";
                        response.FormParams = Enumerable.Empty<StoreProcedureParams>();
                        response.RelatedProgressList = Enumerable.Empty<GetRelatedProgressQueryResult>();
                    }
                }
                #endregion
                ///
            }
        }
        return response;
    }
}
