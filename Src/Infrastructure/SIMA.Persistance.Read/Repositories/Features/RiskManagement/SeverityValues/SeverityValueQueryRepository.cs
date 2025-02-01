using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.SeverityValues;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.SeverityValues;

public class SeverityValueQueryRepository : ISeverityValueQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public SeverityValueQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT AH.[Id]
              ,AH.[Name]
              ,AH.[Code]
			  ,AH.NumericValue
			  ,AH.Color
              ,AH.ValuingIntervalTitle
	          ,A.[Name] ActiveStatus
              ,AH.CreatedAt
          FROM [RiskManagement].[SeverityValue] AH
          INNER JOIN [Basic].[ActiveStatus] A ON AH.ActiveStatusId = A.ID
WHERE AH.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetSeverityValueQueryResult>>> GetAll(GetAllSeverityValuesQuery request)
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
        var response = await multi.ReadAsync<GetSeverityValueQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetSeverityValueQueryResult> GetById(GetSeverityValueQuery request)
    {
        var query = $@"
          {_mainQuery} AND AH.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetSeverityValueQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}