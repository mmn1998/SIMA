using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
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
                string queryCount = @"
                       SELECT Count(*) Result
                       FROM [Bank].[CurrencyType] CT
                       INNER JOIN [Basic].[ActiveStatus] A on A.ID = CT.ActiveStatusID
                       WHERE  CT.ActiveStatusId != 3
                       and (@SearchValue is null OR CT.[Name] like @SearchValue or CT.[Code] like @SearchValue)
                                      ";
                await connection.OpenAsync();

                string query = $@"
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
                      and (@SearchValue is null OR CT.[Name] like @SearchValue or CT.[Code] like @SearchValue)
                    order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                      OFFSET @Skip rows FETCH NEXT @PageSize rows only;
                            ";


                using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
                {
                    SearchValue = "%" + request.Filter + "%",
                    request.Skip,
                    request.PageSize
                }))
                {
                    var response = await multi.ReadAsync<GetCurrencyTypeQueryResult>();
                    var count = await multi.ReadSingleAsync<int>();
                    return Result.Ok(response, count, request.PageSize, request.Page);
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
