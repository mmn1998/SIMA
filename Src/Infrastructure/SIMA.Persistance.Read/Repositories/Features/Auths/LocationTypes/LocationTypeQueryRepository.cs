using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Resources;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;

public class LocationTypeQueryRepository : ILocationTypeQueryRepository
{
    private readonly string _connectionString;

    public LocationTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetLocationTypeQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT LT.[ID] as Id
                                ,LT.[Name]
                                ,LT.[Code]
                                ,LT.[ActiveStatusID]
                                ,A.[Name] as ActiveStatus
	                            ,PLT.[Name] ParentName
                                ,PLT.Id ParentId
                  FROM [Basic].[LocationType] LT
                  left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                  join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                  WHERE LT.[ActiveStatusID] <> 3 AND LT.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetLocationTypeQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException("10059", Messages.LocationTypeNotFoundError);
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetLocationTypeQueryResult>>> GetAll(GetAllLocationTypeQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @"  WITH Query as(
						  SELECT DISTINCT LT.[ID] as Id
      		,LT.[Name]
      		,LT.[Code]
      		,LT.[ActiveStatusID]
      		,A.[Name] as ActiveStatus
      		,PLT.[Name] ParentName
            ,PLT.Id ParentId
      		,lt.[CreatedAt]
FROM [Basic].[LocationType] LT
left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
WHERE  LT.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
            string query = $@"WITH Query as(
							SELECT DISTINCT LT.[ID] as Id
      		,LT.[Name]
      		,LT.[Code]
      		,LT.[ActiveStatusID]
      		,A.[Name] as ActiveStatus
      		,PLT.[Name] ParentName
            ,PLT.Id ParentId
      		,lt.[CreatedAt]
FROM [Basic].[LocationType] LT
left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
WHERE  LT.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetLocationTypeQueryResult>();
                return Result.Ok(response, request, count);
            }
        }

    }
}
