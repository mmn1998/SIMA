using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Request;
using SIMA.Persistance.Read;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BranchManagement.BrokerTypes
{
    public class BrokerTypeReadRepository : IBrokerTypeReadRepository
    {
        private readonly string _connectionString;
        public BrokerTypeReadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }
        public async Task<List<GetBrokerTypeQueryResult>> GetAll(BaseRequest request)
        {
            var result = new List<GetBrokerTypeQueryResult>();
            string query = string.Empty;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = @"
                            SELECT DISTINCT BT.[ID]
                                  ,BT.[Name]
                                  ,BT.[Code]
                            	  ,A.Name ActiveStatus
                            	  ,BT.ActiveStatusId 
,bt.[CreatedAt]
                              FROM [Bank].[BrokerType] BT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
                              WHERE (PT.Name like @SearchValue OR BT.[Code] like @SerachValue)
Order By bt.[CreatedAt] desc  
                            ";
                }
                else
                {
                    query = @"
                            SELECT DISTINCT BT.[ID]
                                  ,BT.[Name]
                                  ,BT.[Code]
                            	  ,A.Name ActiveStatus
                            	  ,BT.ActiveStatusId 
,bt.[CreatedAt]
                              FROM [Bank].[BrokerType] BT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID
Order By bt.[CreatedAt] desc  
                            ";
                }
                result = (await connection.QueryAsync<GetBrokerTypeQueryResult>(query, new { SearchValue = "%" + request.SearchValue + "%" }))
                    .Skip((request.Skip - 1) * request.Take)
                    .Take(request.Take)
                    .ToList();
            }
            return result;
        }
        public async Task<GetBrokerTypeQueryResult> GetById(long id)
        {
            var result = new GetBrokerTypeQueryResult();
            string query = @"
                    SELECT DISTINCT BT.[ID]
                                  ,BT.[Name]
                                  ,BT.[Code]
                            	  ,A.Name ActiveStatus
                            	  ,BT.ActiveStatusId 
                              FROM [Bank].[BrokerType] BT
                              INNER JOIN [Basic].[ActiveStatus] A on A.ID = BT.ActiveStatusID WHERE BT.Id = @Id
                    ";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                result = await connection.QueryFirstAsync<GetBrokerTypeQueryResult>(query, new { Id = id });
                if (result is null) throw SimaResultException.NullException;
            }
            return result;
        }
    }
}
