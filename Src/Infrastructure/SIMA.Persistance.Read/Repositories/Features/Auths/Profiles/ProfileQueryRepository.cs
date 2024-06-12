using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Resources;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Profiles;

public class ProfileQueryRepository : IProfileQueryRepository
{
    private readonly string _connectionString;

    public ProfileQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetProfileQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT P.[ID] as Id
                  ,P.FirstName
	              ,P.LastName
	              ,P.FatherName
	              ,P.NationalID as NationalCode
                  ,P.[ActiveStatusID]
                  ,A.[Name] as ActiveStatus 
              FROM [Authentication].[Profile] P
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID    
              WHERE P.[ActiveStatusID] <> 3 AND P.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetProfileQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException("10055", Messages.ProfileNotFoundError);
            return result;
        }
    }

    public async Task<List<GetPhoneBookQueryResult>> FindWithPhoneBook(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = @$"
                        select P.ID ProfileId
                        ,ph.ID Id
                        ,ph.PhoneNumber
                        ,pht.Name PhoneType
                        ,p.[ActiveStatusID]
                        ,A.[Name] as ActiveStatus 
,p.[CreatedAt]
                        from Authentication.Profile p 
                        join Authentication.PhoneBook ph on p.ID = ph.ProfileID
                        join Basic.PhonType pht on ph.PhoneTypeID = pht.ID
                        join [Basic].[ActiveStatus] A on A.Id = p.ActiveStatusID
                        WHERE  p.ActiveStatusId != 3 and p.ID = @Id
Order By p.[CreatedAt] desc";
            var result = await connection.QueryAsync<GetPhoneBookQueryResult>(query, new { Id = id });

            return result.ToList();
        }
        //return await _context.Profiles.SelectMany(x => x.PhoneBooks).Select(x => new GetPhoneBookQueryResult
        //{ }).ToListAsync();
    }
    public async Task<Result<IEnumerable<GetAddressBookQueryResult>>> FindWithAddressBook(long id, BaseRequest? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var queryCount = @" WITH Query as(
						  select P.ID ProfileId
,a.ID Id,a.Address
,at.Name AddressType
,s.Name ActiveStatus
,p.ActiveStatusId
,p.[CreatedAt]
from Authentication.Profile p 
join Authentication.AddressBook a on p.ID = a.ProfileID
join Basic.AddressType at on a.AddressTypeID = at.ID
join [Basic].[ActiveStatus] S on S.Id = P.ActiveStatusID
WHERE p.ActiveStatusId != 3 and p.ID = @Id
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";

            string query = @$" WITH Query as(
							select P.ID ProfileId
,a.ID Id,a.Address
,at.Name AddressType
,s.Name ActiveStatus
,p.ActiveStatusId
,p.[CreatedAt]
from Authentication.Profile p 
join Authentication.AddressBook a on p.ID = a.ProfileID
join Basic.AddressType at on a.AddressTypeID = at.ID
join [Basic].[ActiveStatus] S on S.Id = P.ActiveStatusID
WHERE p.ActiveStatusId != 3 and p.ID = @Id
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("Id", id);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetAddressBookQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<Result<IEnumerable<GetProfileQueryResult>>> GetAll(GetAllProfileQuery request = null)
    {


        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                var queryCount = @" WITH Query as(
						  SELECT DISTINCT 
                                     P.[ID] as Id
                                    ,P.FirstName
	                                ,P.LastName
	                                ,P.FatherName
	                                ,P.NationalID as NationalCode
	                                ,P.ActiveStatusId 
	                                ,A.Name as ActiveStatus
                                    ,p.[CreatedAt]
                                FROM [Authentication].[Profile] P
                                 join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                                WHERE  P.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
                string query = $@" WITH Query as(
							SELECT DISTINCT 
                                     P.[ID] as Id
                                    ,P.FirstName
	                                ,P.LastName
	                                ,P.FatherName
	                                ,P.NationalID as NationalCode
	                                ,P.ActiveStatusId 
	                                ,A.Name as ActiveStatus
                                    ,p.[CreatedAt]
                                FROM [Authentication].[Profile] P
                                 join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                                WHERE  P.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetProfileQueryResult>();
                return Result.Ok(response, request, count);
            }
            
        }
    }

    public async Task<Result<IEnumerable<GetPhoneBookQueryResult>>> GetAllPhoneBooks(int profileId, BaseRequest? request = null)
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
                var queryCount = @" WITH Query as(
						  SELECT DISTINCT PB.ID as Id,
PB.PhoneNumber,
PT.Name as PhoneTypeName
,P.ActiveStatusId 
,A.Name as ActiveStatus
,p.[CreatedAt]
FROM [Authentication].[Profile] P
INNER JOIN [Authentication].[PhoneBook] PB on PB.ProfileID = P.ID AND PB.[ActiveStatusID] <> 3
left JOIN [Basic].[PhonType] PT on PT.ID = PB.PhoneTypeID
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
WHERE P.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
                string query = $@"WITH Query as(
							SELECT DISTINCT PB.ID as Id,
PB.PhoneNumber,
PT.Name as PhoneTypeName
,P.ActiveStatusId 
,A.Name as ActiveStatus
,p.[CreatedAt]
FROM [Authentication].[Profile] P
INNER JOIN [Authentication].[PhoneBook] PB on PB.ProfileID = P.ID AND PB.[ActiveStatusID] <> 3
left JOIN [Basic].[PhonType] PT on PT.ID = PB.PhoneTypeID
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
WHERE  P.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            dynaimcParameters.Item2.Add("Id", profileId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetPhoneBookQueryResult>();
                return Result.Ok(response, request, count);
            }
            
        }
    }

    public async Task<List<SelectModel>> GetMangersByCompanyId(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = @$"SELECT CONCAT(p.FirstName, ' ', p.LastName) Name, p.ID Id
                                  FROM     
                                      Organization.Staff s INNER JOIN
                                      Authentication.Profile p ON s.ManagerID = p.ID INNER JOIN
                                      Organization.Position po ON s.PositionID = po.ID INNER JOIN
                                      Organization.Department d ON po.DepartmentID = d.ID AND po.DepartmentID = d.ID INNER JOIN
                                      Organization.Company c ON d.CompanyID = c.ID AND d.CompanyID = c.ID WHERE  c.ID = @Id
                                      Order By p.[CreatedAt] desc  ";
            var result = await connection.QueryAsync<SelectModel>(query, new { Id = id });
            return result.ToList();
        }
    }

    public async Task<List<GetShortProfileQueryResult>> GetShort()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT P.[ID] as Id
                  ,P.FirstName
	              ,P.LastName
	              ,P.NationalID as NationalId
                  ,P.[ActiveStatusID]
                  ,A.[Name] as ActiveStatus 
                  ,p.[CreatedAt]
              FROM [Authentication].[Profile] P
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID    
              WHERE P.[ActiveStatusID] <> 3
              Order By p.[CreatedAt] desc  ";
            var result = await connection.QueryAsync<GetShortProfileQueryResult>(query);
            if (result is null) throw new SimaResultException("10055", Messages.ProfileNotFoundError);
            return result.ToList();
        }
    }


}
