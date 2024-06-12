using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.ApprovalOptions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.ApprovalOptions
{
    public class ApprovalOptionQueryRepository : IApprovalOptionQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public ApprovalOptionQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"SELECT DISTINCT T.[Id]
                      ,T.[Name]
                      ,T.[Code]
                      ,T.[ActiveStatusId]
                      ,T.[CreatedAt]
	                  , S.[Name] as ActiveStatus
                  FROM [Project].[ApprovalOption] T
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = T.ActiveStatusId
                  WHERE T.ActiveStatusId != 3";
        }

        public async Task<Result<IEnumerable<GetApprovalOptionQueryResult>>> GetAll(GetAllApprovalOptionsQuery request)
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
                    var response = await multi.ReadAsync<GetApprovalOptionQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<GetApprovalOptionQueryResult> FindById(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
              SELECT DISTINCT T.[Id]
                      ,T.[Name]
                      ,T.[Code]
                      ,T.[ActiveStatusId]
	                  , S.[Name] as ActiveStatus
                  FROM [Project].[ApprovalOption] T
                  INNER JOIN [Basic].[ActiveStatus] S on S.ID = T.ActiveStatusId
                  WHERE T.Id = @Id and T.ActiveStatusId != 3";
                var result = await connection.QueryFirstOrDefaultAsync<GetApprovalOptionQueryResult>(query, new { Id = id });
                result.NullCheck();
                return result ?? throw SimaResultException.NotFound;
            }
        }
    }
}
