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
	                             A.[Id]
	                            ,A.[SerialNumber]
	                            ,A.[Title]
	                            ,A.[Model]
                                ,A.CreatedAt
	                            ,active.[Name] ActiveStatus
	                            From AssetAndConfiguration.Asset A
	                            join Basic.ActiveStatus active on A.ActiveStatusId = active.Id
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
		return response;
    }
}	