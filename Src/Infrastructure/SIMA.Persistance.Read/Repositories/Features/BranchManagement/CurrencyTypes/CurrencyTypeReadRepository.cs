using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Persistance.Read;
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

        public async Task<List<GetCurrencyTypeQueryResult>> GetAll(BaseRequest request)
        {
            try
            {
                var result = new List<GetCurrencyTypeQueryResult>();
                string query = string.Empty;
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    if (!string.IsNullOrEmpty(request.SearchValue))
                    {
                        query = @"
                            SELECT DISTINCT CT.[ID]
                          ,CT.[Name]
                          ,CT.[Code]
                          ,[ISBASECURRENCY]
                    	  ,A.Name ActiveStatus
                    	  ,CT.ActiveStatusId
,ct.[CreatedAt]
                      FROM [Bank].[CurrencyType] CT
                      INNER JOIN [Basic].[ActiveStatus] A on A.ID = CT.ActiveStatusID
                              WHERE (CT.Name like %@SearchValue OR CT.[Code] like @SerachValue)
Order By ct.[CreatedAt] desc  
                            ";
                    }
                    else
                    {
                        query = @"
                            SELECT DISTINCT CT.[ID]
                          ,CT.[Name]
                          ,CT.[Code]
                          ,[ISBASECURRENCY]
                    	  ,A.Name ActiveStatus
                    	  ,CT.ActiveStatusId
,ct.[CreatedAt]
                      FROM [Bank].[CurrencyType] CT
                      INNER JOIN [Basic].[ActiveStatus] A on A.ID = CT.ActiveStatusID
Order By ct.[CreatedAt] desc  
                            ";
                    }
                    result = (await connection.QueryAsync<GetCurrencyTypeQueryResult>(query))
                        .Skip((request.Skip - 1) * request.Take)
                        .Take(request.Take)
                        .ToList();
                }
                return result;
            }
            catch (Exception e)
            {

                throw;
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
