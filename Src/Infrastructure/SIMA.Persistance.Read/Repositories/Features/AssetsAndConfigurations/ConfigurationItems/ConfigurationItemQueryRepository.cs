using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;
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
        using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();
        var mainQuery = @"
                              

                   Select 
 CI.[Id]
			,CI.Title
			,CI.OwnerId
			,P.FirstName + ' ' + P.LastName as OwnerName
			,CI.VersionNumber
			,CI.ReleaseDate
         ,CI.Description
			,CI.LastUpdateDate
			,CI.ConfigurationItemTypeId
			,CIT.Name ConfigurationItemType
			,CI.ConfigurationItemStatusId
			,CIS.Name ConfigurationItemStatusName
			,CI.LicenseSupplierId
			,LicenseSupplier.Name LicenseSupplierName  
			,CI.TimeMeasurementId
			,TM.Name TimeMeasurementName
			,CI.Uptime
			,CI.Mttr
			,CI.SupplierId
			,Supplier.Name SupplierName
			,CI.BusinessCriticalityId
			,BC.Name BusinessCriticalityName
			,CI.CompanyBuildingLocationId
			,CBL.Name CompanyBuildingLocationName
			,CI.Code
			,CI.CreatedAt
			,CI.ActiveStatusId
			,A.[Name] ActiveStatus
			,I.Id IssueId
			,I.Code IssueCode
			,Profiles.FirstName + ' ' + Profiles.LastName as CreatedBy

