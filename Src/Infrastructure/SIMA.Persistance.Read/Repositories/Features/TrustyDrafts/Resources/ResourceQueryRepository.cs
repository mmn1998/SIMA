using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.Resources;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.Resources;

public class ResourceQueryRepository : IResourceQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ResourceQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT DO.[Id]
	  ,DO.[Title]
	  ,DO.[Code]
	  ,DO.AccountTypeId
	  ,At.Name AccountTypeName
	  ,DO.BrokerId
	  ,B.Name BrokerName
	  ,DO.ParentId
	  ,DOP.Title ParentTitle
	  ,DO.CurrencyTypeId
	  ,CT.Name CurrencyTypeName
	  ,DO.CurrentBalance
	  ,DO.BlockedBalance
	  ,DO.AvaliableBalance
	  ,DO.AccountNumber
	  ,DO.[CreatedAt]
	  ,A.[Name] ActiveStatus
FROM TrustyDraft.Resource DO
INNER JOIN [Basic].[ActiveStatus] A ON DO.ActiveStatusId = A.ID
INNER JOIN Bank.AccountType AT on AT.Id = DO.AccountTypeId and At.ActiveStatusId<>3
INNER JOIN Bank.Broker B on B.Id = DO.BrokerId and B.ActiveStatusId<>3
INNER JOIN Bank.CurrencyType CT on CT.Id = DO.CurrencyTypeId and CT.ActiveStatusId<>3
LEFT JOIN TrustyDraft.Resource DOP on DOP.Id = DO.ParentId and DOP.ActiveStatusId<>3
WHERE DO.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetResourceQueryResult>>> GetAll(GetAllResourcesQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetResourceQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetResourceQueryResult> GetById(GetResourceQuery request)
    {
        var query = $@"
          {_mainQuery} AND DO.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetResourceQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;

    }
}
