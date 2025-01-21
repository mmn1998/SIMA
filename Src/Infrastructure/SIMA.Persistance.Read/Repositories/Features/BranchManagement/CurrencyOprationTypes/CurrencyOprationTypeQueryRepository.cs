using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyOprationTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.CurrencyOprationTypes
{
    public class CurrencyOprationTypeQueryRepository : ICurrencyOprationTypeQueryRepository
    {
        private readonly string _connectionString;
        public CurrencyOprationTypeQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }
        public async Task<Result<IEnumerable<GetCurrencyOprationTypeQueryResult>>> GetAll(GetAllCurrencyOprationTypesQuery request)
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
                        FROM [Bank].[CurrencyOprationType] CO
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
                            FROM [Bank].[CurrencyOprationType] CO
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
                    var response = await multi.ReadAsync<GetCurrencyOprationTypeQueryResult>();
                    return Result.Ok(response, request, count);
                }


            }
        }
        public async Task<GetCurrencyOprationTypeQueryResult> GetById(long id)
        {
            var result = new GetCurrencyOprationTypeQueryResult();
            string query = @"
                    SELECT DISTINCT CO.[ID]
                        ,CO.[Name]
                        ,CO.[Code]
                        ,CO.[ActiveStatusID]
                        ,A.Name ActiveStatus
                        ,CO.[CreatedAt]
                    FROM [Bank].[CurrencyOprationType] CO
                    INNER JOIN [Basic].[ActiveStatus] A on A.ID = CO.ActiveStatusID
                    WHERE  CO.ActiveStatusId != 3 And CO.Id = @Id
                    ";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryFirstAsync<GetCurrencyOprationTypeQueryResult>(query, new { Id = id });
                if (result is null) throw SimaResultException.NullException;
            }
            return result;
        }
    }
}
