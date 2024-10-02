using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Resources;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Positions;

public class PositionQueryRepository : IPositionQueryRepository
{
    private readonly string _connectionString;

    public PositionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<GetPositionQueryResult> FindById(long Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
SELECT DISTINCT P.[ID] as Id
    ,P.[Name]
    ,P.[Code] 
    ,P.[ActiveStatusID]
    ,A.[Name] as ActiveStatus 
    ,D.Name as DepartmentName
    ,P.[DepartmentId]
    ,C.Name as CompanyName
    ,C.Id as CompanyId
	,B.Id BranchId
    ,B.Name BranchName
	,p.PersonLimitation
    ,PT.Name PositionTypeName
    ,PT.Id PositionTypeId
    ,PL.Name PositionLevelName
    ,PL.Id PositionLevelId
FROM [Organization].Position P
LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID and D.ActiveStatusId<>3
LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID and C.ActiveStatusId<>3
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
LEFT JOIN Bank.Branch B ON P.BranchId = B.Id and B.ActiveStatusId<>3
left join Organization.PositionLevel PL on pl.Id = p.PositionLevelId and PL.ActiveStatusId<>3
left join Organization.PositionType PT on pt.Id = p.PositionTypeId and PT.ActiveStatusId<>3
WHERE P.[ActiveStatusID] <> 3 AND P.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetPositionQueryResult>(query, new { Id });
            if (result is null) throw new SimaResultException(CodeMessges._100054Code, Messages.PositionNotFoundError);
            return result;
        }
    }
    public async Task<Result<IEnumerable<GetPositionQueryResult>>> GetAll(GetAllPositionsQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
SELECT DISTINCT P.[ID] as Id
    ,P.[Name]
    ,P.[Code]
    ,P.[CreatedAt]
    ,P.[ActiveStatusID]
    ,P.[DepartmentId]
    ,D.Name as DepartmentName
    ,A.[Name] as ActiveStatus 
    ,C.Name as CompanyName
    ,C.Id as CompanyId
	,B.Id BranchId
    ,B.Name BranchName
	,p.PersonLimitation
    ,PT.Name PositionTypeName
    ,PT.Id PositionTypeId
    ,PL.Name PositionLevelName
    ,PL.Id PositionLevelId
FROM [Organization].Position P
LEFT JOIN [Organization].Department D on D.ID = P.DepartmentID and D.ActiveStatusId<>3
LEFT JOIN [Organization].[Company] C on C.ID = D.CompanyID and C.ActiveStatusId<>3
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
LEFT JOIN Bank.Branch B ON P.BranchId = B.Id and B.ActiveStatusId<>3
left join Organization.PositionLevel PL on pl.Id = p.PositionLevelId and PL.ActiveStatusId<>3
left join Organization.PositionType PT on pt.Id = p.PositionTypeId and PT.ActiveStatusId<>3
WHERE P.[ActiveStatusID] <> 3
AND (@PositionLevelId is null OR PL.Id = @PositionLevelId) AND (@BranchId is null OR B.Id = @BranchId) AND (@DepartmentId is null OR D.Id = @DepartmentId)
";
            string queryCount = $@" WITH Query as({mainQuery})
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
            string query = $@"WITH Query as({mainQuery})
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("PositionLevelId", request.PositionLevelId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetPositionQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<Result<IEnumerable<GetPositionQueryResult>>> GetByDepartemantId(long departmentId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
             SELECT DISTINCT 
                   P.[ID] as Id
                  ,P.[Name]
              FROM [Organization].Position P
              WHERE P.ActiveStatusId <> 3 and P.DepartmentId = @DepartmentId

";
            var result = await connection.QueryAsync<GetPositionQueryResult>(query, new { DepartmentId = departmentId });
            return Result.Ok(result);
        }
    }
}
