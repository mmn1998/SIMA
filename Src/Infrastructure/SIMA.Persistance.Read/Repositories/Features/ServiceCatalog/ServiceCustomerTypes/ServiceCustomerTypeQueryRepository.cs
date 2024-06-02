using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCustomerTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCustomerTypes;

public class ServiceCustomerTypeQueryRepository : IServiceCustomerTypeQueryRepository
{
    private readonly string _connectionString;
    public ServiceCustomerTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<Result<IEnumerable<GetServiceCustomerTypeQueryResult>>> GetAll(GetAllServiceCustomerTypesQuery request)
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
                                    SELECT ST.[Id]
                                          ,ST.[Name]
                                          ,ST.[Code]
	                                      ,A.[Name] ActiveStatus
                                      FROM [ServiceCatalog].[ServiceCustomerType] ST
                                      INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
                                      WHERE ST.[Id] = @Id AND ST.ActiveStatusId <> 3
                    ) as Query
                    WHERE {filterClause};";
                string query = $@"
                    SELECT *
                    FROM (
                        SELECT ST.[Id]
                            ,ST.[Name]
                            ,ST.[Code]
	                        ,A.[Name] ActiveStatus
                        FROM [ServiceCatalog].[ServiceCustomerType] ST
                        INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
                        WHERE ST.[Id] = @Id AND ST.ActiveStatusId <> 3
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
                    var response = await multi.ReadAsync<GetServiceCustomerTypeQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
            else
            {
                var queryCount = @" SELECT  COUNT(*) Result
                                    FROM [ServiceCatalog].[ServiceCustomerType] ST
                                  INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
                                    WHERE (@SearchValue is null OR  (ST.Name like @SearchValue OR ST.Code like @SearchValue)) AND ST.[ActiveStatusID] <> 3";

                var query = $@"
                              SELECT ST.[Id]
                                      ,ST.[Name]
                                      ,ST.[Code]
                                      ,A.[Name] ActiveStatus
                                  FROM [ServiceCatalog].[ServiceCustomerType] ST
                                  INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
                                    WHERE (@SearchValue is null OR  (ST.Name like @SearchValue OR ST.Code like @SearchValue)) AND ST.[ActiveStatusID] <> 3
                                    order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetServiceCustomerTypeQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
                }
            }
        }
    }

    public async Task<GetServiceCustomerTypeQueryResult> GetById(GetServiceCustomerTypeQuery request)
    {
        var query = @"
          SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ServiceCustomerType] ST
          INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
          WHERE ST.[Id] = @Id AND ST.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetServiceCustomerTypeQueryResult>(query, new { request.Id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
}