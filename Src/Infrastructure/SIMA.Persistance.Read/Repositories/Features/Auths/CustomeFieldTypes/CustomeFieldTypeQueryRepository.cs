using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.CustomeFieldTypes;
using SIMA.Application.Query.Contract.Features.Auths.CustomerTypes;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Read.Repositories.Features.Auths.CustomerTypes;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.CustomeFieldTypes
{
    public class CustomeFieldTypeQueryRepository : ICustomeFieldTypeQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public CustomeFieldTypeQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"Select
                                CFT.Id,
                                CFT.Name,
                                CFT.Code,
                                CFT.IsList,
                                CFT.IsMultiSelect,
                                CFT.ActiveStatusId,
                                CFT.CreatedAt,
                                A.Name ActiveStatus
                                from Basic.CustomeFieldType CFT
                                join Basic.ActiveStatus A on A.ID = CFT.ActiveStatusId
                                where CFT.ActiveStatusId <> 3";
        }

        public async Task<Result<IEnumerable<GetCustomeFieldTypeQueryResult>>> GetAll(GetAllCustomeFieldTypesQuery request)
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
                var dynaimcParameters = (queryCount + query).GenerateQuery(request);
                using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
                {
                    var count = await multi.ReadFirstAsync<int>();
                    var response = await multi.ReadAsync<GetCustomeFieldTypeQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }
    }
}
