using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.ConfigurationItems;

public class ConfigurationItemQueryRepository : IConfigurationItemQueryRepository
{
    private readonly string _connectionString;
    public ConfigurationItemQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }


    public async Task<Result<IEnumerable<GetConfigurationItemQueryResult>>> GetAll(GetAllConfigurationItemsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                              Select 
	                             CI.[Id]
	                            ,CI.Description
                                ,CI.CreatedAt
	                            ,A.[Name] ActiveStatus
	                            From AssetAndConfiguration.ConfigurationItem CI
	                            join Basic.ActiveStatus A on CI.ActiveStatusId = A.Id
	                            WHERE CI.[ActiveStatusID] <> 3
							";
            var queryCount = $@"
                             WITH Query as(	{mainQuery})
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

            string query = $@"WITH Query as({mainQuery})
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = (queryCount + query).GenerateQuery(request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetConfigurationItemQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<Result<GetConfigurationItemQueryInfoResult>> GetByCode(GetConfigurationItemQuery request)
    {
        var mainQuery = @"
                            select 
                            CI.Id,
                            CI.Code,
                            CI.Title,
                            CI.VersionNumber,
                            CI.ReleaseDate,
                            CI.Description,
                            CI.LicenseTypeId,
                            CI.ActiveStatusId,
                            A.Name ActiveStatus,
                            CI.CreatedAt,
                            P.FirstName + ' ' + P.LastName
                            from AssetAndConfiguration.ConfigurationItem CI 
                            join Authentication.Users U on U.Id = CI.CreatedBy 
                            join Authentication.Profile P on P.Id = U.ProfileID
                            join Basic.ActiveStatus A on A.ID = CI.ActiveStatusId
                            where CI.Code = @Code and CI.ActiveStatusId <> 3


                            ---- OwnerInfo

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
                            from AssetAndConfiguration.ConfigurationItem CI 
                            inner join Organization.Staff S on s.Id = CI.OwnerId and s.ActiveStatusId<>3
                            inner join Organization.Position PO on PO.Id = S.PositionId and Po.ActiveStatusId <> 3
                            inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
                            INNER join Organization.Company Com on Com.Id = D.CompanyId and Com.ActiveStatusId<>3
                            LEFT join Bank.Branch Br on Br.Id = Po.BranchId and Br.ActiveStatusId<>3
                            inner join Authentication.Profile P on s.ProfileId = p.Id and p.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;

                            ---- DataCenter
                            SELECT 
                            DC.Id,
                            DC.Name,
                            DC.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.DataCenter DC on DC.Id = CI.DataCenterId and DC.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;

                            ---- ConfigurationItemType
                            SELECT 
                            CIT.Id,
                            CIT.Name,
                            CIT.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.ConfigurationItemType CIT on CIT.Id = CI.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;

                            ---- ConfigurationItemStatus
                            SELECT 
                            CIS.Id,
                            CIS.Name,
                            CIS.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = CI.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;


                            ---- LicenseType
                            SELECT 
                            LT.Id,
                            LT.Name,
                            LT.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.LicenseType LT on LT.Id = CI.LicenseTypeId and LT.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;


                            -- Supplier
                            SELECT 
                            S.Id,
                            S.Name,
                            S.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN Basic.Supplier S on S.Id = CI.SupplierId and S.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;

                            -- CompanyBuildingLocation
                            SELECT 
                            L.Id,
                            L.Name,
                            L.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN Basic.Location L on L.Id = CI.CompanyBuildingLocationId and CI.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;

                            ---- LicenseSupplier
                            SELECT 
                            LS.Id,
                            LS.Name,
                            LS.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN Basic.Supplier LS on LS.Id = CI.LicenseSupplierId and LS.ActiveStatusId<>3
                            where CI.Code = @Code and CI.ActiveStatusId<>3;
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var multi = await connection.QueryMultipleAsync(mainQuery, new { Code = request.Code });
        var response = new GetConfigurationItemQueryInfoResult();
        response = await multi.ReadFirstOrDefaultAsync<GetConfigurationItemQueryInfoResult>() ?? throw SimaResultException.NotFound;
        response.OwnerInfo = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.OwnerInfo>();
        response.DataCenterInfo = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.DataCenterInfo>();
        response.ConfigurationItemTypeInfo = await multi.ReadFirstOrDefaultAsync<ConfigurationItemTypeInfo>();
        response.ConfigurationItemStatusInfo = await multi.ReadFirstOrDefaultAsync<ConfigurationItemStatusInfo>();
        response.LicenseTypeInfo = await multi.ReadFirstOrDefaultAsync<LicenseTypeInfo>();
        response.SupplierInfo = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.SupplierInfo>();
        response.CompanyBuildingLocation = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.CompanyBuildingLocation>();
        response.LicenseSupplierInfo = await multi.ReadFirstOrDefaultAsync<LicenseSupplierInfo>();
       
        return Result.Ok(response);
    }

    public async Task<GetConfigurationItemQueryInfoResult> GetById(long id)
    {
        var mainQuery = @"
select 
CI.Id,
CI.Code,
CI.Title,
CI.VersionNumber,
CI.ReleaseDate,
CI.Description,
CI.LicenseTypeId,
CI.ActiveStatusId,
A.Name ActiveStatus,
CI.CreatedAt,
P.FirstName + ' ' + P.LastName
from AssetAndConfiguration.ConfigurationItem CI 
join Authentication.Users U on U.Id = CI.CreatedBy 
join Authentication.Profile P on P.Id = U.ProfileID
join Basic.ActiveStatus A on A.ID = CI.ActiveStatusId
where CI.Id= @Id and CI.ActiveStatusId <> 3


---- OwnerInfo

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
from AssetAndConfiguration.ConfigurationItem CI 
inner join Organization.Staff S on s.Id = CI.OwnerId and s.ActiveStatusId<>3
inner join Organization.Position PO on PO.Id = S.PositionId and Po.ActiveStatusId <> 3
inner join Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
INNER join Organization.Company Com on Com.Id = D.CompanyId and Com.ActiveStatusId<>3
LEFT join Bank.Branch Br on Br.Id = Po.BranchId and Br.ActiveStatusId<>3
inner join Authentication.Profile P on s.ProfileId = p.Id and p.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;

---- DataCenter
SELECT 
DC.Id,
DC.Name,
DC.Code
from AssetAndConfiguration.ConfigurationItem CI 
INNER JOIN AssetAndConfiguration.DataCenter DC on DC.Id = CI.DataCenterId and DC.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;

---- ConfigurationItemType
SELECT 
CIT.Id,
CIT.Name,
CIT.Code
from AssetAndConfiguration.ConfigurationItem CI 
INNER JOIN AssetAndConfiguration.ConfigurationItemType CIT on CIT.Id = CI.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;

---- ConfigurationItemStatus
SELECT 
CIS.Id,
CIS.Name,
CIS.Code
from AssetAndConfiguration.ConfigurationItem CI 
INNER JOIN AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = CI.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;


---- LicenseType
SELECT 
LT.Id,
LT.Name,
LT.Code
from AssetAndConfiguration.ConfigurationItem CI 
INNER JOIN AssetAndConfiguration.LicenseType LT on LT.Id = CI.LicenseTypeId and LT.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;


-- Supplier
SELECT 
S.Id,
S.Name,
S.Code
from AssetAndConfiguration.ConfigurationItem CI 
INNER JOIN Basic.Supplier S on S.Id = CI.SupplierId and S.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;

-- CompanyBuildingLocation
SELECT 
L.Id,
L.Name,
L.Code
from AssetAndConfiguration.ConfigurationItem CI 
INNER JOIN Basic.Location L on L.Id = CI.CompanyBuildingLocationId and CI.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;

---- LicenseSupplier
SELECT 
LS.Id,
LS.Name,
LS.Code
from AssetAndConfiguration.ConfigurationItem CI 
INNER JOIN Basic.Supplier LS on LS.Id = CI.LicenseSupplierId and LS.ActiveStatusId<>3
where CI.Id= @Id and CI.ActiveStatusId<>3;
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var multi = await connection.QueryMultipleAsync(mainQuery, new { Id = id });
        var response = new GetConfigurationItemQueryInfoResult();
        response = await multi.ReadFirstOrDefaultAsync<GetConfigurationItemQueryInfoResult>() ?? throw SimaResultException.NotFound;
        response.OwnerInfo = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.OwnerInfo>();
        response.DataCenterInfo = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.DataCenterInfo>();
        response.ConfigurationItemTypeInfo = await multi.ReadFirstOrDefaultAsync<ConfigurationItemTypeInfo>();
        response.ConfigurationItemStatusInfo = await multi.ReadFirstOrDefaultAsync<ConfigurationItemStatusInfo>();
        response.LicenseTypeInfo = await multi.ReadFirstOrDefaultAsync<LicenseTypeInfo>();
        response.SupplierInfo = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.SupplierInfo>();
        response.CompanyBuildingLocation = await multi.ReadFirstOrDefaultAsync<Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems.CompanyBuildingLocation>();
        response.LicenseSupplierInfo = await multi.ReadFirstOrDefaultAsync<LicenseSupplierInfo>();
        return response;
    }
}
