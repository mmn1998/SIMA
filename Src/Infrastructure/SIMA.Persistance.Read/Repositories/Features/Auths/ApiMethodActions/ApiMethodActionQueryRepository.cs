using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.ApiMethodActions;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ApiMethodActions;

public class ApiMethodActionQueryRepository : IApiMethodActionQueryRepository
{
    private readonly string _connectionString;
    public ApiMethodActionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<IEnumerable<GetApiMethodActionQueryResult>> GetAll(GetAllApiMethodActionsQuery request)
    {
        string query = @"SELECT 
		DT.[Id]
      ,DT.[Name]
      ,DT.[Code]
  FROM [Basic].[ApiMethodAction] DT";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<GetApiMethodActionQueryResult>(query);
        }
    }
}