From AssetAndConfiguration.ConfigurationItem CI
join Basic.ActiveStatus A on CI.ActiveStatusId = A.Id
			inner join Organization.Staff s on s.Id = CI.OwnerId
			inner join Authentication.Profile P ON P.Id = s.ProfileId
			inner join AssetAndConfiguration.ConfigurationItemType CIT on CIT.Id = CI.ConfigurationItemTypeId
			inner join AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = CI.ConfigurationItemStatusId
			left join Basic.Supplier LicenseSupplier on S.Id = CI.LicenseSupplierId 
			join Basic.TimeMeasurement TM on TM.Id = CI.TimeMeasurementId
			left join Basic.Supplier Supplier on S.Id = CI.SupplierId 
			inner join AssetAndConfiguration.BusinessCriticality BC on BC.Id = CI.BusinessCriticalityId
			left join Authentication.CompanyBuildingLocation CBL on CBL.Id = CI.CompanyBuildingLocationId
			join Authentication.Users Users on CI.CreatedBy = Users.Id
         join Authentication.Profile Profiles on Users.ProfileID = Profiles.Id
			left join AssetAndConfiguration.ConfigurationItemIssue CIIssue on CIIssue.ConfigurationItemId= CI.Id
			left  join IssueManagement.Issue I on I.Id = CIIssue.IssueId and I.ActiveStatusId<> 3
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
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetConfigurationItemQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<Result<IEnumerable<GetConfigurationItemQueryResult>>> GetAllDataBase(GetAllDataBaseConfigurationItemQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var mainQuery = @"
                              Select 
                                   CI.[Id]
                                  ,CI.Description
                                  ,CI.CreatedAt
                                  ,A.[Name] ActiveStatus
                                  From AssetAndConfiguration.ConfigurationItem CI
                                  join Basic.ActiveStatus A on CI.ActiveStatusId = A.Id
                                  WHERE CI.[ActiveStatusID] <> 3 and CI.ConfigurationItemTypeId = 1 
							";//فقط اقلام پیکربندی که نوع آن ها database  می باشد
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
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetConfigurationItemQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<Result<IEnumerable<GetConfigurationItemComboQueryResult>>> GetConfigurationItemCombo(GetConfigurationItemComboQuery request)
    {
	    var mainQuery = @"SELECT
							     ConfigurationItem.[Id]
								,ConfigurationItem.Title Name
								From AssetAndConfiguration.ConfigurationItem ConfigurationItem
							WHERE ConfigurationItem.ActiveStatusId <> 3 
							";
	    
	    using var connection = new SqlConnection(_connectionString);
	    await connection.OpenAsync();
	    using var multi = await connection.QueryMultipleAsync(mainQuery);
	    var response = await multi.ReadAsync<GetConfigurationItemComboQueryResult>();
	    return Result.Ok(response);
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
                            CI.Mtbf,
                            CI.Mttr,
                            CI.Uptime,
                            CI.TimeMeasurementId,
                            Tm.Name TimeMeasurementName,
                            CI.LicenseTypeId,
                            CI.ActiveStatusId,
                            A.Name ActiveStatus,
                            CI.CreatedAt,
                            P.FirstName + ' ' + P.LastName CreatedBy
                            from AssetAndConfiguration.ConfigurationItem CI 
                            join Authentication.Users U on U.Id = CI.CreatedBy 
                            join Authentication.Profile P on P.Id = U.ProfileID
                            join Basic.ActiveStatus A on A.ID = CI.ActiveStatusId
                            left join Basic.TimeMeasurement TM on TM.Id = CI.TimeMeasurementId and TM.ActiveStatusId<>3
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


                                    ---- BusinessCriticality

                                    select 
                                    BC.Id ,
                                    BC.Name,
                                    BC.Code
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.BusinessCriticality BC on BC.Id = CI.BusinessCriticalityId
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code




                                    ---- ConfigurationItemCustomFieldValue

                                    select 
                                    CICFV.Id ,
                                    CICFV.ItemValue
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemCustomFieldValue CICFV on CICFV.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code



                                    ---- ConfigurationItemSupportTeam

                                    select 
                                    CIST.Id ,
                                    CIST.MainStaffId,
                                    CIST.MainDepartmentId,
                                    CIST.MainBranchId,
                                    CIST.SubsitutedStaffId,
                                    CIST.SubsitutedDepartmentId,
                                    CIST.SubsitutedBranchId
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemSupportTeam CIST on CIST.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code


                                    ---- ConfigurationItemAccessInfo

                                    select 
                                    CIAI.Id ,
                                    CIAI.IPAddressFrom,
                                    CIAI.IPAddressTo,
                                    CIAI.PortFrom,
                                    CIAI.PortTo,
                                    CIAI.ActiveFrom,
                                    CIAI.ActiveTo,
                                    CIAI.ActiveStatusId
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemAccessInfo CIAI on CIAI.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code



                                    ---- ConfigurationItemBackupSchedule

                                    select 
                                    CIBS.Id ,
                                    CIBS.BackupConfigurationItemId,
                                    CIBS.DataCenterId,
                                    CIBS.BackupMethodId,
                                    CIBS.StartTime,
                                    CIBS.TimeMeasurementId,
                                    CIBS.Duration,
                                    CIBS.LastTestDate,
                                    CIBS.ActiveStatusId
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemBackupSchedule CIBS on CIBS.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code


                                    ---- ConfigurationItemApi

                                    select 
                                    CIIA.Id 
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemApi CIIA on CIIA.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code


                                    ---- ConfigurationItemDataProcedure

                                    select 
                                    CIDP.Id
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join Asset.ConfigurationItemDataProcedures CIDP on CIDP.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code


                                    ---- ServiceConfigurationItem

                                    select 
                                    SCI.Id
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join ServiceCatalog.ServiceConfigurationItem SCI on SCI.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code


                                    ---- ConfigurationItemAsset

                                    select 
                                    SCA.Id
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemAsset SCA on SCA.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code


                                    ---- ConfigurationItemDocument

                                    select 
                                    CID.DocumentId,
                                    CID.ConfigurationItemId,
                                    D.ActiveStatusId,
                                    D.CreatedAt
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemDocument CID on CID.ConfigurationItemId = CI.Id
                                    join DMS.Documents D on CID.DocumentId = D.Id 
                                    where CI.ActiveStatusId <> 3 and CI.Code = @Code
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
        response.BusinessCriticalityInfo = await multi.ReadFirstOrDefaultAsync<BusinessCriticalityInfo>();
        response.ConfigurationItemCustomFieldValueList = await multi.ReadAsync<ConfigurationItemCustomFieldValueList>();
        response.ConfigurationItemSupportTeamList = await multi.ReadAsync<ConfigurationItemSupportTeamList>();
        response.ConfigurationItemAccessList = await multi.ReadAsync<ConfigurationItemAccessList>();
        response.ConfigurationItemBackupScheduleList = await multi.ReadAsync<ConfigurationItemBackupScheduleList>();
        response.ConfigurationItemApiList = await multi.ReadAsync<ConfigurationItemApiList>();
        response.ConfigurationItemDataProcedureList = await multi.ReadAsync<ConfigurationItemDataProcedureList>();
        response.ServiceConfigurationItemList = await multi.ReadAsync<ServiceConfigurationItemList>();
        response.ConfigurationItemAssetList = await multi.ReadAsync<ConfigurationItemAssetList>();
        response.ConfigurationItemDocumentList = await multi.ReadAsync<ConfigurationItemDocumentList>();

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
                            where CI.Id = @Id and CI.ActiveStatusId <> 3


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
                            where CI.Id = @Id and CI.ActiveStatusId<>3;

                            ---- DataCenter
                            SELECT 
                            DC.Id,
                            DC.Name,
                            DC.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.DataCenter DC on DC.Id = CI.DataCenterId and DC.ActiveStatusId<>3
                            where CI.Id = @Id and CI.ActiveStatusId<>3;

                            ---- ConfigurationItemType
                            SELECT 
                            CIT.Id,
                            CIT.Name,
                            CIT.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.ConfigurationItemType CIT on CIT.Id = CI.ConfigurationItemTypeId and CIT.ActiveStatusId<>3
                            where CI.Id = @Id and CI.ActiveStatusId<>3;

                            ---- ConfigurationItemStatus
                            SELECT 
                            CIS.Id,
                            CIS.Name,
                            CIS.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.ConfigurationItemStatus CIS on CIS.Id = CI.ConfigurationItemStatusId and CIS.ActiveStatusId<>3
                            where CI.Id = @Id and CI.ActiveStatusId<>3;


                            ---- LicenseType
                            SELECT 
                            LT.Id,
                            LT.Name,
                            LT.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN AssetAndConfiguration.LicenseType LT on LT.Id = CI.LicenseTypeId and LT.ActiveStatusId<>3
                            where CI.Id = @Id and CI.ActiveStatusId<>3;


                            -- Supplier
                            SELECT 
                            S.Id,
                            S.Name,
                            S.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN Basic.Supplier S on S.Id = CI.SupplierId and S.ActiveStatusId<>3
                            where CI.Id = @Id and CI.ActiveStatusId<>3;

                            -- CompanyBuildingLocation
                            SELECT 
                            L.Id,
                            L.Name,
                            L.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN Basic.Location L on L.Id = CI.CompanyBuildingLocationId and CI.ActiveStatusId<>3
                            where CI.Id = @Id and CI.ActiveStatusId<>3;

                            ---- LicenseSupplier
                            SELECT 
                            LS.Id,
                            LS.Name,
                            LS.Code
                            from AssetAndConfiguration.ConfigurationItem CI 
                            INNER JOIN Basic.Supplier LS on LS.Id = CI.LicenseSupplierId and LS.ActiveStatusId<>3
                            where CI.Id = @Id and CI.ActiveStatusId<>3;


                                    ---- BusinessCriticality

                                    select 
                                    BC.Id ,
                                    BC.Name,
                                    BC.Code
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.BusinessCriticality BC on BC.Id = CI.BusinessCriticalityId
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id




                                    ---- ConfigurationItemCustomFieldValue

                                    select 
                                    CICFV.Id ,
                                    CICFV.ItemValue
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemCustomFieldValue CICFV on CICFV.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id



                                    ---- ConfigurationItemSupportTeam

                                    select 
                                    CIST.Id ,
                                    CIST.MainStaffId,
                                    CIST.MainDepartmentId,
                                    CIST.MainBranchId,
                                    CIST.SubsitutedStaffId,
                                    CIST.SubsitutedDepartmentId,
                                    CIST.SubsitutedBranchId
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemSupportTeam CIST on CIST.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id


                                    ---- ConfigurationItemAccessInfo

                                    select 
                                    CIAI.Id ,
                                    CIAI.IPAddressFrom,
                                    CIAI.IPAddressTo,
                                    CIAI.PortFrom,
                                    CIAI.PortTo,
                                    CIAI.ActiveFrom,
                                    CIAI.ActiveTo,
                                    CIAI.ActiveStatusId
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemAccessInfo CIAI on CIAI.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id



                                    ---- ConfigurationItemBackupSchedule

                                    select 
                                    CIBS.Id ,
                                    CIBS.BackupConfigurationItemId,
                                    CIBS.DataCenterId,
                                    CIBS.BackupMethodId,
                                    CIBS.StartTime,
                                    CIBS.TimeMeasurementId,
                                    CIBS.Duration,
                                    CIBS.LastTestDate,
                                    CIBS.ActiveStatusId
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemBackupSchedule CIBS on CIBS.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id


                                    ---- ConfigurationItemApi

                                    select 
                                    CIIA.Id 
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemApi CIIA on CIIA.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id


                                    ---- ConfigurationItemDataProcedure

                                    select 
                                    CIDP.Id
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join Asset.ConfigurationItemDataProcedures CIDP on CIDP.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id


                                    ---- ServiceConfigurationItem

                                    select 
                                    SCI.Id
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join ServiceCatalog.ServiceConfigurationItem SCI on SCI.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id


                                    ---- ConfigurationItemAsset

                                    select 
                                    SCA.Id
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemAsset SCA on SCA.ConfigurationItemId = CI.Id
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id


                                    ---- ConfigurationItemDocument

                                    select 
                                    CID.DocumentId,
                                    CID.ConfigurationItemId,
                                    D.ActiveStatusId,
                                    D.CreatedAt
                                    from AssetAndConfiguration.ConfigurationItem CI
                                    join AssetAndConfiguration.ConfigurationItemDocument CID on CID.ConfigurationItemId = CI.Id
                                    join DMS.Documents D on CID.DocumentId = D.Id 
                                    where CI.ActiveStatusId <> 3 and CI.Id = @Id
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
        response.BusinessCriticalityInfo = await multi.ReadFirstOrDefaultAsync<BusinessCriticalityInfo>();
        response.ConfigurationItemCustomFieldValueList = await multi.ReadAsync<ConfigurationItemCustomFieldValueList>();
        response.ConfigurationItemSupportTeamList = await multi.ReadAsync<ConfigurationItemSupportTeamList>();
        response.ConfigurationItemAccessList = await multi.ReadAsync<ConfigurationItemAccessList>();
        response.ConfigurationItemBackupScheduleList = await multi.ReadAsync<ConfigurationItemBackupScheduleList>();
        response.ConfigurationItemApiList = await multi.ReadAsync<ConfigurationItemApiList>();
        response.ConfigurationItemDataProcedureList = await multi.ReadAsync<ConfigurationItemDataProcedureList>();
        response.ServiceConfigurationItemList = await multi.ReadAsync<ServiceConfigurationItemList>();
        response.ConfigurationItemAssetList = await multi.ReadAsync<ConfigurationItemAssetList>();
        response.ConfigurationItemDocumentList = await multi.ReadAsync<ConfigurationItemDocumentList>();
        return response;
    }
}
