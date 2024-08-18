using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceStatuses
{
    public class ServiceStatusQueryRepository : IServiceStatusQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public ServiceStatusQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"SELECT SS.[Id]
              ,SS.[Name]
              ,SS.[Code]
              ,SS.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ServiceStatus] SS
          INNER JOIN [Basic].[ActiveStatus] A ON SS.ActiveStatusId = A.ID
          WHERE SS.ActiveStatusId <> 3";
        }

        public async Task<Result<IEnumerable<GetServiceStatusesQueryResult>>> GetAll(GetAllServiceStatusesQuery request)
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
                    var response = await multi.ReadAsync<GetServiceStatusesQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<GetServiceStatusesQueryResult> GetById(GetServiceStatusQuery request)
        {
            var query = @"
          SELECT SS.[Id]
              ,SS.[Name]
              ,SS.[Code]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ServiceStatus] SS
          INNER JOIN [Basic].[ActiveStatus] A ON SS.ActiveStatusId = A.ID
          WHERE SS.[Id] = @Id AND SS.ActiveStatusId <> 3";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstAsync<GetServiceStatusesQueryResult>(query, new { Id = request.Id });
                result.NullCheck();
                return result ?? throw SimaResultException.NotFound;
            }
        }
    }
}
