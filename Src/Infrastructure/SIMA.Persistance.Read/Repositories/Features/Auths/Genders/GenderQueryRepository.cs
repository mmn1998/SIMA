using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Genders;

public class GenderQueryRepository : IGenderQueryRepository
{
    private readonly string _connectionString;
    public GenderQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetGenderQueryResult> FindById(long id)
    {
        var query = @"SELECT  
                                 g.[ID] Id
                                ,g.[Name]
                                ,g.[Code]
                                ,a.ID ActiveStatusId
                                ,a.Name ActiveStatus
                            FROM [Basic].[Gender] g
                                join Basic.ActiveStatus a
                                on g.ActiveStatusId = a.ID
                                 WHERE g.ActiveStatusID <> 3 And g.ID = @Id";
        using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryFirstOrDefaultAsync<GetGenderQueryResult>(query, new { Id = id });
        result.NullCheck();
        return result;
    }

    public async Task<Result<IEnumerable<GetGenderQueryResult>>> GetAll(GetAllGenderQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @" WITH Query as(
						  SELECT DISTINCT 
     g.[ID] as Id
    ,g.[Name]
    ,g.[Code]
    ,a.ID ActiveStatusId
    ,a.Name ActiveStatus
   ,g.[CreatedAt]
FROM [Basic].[Gender] g
      join Basic.ActiveStatus a on g.ActiveStatusId = a.ID
      WHERE  g.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";

            string query = $@"WITH Query as(
							  SELECT DISTINCT 
     g.[ID] as Id
    ,g.[Name]
    ,g.[Code]
    ,a.ID ActiveStatusId
    ,a.Name ActiveStatus
   ,g.[CreatedAt]
FROM [Basic].[Gender] g
      join Basic.ActiveStatus a on g.ActiveStatusId = a.ID
      WHERE  g.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetGenderQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
}
