using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialSuppliers;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.FinancialSuppliers
{
    public class FinancialSupplierQueryRepository : IFinancialSupplierQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public FinancialSupplierQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"
SELECT CO.[ID]
    ,CO.[Name]
    ,CO.[Code]
    ,CO.[ActiveStatusID]
    ,A.Name ActiveStatus
    ,CO.[CreatedAt]
	,Co.CustomerId
	,c.Name CustomerFullName
	,c.CustomerNumber
FROM [Bank].[FinancialSupplier] CO
INNER JOIN [Basic].[ActiveStatus] A on A.ID = CO.ActiveStatusID
Inner join Bank.Customer C on c.Id = CO.CustomerId and c.ActiveStatusId<>3
WHERE  CO.ActiveStatusId != 3
";
        }
        public async Task<Result<IEnumerable<GetFinancialSupplierQueryResult>>> GetAll(GetAllFinancialSuppliersQuery request)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string queryCount = $@" WITH Query as({_mainQuery})
								                    SELECT Count(*) FROM Query
								                     /**where**/
								 
								                     ; ";

            string query = $@" WITH Query as({_mainQuery})
								                        SELECT * FROM Query
								                         /**where**/
								                         /**orderby**/
                                                            OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
            var count = await multi.ReadFirstAsync<int>();
            var response = await multi.ReadAsync<GetFinancialSupplierQueryResult>();
            return Result.Ok(response, request, count);
        }
        public async Task<GetFinancialSupplierQueryResult> GetById(long id)
        {
            var result = new GetFinancialSupplierQueryResult();
            string query = $@"
                      {_mainQuery} And CO.Id = @Id
                    ";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            result = await connection.QueryFirstAsync<GetFinancialSupplierQueryResult>(query, new { Id = id });
            if (result is null) throw SimaResultException.NullException;
            return result;
        }
    }
}
