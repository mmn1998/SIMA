using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialActionTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.FinancialActionTypes
{
    public class FinancialActionTypeQueryRepository : IFinancialActionTypeQueryRepository
    {
        private readonly string _connectionString;
        public FinancialActionTypeQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }
        public async Task<Result<IEnumerable<GetFinancialActionTypeQueryResult>>> GetAll(GetAllFinancialActionTypesQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string queryCount = @" WITH Query as(
						  SELECT DISTINCT CO.[ID]
                            ,CO.[Name]
                            ,CO.[Code]
                            ,CO.[ActiveStatusID]
                            ,A.Name ActiveStatus
                            ,CO.[CreatedAt]
                        FROM [Bank].[FinancialActionType] CO
                        INNER JOIN [Basic].[ActiveStatus] A on A.ID = CO.ActiveStatusID
                        WHERE  CO.ActiveStatusId != 3
							                    )
								                    SELECT Count(*) FROM Query
								                     /**where**/
								 
								                     ; ";


                string query = $@" WITH Query as(
							SELECT DISTINCT CO.[ID]
                                ,CO.[Name]
                                ,CO.[Code]
                                ,CO.[ActiveStatusID]
                                ,A.Name ActiveStatus
                                ,CO.[CreatedAt]
                            FROM [Bank].[FinancialActionType] CO
                            INNER JOIN [Basic].[ActiveStatus] A on A.ID = CO.ActiveStatusID
                            WHERE  CO.ActiveStatusId != 3
							                        )
								                        SELECT * FROM Query
								                         /**where**/
								                         /**orderby**/
                                                            OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
                var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetFinancialActionTypeQueryResult>();
                    return Result.Ok(response, request, count);
                }


            }
        }
        public async Task<GetFinancialActionTypeQueryResult> GetById(long id)
        {
            var result = new GetFinancialActionTypeQueryResult();
            string query = @"
                    SELECT DISTINCT CO.[ID]
                        ,CO.[Name]
                        ,CO.[Code]
                        ,CO.[ActiveStatusID]
                        ,A.Name ActiveStatus
                        ,CO.[CreatedAt]
                    FROM [Bank].[FinancialActionType] CO
                    INNER JOIN [Basic].[ActiveStatus] A on A.ID = CO.ActiveStatusID
                    WHERE  CO.ActiveStatusId != 3 And CO.Id = @Id
                    ";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryFirstAsync<GetFinancialActionTypeQueryResult>(query, new { Id = id });
                if (result is null) throw SimaResultException.NullException;
            }
            return result;
        }
    }
}
