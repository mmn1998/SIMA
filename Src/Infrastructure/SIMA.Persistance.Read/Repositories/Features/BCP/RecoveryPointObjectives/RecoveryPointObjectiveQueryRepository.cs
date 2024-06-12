using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read.Repositories.Features.BCP.Consequences;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.RecoveryPointObjectives;

public class RecoveryPointObjectiveQueryRepository : IRecoveryPointObjectiveQueryRepository
{
    private readonly string _connectionString;
    public RecoveryPointObjectiveQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetRecoveryPointObjectiveQueryResult>>> GetAll(GetAllRecoveryPointObjectivesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               Select 
								 F.[Id]
								,F.[Name]
								,F.[Code]
								,F.[RpoFrom]
								,F.[RpoTo]
                                ,F.CreatedAt
								,A.[Name] ActiveStatus
								From [BCP].RecoveryPointObjective F
								join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
								WHERE F.[ActiveStatusID] <> 3
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
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetRecoveryPointObjectiveQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetRecoveryPointObjectiveQueryResult> GetById(GetRecoveryPointObjectiveQuery request)
    {
        var query = @"
          SELECT C.[Id]
              ,C.[Name]
              ,C.[Code]
			  ,C.[RpoFrom]
			  ,C.[RpoTo]
              ,A.[Name] ActiveStatus
          FROM [BCP].[RecoveryPointObjective] C
          INNER JOIN [Basic].[ActiveStatus] A ON C.ActiveStatusId = A.ID
          WHERE C.[Id] = @Id AND C.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetRecoveryPointObjectiveQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}