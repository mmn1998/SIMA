using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.DataProcedures;

public class DataProcedureQueryRepository : IDataProcedureQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public DataProcedureQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
Select
     DP.[Id]
    ,DP.[Name]
    ,DP.[Code]
    ,DP.CreatedAt
    ,DP.[ActiveStatusId]
    ,A.[Name] ActiveStatus
	,DP.DataProcedureTypeId
	,DPT.Name DataProcedureTypeName
	,DP.DatabaseId
	,CI.Title DatabaseName
	,DP.Description
	,DP.IsInternalApi
	,Dp.ReleaseDate
	,DP.VersionNumber
From AssetAndConfiguration.DataProcedure DP
join Basic.ActiveStatus A on DP.ActiveStatusId = A.Id
inner join AssetAndConfiguration.ConfigurationItem CI on CI.Id = DP.DatabaseId and CI.ActiveStatusId<>3
inner join AssetAndConfiguration.DataProcedureType DPT on DPT.Id = Dp.DataProcedureTypeId and DPT.ActiveStatusId<>3
WHERE DP.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetDataProcedureQueryResult>>> GetAll(GetAllDataProceduresQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var queryCount = $@"
                             WITH Query as(	{_mainQuery}	)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

        string query = $@"WITH Query as(
							 {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetDataProcedureQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetDataProcedureQueryResult> GetById(GetDataProcedureQuery request)
    {
        var query = $@"
         {_mainQuery} AND DP.[Id] = @Id

--Documents
select
	BCSD.Id,
	D.Id DocumentId,
	d.Name DocumentFileName,
	d.DocumentTypeId,
	dt.Name DocumentTypeName,
	d.FileExtensionId,
	de.Name DocumentExtensionName,
	s.Name AttachStepName,
	BCSD.CreatedAt,
	P.FirstName + ' ' + P.LastName CreatedBy
From AssetAndConfiguration.DataProcedure DP
inner join AssetAndConfiguration.DataProcedureDocument BCSD on BCSD.DataProcedureId = DP.Id and BCSD.ActiveStatusId<>3
inner join DMS.Documents D on BCSD.DocumentId = D.Id and D.ActiveStatusId<>3
inner join DMS.DocumentType DT on DT.Id = D.DocumentTypeId and DT.ActiveStatusId<>3
inner join DMS.DocumentExtension DE on DE.Id = D.FileExtensionId and DE.ActiveStatusId<>3
left join Project.Step S on S.Id = D.AttachStepId and s.ActiveStatusID<>3
left join Authentication.Users U on U.Id = D.CreatedBy 
left join Authentication.Profile P on P.Id = U.ProfileID
where DP.Id = @Id
order by D.CreatedAt desc

----SupportTeam
select distinct
    ASA.StaffId,
    (p.FirstName + ' ' + p.LastName) StaffFullName,
    C.Id CompanyId,
    C.Name CompanyName,
    D.Id DepartmentId,
    D.Name DepartmentName,
    C.Id CompanyId,
    C.Name CompanyName,
    ASA.BranchId,
    Br.Name BranchName
From AssetAndConfiguration.DataProcedure DP
INNER JOIN AssetAndConfiguration.DataProcedureSupportTeam ASA on ASA.DataProcedureId = DP.Id and ASA.ActiveStatusId<>3
INNER JOIN Organization.Staff Ss on ss.Id = ASA.StaffId and ss.ActiveStatusId<>3
INNER JOIN Authentication.Profile P on P.Id = Ss.ProfileId and P.ActiveStatusId<>3
INNER JOIN Organization.Position PO on PO.Id = SS.PositionId and Po.ActiveStatusId <> 3
INNER JOIN Organization.Department D on D.Id = PO.DepartmentId and D.ActiveStatusId<>3
INNER JOIN Organization.Company C on C.Id = D.CompanyId and C.ActiveStatusId<>3
LEFT JOIN Bank.Branch Br on Br.Id = ASA.BranchId and Br.ActiveStatusId<>3
where DP.Id = @Id

---InputParam
select
	DPIP.Description,
	DPIP.Name,
	DPIP.DataType,
	DPIP.IsMandatory,
	DPIP.ParentId,
	DPIPP.Name ParentName
From AssetAndConfiguration.DataProcedure DP
INNER JOIN [AssetAndConfiguration].[DataProcedureInputParam] DPIP on DPIP.DataProcedureId = DP.Id and DPIP.ActiveStatusId<>3
LEFT JOIN [AssetAndConfiguration].[DataProcedureInputParam] DPIPP on DPIP.ParentId = DPIP.Id and DPIPP.ActiveStatusId<>3
where DP.Id = @Id

---OutputParam
select
	DPIP.Description,
	DPIP.Name,
	DPIP.DataType,
	DPIP.ParentId,
	DPIPP.Name ParentName
From AssetAndConfiguration.DataProcedure DP
INNER JOIN [AssetAndConfiguration].DataProcedureOutputParam DPIP on DPIP.DataProcedureId = DP.Id and DPIP.ActiveStatusId<>3
LEFT JOIN [AssetAndConfiguration].DataProcedureOutputParam DPIPP on DPIP.ParentId = DPIP.Id and DPIPP.ActiveStatusId<>3
where DP.Id = @Id

----ConfigurationItem
select
	CIDP.ConfigurationItemId,
	CI.Title ConfigurationItemName
From AssetAndConfiguration.DataProcedure DP
INNER JOIN Asset.ConfigurationItemDataProcedures CIDP on CIDP.DataProcedureId = DP.Id and CIDP.ActiveStatusId<>3
INNER JOIN AssetAndConfiguration.ConfigurationItem CI on CIDP.ConfigurationItemId = CI.Id and CI.ActiveStatusId<>3
where DP.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id });
        var result = await multi.ReadFirstOrDefaultAsync<GetDataProcedureQueryResult>();
        result.NullCheck();
        result.DocumentList = await multi.ReadAsync<GetDataProcedureDocumentQueryResult>();
        result.SupportTeamList = await multi.ReadAsync<GetDataProcedureSupportTeamQueryResult>();
        result.InputParamList = await multi.ReadAsync<GetDataProcedureInputParamQueryResult>();
        result.OutputParamList = await multi.ReadAsync<GetDataProcedureOutputParamQueryResult>();
        result.CobfigurationItemList = await multi.ReadAsync<GetConfigurationItemDataProcedureResult>();
        return result ?? throw SimaResultException.NotFound;
    }
}