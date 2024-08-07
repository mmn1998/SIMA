using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Application.Query.Contract.Features.Auths.Domains;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Domains;
public class DomainQueryRepository : IDomainQueryRepository
{
    private readonly SIMADBContext _readContext;
    private readonly string _connectionString;

    public DomainQueryRepository(SIMADBContext readContext, IConfiguration configuration)
    {
        _readContext = readContext;
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<GetDomainQueryResult> FindById(long id)
    {
        var query = @"select d.Id , d.Name , d.Code , a.ID ActiveStatusId, a.Name ActiveStatus  
				   from Authentication.Domain d  
				   join Basic.ActiveStatus a
                    on d.ActiveStatusId = a.ID
                      WHERE d.Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<GetDomainQueryResult>(query, new { Id = id });
            return result ?? throw SimaResultException.NotFound;
        }
    }
    //ForDropDown
    public async Task<List<GetDomainQueryResult>> GetAll()
    {

        string query = $@" 
             SELECT DISTINCT  
			 D.[ID] as Id
			,D.[Name]
			,D.[Code]
			,a.ID ActiveStatusId
			,a.Name ActiveStatus
			,d.[CreatedAt]
			FROM [Authentication].Domain D
			join Basic.ActiveStatus a on D.ActiveStatusId = a.ID
			WHERE  D.[ActiveStatusID] <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<GetDomainQueryResult>(query);
            return result.ToList() ?? throw SimaResultException.NotFound;
        }

    }
}
