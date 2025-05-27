using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.AssetsAndConfigurations.Apis;

public class ApiQueryRepository : IApiQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ApiQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
Select
     AP.[Id]
    ,AP.[Name]
    ,AP.[Code]
	,AP.Description
    ,AP.CreatedAt
    ,AP.[ActiveStatusId]
    ,A.[Name] ActiveStatus
	,AP.IsInternalApi
	,AP.Prerequisites
	,Ap.BaseUrl
	,AP.IpAddress
	,Ap.ApiAddress
	,Ap.PortNumber
	,Ap.RateLimitingMax
	,Ap.RateLimitingMin
	,AP.ApiTypeId
	,APT.Name ApiTypeName
	,Ap.NetworkProtocolId
	,NP.Name NetworkProtocolName
	,Ap.ApiMethodActionId
	,AMA.Name ApiMethodActionName
	,AP.ApiAuthenticationMethodId
	,AAM.Name ApiAuthenticationMethodName
	,Ap.AuthenticationWorkflow
	,Ap.OwnerResponsibleId
	,P.FirstName + ' ' + p.LastName OwnerResponsibleName
	,Ap.OwnerDepartmentId
	,D.Name DepartmentName
	,Ap.RulesAndConditions
From ServiceCatalog.Api Ap
join Basic.ActiveStatus A on AP.ActiveStatusId = A.Id
LEFT JOIN ServiceCatalog.ApiType APT on APT.Id = AP.ApiTypeId and APT.ActiveStatusId<>3
LEFT JOIN ServiceCatalog.ApiAuthentoicationMethod AAM on AAM.Id = Ap.ApiAuthenticationMethodId and AAM.ActiveStatusId<>3
LEFT JOIN Basic.ApiMethodAction AMA on AMA.Id = Ap.ApiMethodActionId and AMA.ActiveStatusId<>3
LEFT JOIN Basic.NetworkProtocol NP on NP.Id = Ap.NetworkProtocolId and NP.ActiveStatusId<>3
LEFT JOIN Organization.Department D on D.Id = Ap.OwnerDepartmentId and D.ActiveStatusId<>3
LEFT JOIN Organization.Staff S on S.Id = Ap.OwnerResponsibleId and S.ActiveStatusId<>3
LEFT JOIN Authentication.Profile P on P.Id = S.ProfileId and P.ActiveStatusId<>3
WHERE AP.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetApiQueryResult>>> GetAll(GetAllApisQuery request)
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
        var response = await multi.ReadAsync<GetApiQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetApiQueryResult> GetById(GetApiQuery request)
    {
        var query = $@"
         {_mainQuery} 
 AND Ap.[Id] = @Id
-------- apiDocument
select 
	D.Id
	,d.FileAddress DocumentPath
	,DT.Name DocumentTypeName
	,d.DocumentTypeId
	,d.FileExtensionId
	,DE.Name DocumentExtensionName
	,ad.CreatedAt
	,(p2.FirstName + ' ' + P2.LastName) CreatedBy
FROM ServiceCatalog.Api a
inner join ServiceCatalog.ApiDocument ad on ad.ApiId =a.Id
inner join DMS.Documents D on ad.DocumentId = d.Id
inner join DMS.DocumentType DT on d.DocumentTypeId = DT.Id
inner join DMS.DocumentExtension DE on d.FileExtensionId = DE.Id
INNER JOIN Authentication.Users U2 on ad.CreatedBy = U2.Id
INNER JOIN Authentication.Profile P2 on P2.Id = U2.ProfileID
where a.ActiveStatusId <> 3 and ad.ActiveStatusId <> 3 and  a.[Id] = @Id
----- ApiSupportTeam
select
s.Id StaffId,
p.FirstName +' '+p.LastName Staff,
d.Id DepartmentId,
d.Name Department,
b.Id BranchId,
b.Name Branch
from ServiceCatalog.Api a
join ServiceCatalog.ApiSupportTeam ast on a.Id=ast.ApiId
join Organization.Staff s on ast.StaffId= s.Id and s.ActiveStatusId<>3
join Authentication.Profile p on s.ProfileId=p.Id and p.ActiveStatusId<>3
join Organization.Department d on ast.DepartmentId=d.Id and d.ActiveStatusId<>3
join Bank.Branch b on ast.BranchId=b.Id and b.ActiveStatusId<>3
where a.ActiveStatusId <> 3 and a.[Id] = @Id
------ ConfigurationItemApi
select 
c.Id,
c.ConfigurationItemId,
CI.Title ConfigurationItemName
from ServiceCatalog.Api a
join AssetAndConfiguration.ConfigurationItemApi c on a.id=c.ApiId and c.ActiveStatusId<>3
inner join AssetAndConfiguration.ConfigurationItem CI on CI.Id = c.ConfigurationItemId and CI.ActiveStatusId<>3
where a.ActiveStatusId <> 3 and a.[Id] = @Id
------ ApiRequestHeaderParam
select 
rh.Name 
,rh.DataType
,rh.IsMandatory
,rh.ParentId
,rh2.Name Parent
,rh.Description
from ServiceCatalog.Api a
join ServiceCatalog.ApiRequestHeaderParam rh on a.Id=rh.ApiId and rh.ActiveStatusId<>3
left join ServiceCatalog.ApiRequestHeaderParam rh2 on rh.Id=rh2.ParentId
where a.ActiveStatusId <> 3 and a.[Id] = @Id
------ ApiRequestBodyParam
select 
rb.Name 
,rb.DataType
,rb.IsMandatory
,rb.ParentId
,rb2.Name Parent
,rb.Description
from ServiceCatalog.Api a
join ServiceCatalog.ApiRequestBodyParam rb on a.Id=rb.ApiId and rb.ActiveStatusId<>3
left join ServiceCatalog.ApiRequestBodyParam rb2 on rb.Id=rb2.ParentId
where a.ActiveStatusId <> 3 and a.[Id] = @Id
------ ApiResponseHeaderParam
select 
rh.Name 
,rh.DataType
,rh.Description
,rh.ParentId
,rh2.Name Parent
from ServiceCatalog.Api a
join ServiceCatalog.ApiResponseHeaderParam rh on a.Id=rh.ApiId and rh.ActiveStatusId<>3
left join ServiceCatalog.ApiResponseHeaderParam rh2 on rh.Id=rh2.ParentId
where a.ActiveStatusId <> 3 and a.[Id] = @Id
------ ApiResponseBodyParam
select 
rb.Name 
,rb.DataType
,rb.Description
,rb.ParentId
,rb2.Name Parent
from ServiceCatalog.Api a
join ServiceCatalog.ApiResponseBodyParam rb on a.Id=rb.ApiId and rb.ActiveStatusId<>3
left join ServiceCatalog.ApiResponseBodyParam rb2 on rb.Id=rb2.ParentId
--where a.ActiveStatusId <> 3 and a.[Id] = @Id
------ ApiRequestUrlParam
select 
u.Name 
,u.IsMandatory
,u.DataType
,u.Description
,u.ParentId
,u2.Name Parent
from ServiceCatalog.Api a
join ServiceCatalog.ApiRequestUrlParam u on a.Id=u.ApiId and u.ActiveStatusId<>3
left join ServiceCatalog.ApiRequestUrlParam u2 on u2.Id=u.ParentId and u2.ActiveStatusId<>3
where a.ActiveStatusId <> 3 and a.[Id] = @Id
------ ApiRequestQueryStringParam
select 
s.Name 
,s.IsMandatory
,s.DataType
,s.Description
,s.ParentId
,s2.Name Parent
from ServiceCatalog.Api a
join ServiceCatalog.ApiRequestQueryStringParam s on a.Id=s.ApiId and s.ActiveStatusId<>3
left join ServiceCatalog.ApiRequestQueryStringParam s2 on s2.Id=s.ParentId and s2.ActiveStatusId<>3
where a.ActiveStatusId <> 3 and a.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var multi = await connection.QueryMultipleAsync(query, new { Id = request.Id });
        var response = new GetApiQueryResult();
        response = await multi.ReadFirstOrDefaultAsync<GetApiQueryResult>() ?? throw SimaResultException.NotFound;
        response.ApiDocumentList = await multi.ReadAsync<ApiDocumentQueryResult>();
        response.ApiSupportTeamList = await multi.ReadAsync<ApiSupportTeamQueryResult>();
        response.ConfigurationItemApiList = await multi.ReadAsync<ConfigurationItemApiQueryResult>();
        response.ApiRequestHeaderParamList = await multi.ReadAsync<ApiRequestHeaderParamQueryResult>();
        response.ApiRequestBodyParamList = await multi.ReadAsync<ApiRequestBodyParamQueryResult>();
        response.ApiResponseHeaderParamList = await multi.ReadAsync<ApiResponseHeaderParamQueryResult>();
        response.ApiResponseBodyParamList = await multi.ReadAsync<ApiResponseBodyParamQueryResult>();
        response.ApiRequestUrlParamList = await multi.ReadAsync<ApiRequestUrlParamQueryResult>();
        response.ApiRequestQueryStringParamList = await multi.ReadAsync<ApiRequestQueryStringParamQueryResult>();

        return response;
    }
}