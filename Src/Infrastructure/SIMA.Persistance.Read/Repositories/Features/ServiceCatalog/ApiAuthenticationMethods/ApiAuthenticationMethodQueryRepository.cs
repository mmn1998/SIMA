using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiAuthenticationMethods;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ApiAuthenticationMethods
{
    public class ApiAuthenticationMethodQueryRepository : IApiAuthenticationMethodQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public ApiAuthenticationMethodQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"SELECT AAM.[Id]
              ,AAM.[Name]
              ,AAM.[Code]
              ,AAM.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ApiAuthentoicationMethod] AAM
          INNER JOIN [Basic].[ActiveStatus] A ON AAM.ActiveStatusId = A.ID
          WHERE AAM.ActiveStatusId <> 3";
        }

        public async Task<Result<IEnumerable<GetApiAuthenticationMethodsQueryResult>>> GetAll(GetAllApiAuthenticationMethodsQuery request)
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
                    var response = await multi.ReadAsync<GetApiAuthenticationMethodsQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<GetApiAuthenticationMethodsQueryResult> GetById(GetApiAuthenticationMethodQuery request)
        {
            var query = @"
          SELECT AAM.[Id]
              ,AAM.[Name]
              ,AAM.[Code]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ApiAuthentoicationMethod] AAM
          INNER JOIN [Basic].[ActiveStatus] A ON AAM.ActiveStatusId = A.ID
          WHERE AAM.[Id] = @Id AND AAM.ActiveStatusId <> 3";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstAsync<GetApiAuthenticationMethodsQueryResult>(query, new { Id = request.Id });
                result.NullCheck();
                return result ?? throw SimaResultException.NotFound;
            }
        }
    }
}
