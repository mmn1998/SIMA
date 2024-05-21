using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
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
                  FROM [Basic].[LocationType] LT
                  left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                  join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                  WHERE LT.[ActiveStatusID] <> 3 AND LT.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetLocationTypeQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException("10059",Messages.LocationTypeNotFoundError);
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetLocationTypeQueryResult>>> GetAll(GetAllLocationTypeQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            if (!string.IsNullOrEmpty(request.Filter) && request.Filter.Contains(":"))
            {
                var splitedFilter = request.Filter.Split(":");
                string? SearchValue = splitedFilter[1].Trim().Sanitize();
                string filterClause = $"{splitedFilter[0].Trim()} Like N'%{SearchValue}%'";
                string queryCount = @$" 
                    SELECT COUNT(*)
                    FROM (
                        SELECT DISTINCT LT.[ID] as Id
              		            ,LT.[Name]
              		            ,LT.[Code]
              		            ,LT.[ActiveStatusID]
              		            ,A.[Name] as ActiveStatus
              		            ,PLT.[Name] ParentName
              		            ,lt.[CreatedAt]
                          FROM [Basic].[LocationType] LT
                          left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                          join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                          WHERE  LT.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                      SELECT DISTINCT LT.[ID] as Id
              		        ,LT.[Name]
              		        ,LT.[Code]
              		        ,LT.[ActiveStatusID]
              		        ,A.[Name] as ActiveStatus
              		        ,PLT.[Name] ParentName
              		        ,lt.[CreatedAt]
                      FROM [Basic].[LocationType] LT
                      left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
                      join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
                      WHERE  LT.ActiveStatusId != 3
                    ) as Query
                    WHERE {filterClause}
                    ORDER BY {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
";
                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetLocationTypeQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                string queryCount = @"
              SELECT COUNT(*) Result
              FROM [Basic].[LocationType] LT
              left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
              join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
              WHERE  LT.ActiveStatusId != 3
              and (@SearchValue is null OR LT.[Name] like @SearchValue or LT.[Code] like @SearchValue)";
                string query = $@"
              SELECT DISTINCT LT.[ID] as Id
              		,LT.[Name]
              		,LT.[Code]
              		,LT.[ActiveStatusID]
              		,A.[Name] as ActiveStatus
              		,PLT.[Name] ParentName
              		,lt.[CreatedAt]
              FROM [Basic].[LocationType] LT
              left JOIN [Basic].[LocationType] PLT on PLT.ID = LT.ParentID
              join [Basic].[ActiveStatus] A on A.Id = LT.ActiveStatusID
              WHERE  LT.ActiveStatusId != 3
              and (@SearchValue is null OR LT.[Name] like @SearchValue or LT.[Code] like @SearchValue)
              order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
              OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetLocationTypeQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }
}
