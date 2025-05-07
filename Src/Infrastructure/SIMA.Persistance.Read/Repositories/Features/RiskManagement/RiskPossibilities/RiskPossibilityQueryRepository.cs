using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskPossibilities;

public class RiskPossibilityQueryRepository : IRiskPossibilityQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public RiskPossibilityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT DISTINCT R.[Id]
                      ,R.[Name]
                      ,R.[Code]
                      ,R.[Possibility]
                      ,R.[ActiveStatusId]
                      ,R.[CreatedAt]
	                  , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskPossibility] R
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = R.ActiveStatusId
                  WHERE R.ActiveStatusId != 3";
    }

    public async Task<Result<IEnumerable<GetRiskPossibilitiesQueryResult>>> GetAll(GetAllRiskPossibilitiesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
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
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetRiskPossibilitiesQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetRiskPossibilitiesQueryResult> GetById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
              SELECT DISTINCT R.[Id]
                      ,R.[Name]
                      ,R.[Code]
                      ,R.[Possibility]
                      ,R.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[RiskPossibility] R
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = R.ActiveStatusId
                  WHERE R.Id = @Id and R.ActiveStatusId != 3";
            var result = await connection.QueryFirstOrDefaultAsync<GetRiskPossibilitiesQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result ?? throw SimaResultException.NotFound;
        }
    }
}
