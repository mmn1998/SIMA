using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.ServicePriorities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ServicePriorities;

public class OrganizationalServicePriorityQueryRepository : IOrganizationalServicePriorityQueryRepository
{
    private readonly string _connectionString;
    public OrganizationalServicePriorityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetOrganizationalServicePriorityQueryResult>>> GetAll(GetAllOrganizationalServicePrioritiesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                               Select 
								 F.[Id]
								,F.[Name]
								,F.[Code]
                                ,F.CreatedAt
                                ,F.[Ordering]
								,A.[Name] ActiveStatus
								From [BCP].ServicePriority F
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
                var response = await multi.ReadAsync<GetOrganizationalServicePriorityQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<GetOrganizationalServicePriorityQueryResult> GetById(GetOrganizationalServicePriorityQuery request)
    {
        var query = @"
          SELECT C.[Id]
              ,C.[Name]
              ,C.[Code]
              ,C.[Ordering]
              ,A.[Name] ActiveStatus
          FROM [BCP].[ServicePriority] C
          INNER JOIN [Basic].[ActiveStatus] A ON C.ActiveStatusId = A.ID
          WHERE C.[Id] = @Id AND C.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetOrganizationalServicePriorityQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}