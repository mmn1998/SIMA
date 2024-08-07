using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.DataTypes;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.DataTypes;

public class DataTypeQueryRepository : IDataTypeQueryRepository
{
    private readonly string _connectionString;
    public DataTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<IEnumerable<GetDataTypeQueryResult>> GetAll(GetDataTypeQuery request)
    {
        string query = @"SELECT 
		DT.[Id]
      ,DT.[Name]
      ,DT.[Code]
      ,DT.[IsList]
      ,DT.[IsMultiSelect]
      ,DT.[ActiveStatusId]
	  ,A.Name ActiveStatus
  FROM [Basic].[DataType] DT
  inner join Basic.ActiveStatus A on A.ID = DT.ActiveStatusId";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<GetDataTypeQueryResult>(query);
        }
    }
}