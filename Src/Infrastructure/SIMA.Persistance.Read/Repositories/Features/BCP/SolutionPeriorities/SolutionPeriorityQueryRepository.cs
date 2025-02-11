using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.SolutionPeriorities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.SolutionPeriorities;

public class SolutionPeriorityQueryRepository : ISolutionPeriorityQueryRepository
{
    private readonly string _connectionString;
    public SolutionPeriorityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetSolutionPeriorityQueryResult>>> GetAll(GetAllSolutionPerioritiesQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var mainQuery = @"
                               Select 
								 F.[Id]
								,F.[Name]
								,F.[Code]
                                ,F.CreatedAt
                                ,F.[Priority]
								,A.[Name] ActiveStatus
								From [BCP].SolutionPeriority F
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
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetSolutionPeriorityQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetSolutionPeriorityQueryResult> GetById(GetSolutionPeriorityQuery request)
    {
        var query = @"
          SELECT C.[Id]
              ,C.[Name]
              ,C.[Code]
              ,C.[Priority]
              ,A.[Name] ActiveStatus
          FROM [BCP].[SolutionPeriority] C
          INNER JOIN [Basic].[ActiveStatus] A ON C.ActiveStatusId = A.ID
          WHERE C.[Id] = @Id AND C.ActiveStatusId <> 3";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetSolutionPeriorityQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}