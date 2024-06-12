using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.CurrencyTypes
{
    public class CurrencyTypeReadRepository : ICurrencyTypeReadRepository
    {
        private readonly string _connectionString;

        public CurrencyTypeReadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public async Task<Result<IEnumerable<GetCurrencyTypeQueryResult>>> GetAll(GetAllCurrencyTypesQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string queryCount = @" WITH Query as(
						    SELECT DISTINCT CT.[ID]
    ,CT.[Name]
    ,CT.[Code]
    ,CT.[ISBASECURRENCY]
    	,A.Name ActiveStatus
    	,CT.ActiveStatusId
    ,ct.[CreatedAt]
FROM [Bank].[CurrencyType] CT
INNER JOIN [Basic].[ActiveStatus] A on A.ID = CT.ActiveStatusID
WHERE  CT.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;
                                      ";

                string query = $@"
                     WITH Query as(
							  SELECT DISTINCT CT.[ID]
    ,CT.[Name]
    ,CT.[Code]
    ,CT.[ISBASECURRENCY]
    	,A.Name ActiveStatus
    	,CT.ActiveStatusId
    ,ct.[CreatedAt]
FROM [Bank].[CurrencyType] CT
INNER JOIN [Basic].[ActiveStatus] A on A.ID = CT.ActiveStatusID
WHERE  CT.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;
                            ";

                var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetCurrencyTypeQueryResult>();
                    return Result.Ok(response, request, count);
                }

            }
        }

        public async Task<GetCurrencyTypeQueryResult> GetById(long id)
        {
            var result = new GetCurrencyTypeQueryResult();
            string query = @"
                    SELECT DISTINCT CT.[ID]
                          ,CT.[Name]
                          ,CT.[Code]
                          ,[ISBASECURRENCY]
                    	  ,A.Name ActiveStatus
                    	  ,CT.ActiveStatusId
                      FROM [Bank].[CurrencyType] CT
                      INNER JOIN [Basic].[ActiveStatus] A on A.ID = CT.ActiveStatusID
                      WHERE CT.ID = @Id
                    ";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryFirstAsync<GetCurrencyTypeQueryResult>(query, new { Id = id });
                if (result is null) throw SimaResultException.NullException;
            }
            return result;
        }
    }
}
