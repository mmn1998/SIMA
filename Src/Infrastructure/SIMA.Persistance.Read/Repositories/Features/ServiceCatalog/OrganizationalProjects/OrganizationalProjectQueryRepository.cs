using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.OrganizationalProjects;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.OrganizationalProjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.OrganizationalProjects
{
    public class OrganizationalProjectQueryRepository : IOrganizationalProjectQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public OrganizationalProjectQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"
                    SELECT OP.[Id]
                                  ,OP.[Name]
                                  ,OP.[Code]
	                              ,A.[Name] ActiveStatus
                                  ,OP.CreatedAt
                              FROM [ServiceCatalog].[OrganizationalProject] OP
                              INNER JOIN [Basic].[ActiveStatus] A ON OP.ActiveStatusId = A.ID
                    WHERE OP.ActiveStatusId <> 3";
        }

        public async Task<GetOrganizationalProjectsQueryResult> GetById(GetOrganizationalProjectQuery request)
        {
            var query = @"
                          SELECT OP.[Id]
                                  ,OP.[Name]
                                  ,OP.[Code]
	                              ,A.[Name] ActiveStatus
                                  ,OP.CreatedAt
                              FROM [ServiceCatalog].[OrganizationalProject] OP
                              INNER JOIN [Basic].[ActiveStatus] A ON OP.ActiveStatusId = A.ID
                          WHERE OP.[Id] = @Id AND OP.ActiveStatusId <> 3";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstAsync<GetOrganizationalProjectsQueryResult>(query, new { Id= request.Id });
                result.NullCheck();
                return result ?? throw SimaResultException.NotFound;
            }
        }

        public async Task<Result<IEnumerable<GetOrganizationalProjectsQueryResult>>> GetAll(GetAllOrganizationalProjectsQuery request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


                string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
                var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetOrganizationalProjectsQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }
    }
}
