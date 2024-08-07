using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCategories;

public class ServiceCategoryQueryRepository : IServiceCategoryQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ServiceCategoryQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT ST.[Id]
    ,ST.[Name]
    ,ST.[Code]
    ,ST.[ServiceTypeId]
    ,ST.[CreatedAt]
    ,A.[Name] ActiveStatus
FROM [ServiceCatalog].[ServiceCategory] ST
INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
WHERE ST.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetServiceCategoryQueryResult>>> GetAll(GetAllServiceCategoriesQuery request)
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
                var response = await multi.ReadAsync<GetServiceCategoryQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetServiceCategoryQueryResult> GetById(GetServiceCategoryQuery request)
    {
        var query = @"
          SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
              ,ST.[ServiceTypeId]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ServiceCategory] ST
          INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
          WHERE ST.[Id] = @Id AND ST.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetServiceCategoryQueryResult>(query, new { request.Id });
            result.NullCheck();
            return result ?? throw SimaResultException.NotFound;
        }
    }

    public async Task<Result<IEnumerable<GetServiceCategoryQueryResult>>> GetByServiceTypeId(long serviceTypeId)
    {
        var query = @"
          SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
              ,ST.[ServiceTypeId]
          FROM [ServiceCatalog].[ServiceCategory] ST
          WHERE ST.[ServiceTypeId] = @ServiceTypeId AND ST.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<GetServiceCategoryQueryResult>(query, new { ServiceTypeId = serviceTypeId });
            result.NullCheck();
            return Result.Ok(result ?? throw SimaResultException.NotFound);
        }
    }
}