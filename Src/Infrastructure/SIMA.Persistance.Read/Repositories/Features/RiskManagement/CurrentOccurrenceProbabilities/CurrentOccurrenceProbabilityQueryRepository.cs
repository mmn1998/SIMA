using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.CurrentOccurrenceProbabilities;

public class CurrentOccurrenceProbabilityQueryRepository : ICurrentOccurrenceProbabilityQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public CurrentOccurrenceProbabilityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
	IOP.[Id]
    ,IOP.[Code]
	,IOP.CurrentOccurrenceProbabilityValueId
	,IOPV.Name CurrentOccurrenceProbabilityValueName
    ,F.ValueTitle
	,IOP.FrequencyId
	,F.Name FrequencyName
	,IOP.InherentOccurrenceProbabilityValueId
	,IOPV.Name InherentOccurrenceProbabilityValueName
	,A.[Name] ActiveStatus
    ,IOP.CreatedAt
FROM [RiskManagement].[CurrentOccurrenceProbability] IOP
INNER JOIN [Basic].[ActiveStatus] A ON IOP.ActiveStatusId = A.ID
INNER JOIN RiskManagement.Frequency F on F.Id = IOP.FrequencyId AND F.ActiveStatusId<>3
INNER JOIN RiskManagement.CurrentOccurrenceProbabilityValue COPV on COPV.Id = IOP.CurrentOccurrenceProbabilityValueId AND COPV.ActiveStatusId<>3
INNER JOIN RiskManagement.InherentOccurrenceProbabilityValue IOPV on IOPV.Id = IOP.InherentOccurrenceProbabilityValueId AND IOPV.ActiveStatusId<>3
WHERE IOP.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetCurrentOccurrenceProbabilityQueryResult>>> GetAll(GetAllCurrentOccurrenceProbabilitiesQuery request)
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
        var response = await multi.ReadAsync<GetCurrentOccurrenceProbabilityQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetCurrentOccurrenceProbabilityQueryResult> GetById(GetCurrentOccurrenceProbabilityQuery request)
    {
        var query = $@"
          {_mainQuery} AND IOP.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetCurrentOccurrenceProbabilityQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}