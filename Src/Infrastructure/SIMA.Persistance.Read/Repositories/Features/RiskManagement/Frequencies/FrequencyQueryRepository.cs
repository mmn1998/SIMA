using System.Data.SqlClient;
using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.Frequencies;
using SIMA.Application.Query.Contract.Features.RiskManagement.TriggerStatuses;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Frequencies;

public class FrequencyQueryRepository : IFrequencyQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;

    public FrequencyQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();;
        _mainQuery = @"SELECT DISTINCT T.[Id]
                      ,T.[Name]
                      ,T.[Code]
                      ,T.[ActiveStatusId]
					  ,T.[NumericValue]
					  ,T.[ValueTitle]
                      ,T.[CreatedAt]
	                  , S.[Name] as ActiveStatus
                  FROM [RiskManagement].Frequency T
				  INNER JOIN [Basic].[ActiveStatus] S on S.ID = T.ActiveStatusId
                  WHERE T.ActiveStatusId != 3";
    }
    public async Task<Result<IEnumerable<GetFrequencyQueryResult>>> GetAll(GetAllFrequenciesQuery request)
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
                var response = await multi.ReadAsync<GetFrequencyQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetFrequencyQueryResult> GetById(long id)
    {
	    using (var connection = new SqlConnection(_connectionString))
	    {
		    await connection.OpenAsync();
		    string query = @"
              SELECT DISTINCT T.[Id]
                      ,T.[Name]
                      ,T.[Code]
                      ,T.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
                  FROM [RiskManagement].[Frequency] T
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = T.ActiveStatusId
                  WHERE T.Id = @Id and T.ActiveStatusId != 3";
		    var result = await connection.QueryFirstOrDefaultAsync<GetFrequencyQueryResult>(query, new { Id = id });
		    result.NullCheck();
		    return result ?? throw SimaResultException.NotFound;
	    }
    }
}