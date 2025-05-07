using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Assets;

public class AssetQueryRepository : IAssetQueryRepository
{
    private readonly string _connectionString;
    public AssetQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }


    public async Task<Result<IEnumerable<GetAssetQueryResult>>> GetAll(GetAllAssetsQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var mainQuery = @"
       	
	Select 
	    A.[Id],
		A.Code
	    ,A.[SerialNumber]
	    ,A.[Title]
	    ,A.[Model]
		,A.VersionNumber
		,A.Manufacturer
		,A.ManufactureDate
		,A.OwnershipDate
		,A.InServiceDate
		,A.ExpireDate
		,A.RetiredDate
		,A.Description
		,A.OwnershipPaymentValue
		,A.OwnershipPrepaymentValue
		,A.HasConfidentialInformation
		,I.Id IssueId
		,I.Code IssueCode
		,wf.Id WorkflowId
		,wf.Name WorkflowName
		,State.Id CurrentStateId
		,State.Name CurrentStateName
		,S.Id CurrentStepId
		,S.Name CurrentStepName
		,I.CreatedAt IssueCreatedAt 
        ,A.CreatedAt
		,(P.FirstName + ' ' + P.LastName) CreatedBy
	    ,active.[Name] ActiveStatus
	    From AssetAndConfiguration.Asset A
	    join Basic.ActiveStatus active on A.ActiveStatusId = active.Id
		LEFT JOIN Authentication.Users U on U.Id = A.CreatedBy AND U.ActiveStatusId<>3
        LEFT JOIN Authentication.Profile P on P.Id = U.ProfileID AND P.ActiveStatusId<>3
		Inner join IssueManagement.Issue I on I.SourceId = A.Id  and I.ActiveStatusId <>3
		Inner join Project.Step S on S.Id = I.CurrenStepId and S.ActiveStatusId <>3
		inner join Project.WorkFlow wf on wf.Id = S.WorkFlowID
        LEFT JOIN Project.State State on State.Id =I.CurrentStateId	
		WHERE A.[ActiveStatusID] <> 3

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
        var response = await multi.ReadAsync<GetAssetQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<Result<GetAssetQueryInfoResult>> GetByCode(GetAssetByCodeQuery request)
    {
        var mainQuery = @"
SELECT
     Asset.[Id]
    ,Asset.SerialNumber
	,Asset.VersionNumber
    ,Asset.[Code]
    ,Asset.CreatedAt
	,Asset.ManufactureDate
	,Asset.Model
	,Asset.Title
	,Asset.HasConfidentialInformation
	,Asset.Description
	,Asset.ExpireDate
	,Asset.OwnershipDate
	,Asset.InServiceDate
	,Asset.Manufacturer
	,Asset.OwnershipPaymentValue
	,Asset.OwnershipPrepaymentValue
	,Asset.RetiredDate
    ,Asset.[ActiveStatusId]
    ,A.[Name] ActiveStatus
	,(P.FirstName + ' ' + P.LastName) CreatedBy
From AssetAndConfiguration.Asset Asset
JOIN Basic.ActiveStatus A on Asset.ActiveStatusId = A.Id
LEFT JOIN Authentication.Users U on U.Id = Asset.CreatedBy AND U.ActiveStatusId<>3
LEFT JOIN Authentication.Profile P on P.Id = U.ProfileID AND P.ActiveStatusId<>3
WHERE Asset.ActiveStatusId <> 3 AND Asset.Code = @Code

-- DataCenter
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.DataCenter DC on DC.Id = Asset.DataCenterId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- Supplier
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Basic.Supplier DC on DC.Id = Asset.SupplierId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;


--Owner
SELECT
	S.Id
	,(P.FirstName + ' ' + p.LastName) Name
	,S.StaffNumber
	,D.Id DepartmentId
	,D.Name DepartmentName
	,Com.Id CompanyId
	,Com.Name CompanyName
	,PO.BranchId
	,Br.Name BranchName
From AssetAndConfiguration.Asset Asset
inner join Organization.Staff S on s.Id = Asset.OwnerId and s.ActiveStatusId<>3
inner join Organization.Position PO on PO.Id = S.PositionId and Po.ActiveStatusId <> 3
inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
INNER join Organization.Company Com on Com.Id = D.CompanyId and Com.ActiveStatusId<>3
LEFT join Bank.Branch Br on Br.Id = Po.BranchId and Br.ActiveStatusId<>3
inner join Authentication.Profile P on s.ProfileId = p.Id and p.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;


-- WareHouseInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Authentication.Warehouse DC on DC.Id = Asset.WarehouseId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- AssetTypeInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.AssetType DC on DC.Id = Asset.AssetTypeId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- AssetCategoryInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.Category DC on DC.Id = Asset.AssetCategoryId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- OprationStatusInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Asset.OperationalStatus DC on DC.Id = Asset.OperationalStatusId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- AssetTechnicalStatusInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.AssetTechnicalStatus DC on DC.Id = Asset.AssetTechnicalStatusId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- AssetPhysicalStatusInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.AssetPhysicalStatus DC on DC.Id = Asset.AssetPhysicalStatusId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- OwnerShipTypeInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Authentication.OwnershipType DC on DC.Id = Asset.OwnershipTypeId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- UserTypeInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Basic.UserType DC on DC.Id = Asset.UserTypeId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- BusinessCriticalityInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.BusinessCriticality DC on DC.Id = Asset.BusinessCriticalityId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;

-- PhysicalLocationInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Basic.Location DC on DC.Id = Asset.PhysicalLocationId and DC.ActiveStatusId<>3
where Asset.Code = @Code and Asset.ActiveStatusId<>3;
";
		using var connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();
		var multi = await connection.QueryMultipleAsync(mainQuery, new { Code = request.Code });
		var response = new GetAssetQueryInfoResult();
		response = await multi.ReadFirstOrDefaultAsync<GetAssetQueryInfoResult>() ?? throw SimaResultException.NotFound;
		response.DataCenterInfo = await multi.ReadFirstOrDefaultAsync<DataCenterInfo>();
		response.SupplierInfo = await multi.ReadFirstOrDefaultAsync<SupplierInfo>();
		response.OwnerInfo = await multi.ReadFirstOrDefaultAsync<OwnerInfo>();
		response.WareHouseInfo = await multi.ReadFirstOrDefaultAsync<WareHouseInfo>();
		response.AssetTypeInfo = await multi.ReadFirstOrDefaultAsync<AssetTypeInfo>();
		response.AssetCategoryInfo = await multi.ReadFirstOrDefaultAsync<AssetCategoryInfo>();
		response.OprationStatusInfo = await multi.ReadFirstOrDefaultAsync<OprationStatusInfo>();
		response.AssetTechnicalStatusInfo = await multi.ReadFirstOrDefaultAsync<AssetTechnicalStatusInfo>();
		response.AssetPhysicalStatusInfo = await multi.ReadFirstOrDefaultAsync<AssetPhysicalStatusInfo>();
		response.OwnerShipTypeInfo = await multi.ReadFirstOrDefaultAsync<OwnerShipTypeInfo>();
		response.UserTypeInfo = await multi.ReadFirstOrDefaultAsync<UserTypeInfo>();
		response.BusinessCriticalityInfo = await multi.ReadFirstOrDefaultAsync<BusinessCriticalityInfo>();
		response.PhysicalLocationInfo = await multi.ReadFirstOrDefaultAsync<PhysicalLocationInfo>();
		return Result.Ok(response);
    }

    public async Task<GetAssetQueryInfoResult> GetById(long id)
    {
        var mainQuery = @"SELECT
     Asset.[Id]
    ,Asset.SerialNumber
	,Asset.VersionNumber
    ,Asset.[Code]
    ,Asset.CreatedAt
	,Asset.ManufactureDate
	,Asset.Model
	,Asset.Title
	,Asset.HasConfidentialInformation
	,Asset.Description
	,Asset.ExpireDate
	,Asset.OwnershipDate
	,Asset.InServiceDate
	,Asset.Manufacturer
	,Asset.OwnershipPaymentValue
	,Asset.OwnershipPrepaymentValue
	,Asset.RetiredDate
    ,Asset.[ActiveStatusId]
    ,A.[Name] ActiveStatus
	,(P.FirstName + ' ' + P.LastName) CreatedBy
From AssetAndConfiguration.Asset Asset
JOIN Basic.ActiveStatus A on Asset.ActiveStatusId = A.Id
LEFT JOIN Authentication.Users U on U.Id = Asset.CreatedBy AND U.ActiveStatusId<>3
LEFT JOIN Authentication.Profile P on P.Id = U.ProfileID AND P.ActiveStatusId<>3
WHERE Asset.ActiveStatusId <> 3 AND Asset.Id= @Id

-- DataCenter
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.DataCenter DC on DC.Id = Asset.DataCenterId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- Supplier
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Basic.Supplier DC on DC.Id = Asset.SupplierId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;


--Owner
SELECT
	S.Id
	,(P.FirstName + ' ' + p.LastName) Name
	,S.StaffNumber
	,D.Id DepartmentId
	,D.Name DepartmentName
	,Com.Id CompanyId
	,Com.Name CompanyName
	,PO.BranchId
	,Br.Name BranchName
From AssetAndConfiguration.Asset Asset
inner join Organization.Staff S on s.Id = Asset.OwnerId and s.ActiveStatusId<>3
inner join Organization.Position PO on PO.Id = S.PositionId and Po.ActiveStatusId <> 3
inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
INNER join Organization.Company Com on Com.Id = D.CompanyId and Com.ActiveStatusId<>3
LEFT join Bank.Branch Br on Br.Id = Po.BranchId and Br.ActiveStatusId<>3
inner join Authentication.Profile P on s.ProfileId = p.Id and p.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;


-- WareHouseInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Authentication.Warehouse DC on DC.Id = Asset.WarehouseId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- AssetTypeInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.AssetType DC on DC.Id = Asset.AssetTypeId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- AssetCategoryInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.Category DC on DC.Id = Asset.AssetCategoryId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- OprationStatusInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Asset.OperationalStatus DC on DC.Id = Asset.OperationalStatusId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- AssetTechnicalStatusInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.AssetTechnicalStatus DC on DC.Id = Asset.AssetTechnicalStatusId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- AssetPhysicalStatusInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.AssetPhysicalStatus DC on DC.Id = Asset.AssetPhysicalStatusId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- OwnerShipTypeInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Authentication.OwnershipType DC on DC.Id = Asset.OwnershipTypeId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- UserTypeInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Basic.UserType DC on DC.Id = Asset.UserTypeId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- BusinessCriticalityInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN AssetAndConfiguration.BusinessCriticality DC on DC.Id = Asset.BusinessCriticalityId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

-- PhysicalLocationInfo
SELECT 
DC.Id,
DC.Name,
DC.Code
From AssetAndConfiguration.Asset Asset
INNER JOIN Basic.Location DC on DC.Id = Asset.PhysicalLocationId and DC.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;
--AssetCustomFeildValue
select 
ACFV.AssetCustomFieldId,
CFT.Id,
CFT.Name,
ACFV.ItemValue


from AssetAndConfiguration.Asset Asset

inner join Asset.AssetCustomFieldValue ACFV on ACFV.AssetId = Asset.Id
inner join AssetAndConfiguration.AssetCustomField ACF on ACF.Id = ACFV.AssetCustomFieldId
inner join Basic.CustomeFieldType CFT on  CFT.Id = ACF.CustomeFieldTypeId
inner join Asset.AssetCustomFieldOption ACFO on ACFO.AssetCustomFieldId = ACF.Id
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

--AssetCustomFeildOption

select 
	ACFO.Id,
	ACFO.OptionValue,
	ACFO.OptionText,
	ACFV.Id AssetCustomFeildValueId


from AssetAndConfiguration.Asset Asset
inner join Asset.AssetCustomFieldValue ACFV on  ACFV.AssetId = Asset.Id
inner join AssetAndConfiguration.AssetCustomField ACF on ACF.Id = ACFV.AssetCustomFieldId
inner join Basic.CustomeFieldType CFT on  CFT.Id = ACF.CustomeFieldTypeId
inner join Asset.AssetCustomFieldOption ACFO on ACFO.AssetCustomFieldId = ACF.Id
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

--ServiceAsset

select 
 SA.Id,
 SA.ServiceId,
 S.Name,
 S.Code,
 SA.CreatedAt,
 (P.FirstName + ' ' + P.LastName) CreatedBy

from AssetAndConfiguration.Asset Asset
inner join ServiceCatalog.ServiceAsset SA on SA.AssetId =  Asset.Id
join ServiceCatalog.Service S on SA.ServiceId = S.Id
LEFT JOIN Authentication.Users U on U.Id = SA.CreatedBy AND U.ActiveStatusId<>3
LEFT JOIN Authentication.Profile P on P.Id = U.ProfileID AND P.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

--AssetDocument
select 
AD.DocumentId,
D.DocumentTypeId,
D.Name Title,
D.FileExtensionId DocumentExtentionId,
AD.CreatedAt,
(P.FirstName + ' ' + P.LastName) CreatedBy

from AssetAndConfiguration.Asset Asset
inner join AssetAndConfiguration.AssetDocument AD on AD.AssetId = Asset.Id
inner join  DMS.Documents D on D.Id =  AD.DocumentId
LEFT JOIN Authentication.Users U on U.Id = AD.CreatedBy AND U.ActiveStatusId<>3
LEFT JOIN Authentication.Profile P on P.Id = U.ProfileID AND P.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;


--AssetAssignedStaff
select 
	AAS.StaffId,
    (P.FirstName + ' ' + P.LastName) StaffName,
	S.StaffNumber,
	AAS.DepartmentId,
	D.Name DepartmentName,
	D.CompanyId,
	C.Name CompanyName,
	AAS.BranchId,
	B.Name BranchName,
	AAS.ResponsibleTypeId,
	RT.Name ResponsibleTypeName,
    (P.FirstName + ' ' + P.LastName) CreatedBy,
	AAS.CreatedAt


from AssetAndConfiguration.Asset Asset
inner join Asset.AssetAssignedStaffs AAS on  AAS.AssetId =  Asset.Id
inner join Organization.Staff S on  S.Id = AAS.StaffId
inner join Organization.Department D on D.Id =  AAS.DepartmentId
inner join Organization.Company C on C.Id = D.CompanyId
LEFT JOIN Authentication.Profile P on P.Id = S.ProfileID AND P.ActiveStatusId<>3
left join  Bank.Branch B on B.Id = AAS.BranchId
left join Basic.ResponsibleType RT on RT.Id = AAS.ResponsibleTypeId
LEFT JOIN Authentication.Users U on U.Id = AAS.CreatedBy AND U.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;


--ConfigurationItemAsset 

select 
CIA.Id,
Asset.Title Name , 
CI.Code,
CI.VersionNumber,
CIA.CreatedAt,
(P.FirstName + ' ' + P.LastName) CreatedBy



from  AssetAndConfiguration.Asset Asset

left  join AssetAndConfiguration.ConfigurationItemAsset CIA on CIA.AssetId = Asset.Id
left join AssetAndConfiguration.ConfigurationItem CI on CI.Id = CIA.ConfigurationItemId
LEFT JOIN Authentication.Users U on U.Id = CIA.CreatedBy AND U.ActiveStatusId<>3
LEFT JOIN Authentication.Profile P on P.Id = U.ProfileID AND P.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;

--ComplexAsset

select 
CA.Id,
Asset.SerialNumber,
Asset.Title Name,
Asset.Model,
CA.CreatedAt,
(P.FirstName + ' ' + P.LastName) CreatedBy

from AssetAndConfiguration.Asset Asset

inner join AssetAndConfiguration.ComplexAsset CA on CA.AssetId = Asset.Id
LEFT JOIN Authentication.Users U on U.Id = CA.CreatedBy AND U.ActiveStatusId<>3
LEFT JOIN Authentication.Profile P on P.Id = U.ProfileID AND P.ActiveStatusId<>3
where Asset.Id= @Id and Asset.ActiveStatusId<>3;
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var multi = await connection.QueryMultipleAsync(mainQuery, new { Id = id });
        var response = new GetAssetQueryInfoResult();
        response = await multi.ReadFirstOrDefaultAsync<GetAssetQueryInfoResult>() ?? throw SimaResultException.NotFound;
        response.DataCenterInfo = await multi.ReadFirstOrDefaultAsync<DataCenterInfo>();
        response.SupplierInfo = await multi.ReadFirstOrDefaultAsync<SupplierInfo>();
        response.OwnerInfo = await multi.ReadFirstOrDefaultAsync<OwnerInfo>();
        response.WareHouseInfo = await multi.ReadFirstOrDefaultAsync<WareHouseInfo>();
        response.AssetTypeInfo = await multi.ReadFirstOrDefaultAsync<AssetTypeInfo>();
        response.AssetCategoryInfo = await multi.ReadFirstOrDefaultAsync<AssetCategoryInfo>();
        response.OprationStatusInfo = await multi.ReadFirstOrDefaultAsync<OprationStatusInfo>();
        response.AssetTechnicalStatusInfo = await multi.ReadFirstOrDefaultAsync<AssetTechnicalStatusInfo>();
        response.AssetPhysicalStatusInfo = await multi.ReadFirstOrDefaultAsync<AssetPhysicalStatusInfo>();
        response.OwnerShipTypeInfo = await multi.ReadFirstOrDefaultAsync<OwnerShipTypeInfo>();
        response.UserTypeInfo = await multi.ReadFirstOrDefaultAsync<UserTypeInfo>();
        response.BusinessCriticalityInfo = await multi.ReadFirstOrDefaultAsync<BusinessCriticalityInfo>();
        response.PhysicalLocationInfo = await multi.ReadFirstOrDefaultAsync<PhysicalLocationInfo>();
        response.AssetCustomFeildValue = await multi.ReadAsync<AssetCustomFeildValueInfo>();
        var childern = await multi.ReadAsync<AssetCustomFeildOptionInfo>();
        response.ServiceAsset = await multi.ReadAsync<ServiceAssetInfo>();
        response.AssetDocument = await multi.ReadAsync<AssetDocumentInfo>();
        /*
        response.AssetCustomFeildOption = await multi.ReadAsync<AssetCustomFeildOptionInfo>();
        */
        response.AssetAssignedStaff = await multi.ReadAsync<AssetAssignedStaffInfo>();
        response.ConfigurationItemAsset = await multi.ReadAsync<ConfigurationItemAssetInfo>();
        response.ComplexAsset = await multi.ReadAsync<ComplexAssetInfo>();
        
        
        var formPermission =  response.AssetCustomFeildValue.Select(ss => new AssetCustomFeildValueInfo
        {
	        AssetCustomFeildOption = childern.Where(x=>x.AssetCustomFeildValueId == ss.Id).ToList()
        }).ToList();

		return response;
    }
}	