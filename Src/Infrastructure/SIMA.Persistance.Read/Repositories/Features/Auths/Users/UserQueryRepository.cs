using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Groups;
using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Users;

public class UserQueryRepository : IUserQueryRepository
{
    private readonly string _connectionString;
    private readonly SIMADBContext _readContext;
    public UserQueryRepository(IConfiguration configuration, SIMADBContext context)
    {
        _connectionString = configuration.GetConnectionString();
        _readContext = context;
    }
    public async Task<GetUserQueryResult> FindById(long id)
    {
        var query = @"SELECT U.[ID] Id
                                ,U.[User_SecretKey]
                                ,U.[ProfileID]
                                ,U.[CompanyID]
                                ,U.[ActiveStatusID]
                                ,U.[Username]
                                ,U.[Password]
                                ,U.[SecretKey]
                                ,U.[IsLoggedIn]
                                ,U.[ActiveFrom]
                                ,U.[ActiveTo]
                                ,U.[CreatedAt]
                                ,U.[CreatedBy]
                                ,U.[ModifiedAt]
                                ,U.[ModifiedBy]
                                ,A.[Name] as ActiveStatus
                            FROM [Authentication].[Users] U
                            join [Basic].[ActiveStatus] A on A.Id = U.ActiveStatusID
                            where U.Id = @Id and U.ActiveStatusId != 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<GetUserQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }
    public async Task<bool> IsUsernameUnique(string username, long userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {

            await connection.OpenAsync();

            int result = 0;

            if (userId > 0)
                result = await connection.QueryFirstOrDefaultAsync<int>("SELECT top 1 1 FROM Authentication.Users WHERE Username = @Username and Id <> @UserId", new { Username = username, UserId = userId });
            else
                result = await connection.QueryFirstOrDefaultAsync<int>("SELECT top 1 1 FROM Authentication.Users WHERE Username = @Username", new { Username = username });
            return result == 0;
        }
    }
    public async Task<bool> IsUsrConfigSatisfied(long configurationId, long userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<int>("Select top 1 1 From [Authentication].[UserConfig] where ConfigurationID = @ConfigurationId and UserID = @UserId", new { UserId = userId, ConfigurationId = configurationId });
            return result != 0;
        }
    }
    public async Task<LoginUserQueryResult> GetPermissions(long userId)
    {
        var response = new LoginUserQueryResult();
        var query = @"
                                         --user

 select distinct
       u.Id UserId,
       u.CompanyId,
       u.Username,
       u.IsFirstLogin,
       u.IsLocked,
       u.AccessFailedOverallCount,
       u.AccessFailedDate,
       u.ConfirmCode,
       u.IsSendOTP
    from Authentication.Users u
   where U.Id=@UserId  and U.ActiveStatusId<>3

    --Roles
    select distinct r.id RoleId
    from Authentication.Users u
    inner join Authentication.UserRole ur on u.Id = ur.UserId
    inner join Authentication.Role r on r.Id = ur.RoleId
    where U.Id=@UserId  and U.ActiveStatusId<>3

    --groups
    select distinct g.id GroupId
    from Authentication.Users u
    inner  join Authentication.UserGroup ug on u.Id = ug.UserId
    inner join Authentication.Groups g on g.Id = ug.GroupId
    where U.Id=@UserId  and U.ActiveStatusId<>3

    --permission
    select distinct p1.Code Code
    from Authentication.Users u
    inner  join Authentication.UserPermission up on u.Id = up.UserId
    inner join [Authentication].[Permission] P1 on P1.Id=up.PermissionId
    where U.Id=@UserId  and U.ActiveStatusId<>3 and up.ActiveStatusId != 3

    union 

    select distinct p2.Code Code
    from Authentication.Users u
    inner join Authentication.UserRole ur on u.Id = ur.UserId
    inner join Authentication.Role r on r.Id = ur.RoleId
    inner join Authentication.RolePermission rp on r.Id = rp.RoleId and rp.ActiveStatusId<>3
    inner join [Authentication].[Permission] P2 on P2.Id=rp.PermissionId
    where U.Id=@UserId and U.ActiveStatusId<>3

    union 

    select distinct P3.Code Code
    from Authentication.Users u
    inner  join Authentication.UserGroup ug on u.Id = ug.UserId
    inner join Authentication.Groups g on g.Id = ug.GroupId
    inner join Authentication.GroupPermission gp on g.Id = gp.GroupId and gp.ActiveStatusId<>3
    inner join [Authentication].[Permission] P3 on P3.Id=gp.PermissionId
    where U.Id=@UserId  and u.ActiveStatusId<>3

    --Menus
 SELECT distinct f.Code Code    from 
 Authentication.Domain d 
 join Authentication.DomainForms df on d.Id=df.DomainId
 join Authentication.Form f on df.FormId=f.Id
 join Authentication.FormUser fu on f.Id=fu.FormId
 join Authentication.Users u on fu.UserId=u.Id
 where fu.UserId=@UserId  and fu.ActiveStatusId<>3 and u.ActiveStatusId <>3 and f.ActiveStatusId <>3
 union
 select distinct f.Code Code from 
Authentication.Domain d
join Authentication.DomainForms df on d.Id=df.DomainId and df.ActiveStatusId <>3
join Authentication.Form f on df.FormId=f.Id and f.ActiveStatusId <>3
join Authentication.FormGroup fg on f.Id=fg.FormId and fg.ActiveStatusId <>3
join Authentication.Groups g on fg.GroupId=g.Id and g.ActiveStatusId <>3
join Authentication.UserGroup ug on g.Id=ug.GroupId  and ug.ActiveStatusId <>3
join Authentication.Users u on ug.UserId=u.Id and u.ActiveStatusId <>3
where u.Id=@UserId  and u.ActiveStatusId<>3 and fg.ActiveStatusId <>3 and f.ActiveStatusId <>3
 union
 select distinct f.Code Code from 
   Authentication.Domain d
   join Authentication.DomainForms df on d.Id=df.DomainId and df.ActiveStatusId <>3
   join Authentication.Form f on df.FormId=f.Id and f.ActiveStatusId <>3
   join Authentication.FormRole fr on f.Id=fr.FormId and fr.ActiveStatusId <>3
   join Authentication.Role r on fr.RoleId=r.Id and R.ActiveStatusId <>3
   join Authentication.UserRole ur on r.Id=ur.RoleId and UR.ActiveStatusId <> 3
   join Authentication.Users u on ur.UserId=u.Id and u.ActiveStatusId <>3
   where u.Id=@UserId  and u.ActiveStatusId<>3 and fr.ActiveStatusId <>3 and f.ActiveStatusId <>3 ";
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var multi = await connection.QueryMultipleAsync(query, new { UserId = userId }))
            {
                response.UserInfoLogin = multi.ReadAsync<UserInfoLogin>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
                response.RoleIds = await multi.ReadAsync<long>();
                response.GroupIds = await multi.ReadAsync<long>();
                response.Permissions = await multi.ReadAsync<int>();
                response.Menue = await multi.ReadAsync<long>();
            }
        }

        if (response.UserInfoLogin.IsFirstLogin != "0")
        {
            response.Permissions = response.Permissions.Where(x => x == 1001); //Access for ChangePassword
            response.RoleIds = null;
            response.GroupIds = null;
            response.Menue = null;
            return response;
        }

        if (response.UserInfoLogin.ConfirmCode is not null)
        {
            response.Permissions = response.Permissions.Where(x => x == 1002); //Access For OTP
            response.RoleIds = null;
            response.GroupIds = null;
            response.Menue = null;
            return response;
        }

        return response;
    }
    public async Task<GetInfoByUserIdQueryResult> GetInfoByUserId(long userId)
    {

        var response = new GetInfoByUserIdQueryResult();
        using (var connection = new SqlConnection(_connectionString))
        {

            var query = $@"SELECT distinct
                                  u.ID  UserId,
                                  U.[Username] Username,
                                  profile.FirstName, 
                                  profile.LastName,
                                  profile.FatherName
                                  ,gender.Name Gender,
                                  gender.Code  GenderCode
                                  ,profile.NationalID,
                                  [Basic].[Miladi_To_Persian](profile.Brithday)Brithday
                                  ,U.CompanyId 
                                  ,C.Name Company
                                  ,C.Code CompanyCode
                                  FROM  Authentication.Users  u
                                  INNER JOIN  Authentication.Profile  profile ON u.ProfileID = profile.ID
                                  INNER JOIN  Basic.Gender  gender ON gender.ID = profile.GenderID
                                  Inner Join Organization.Company C On C.Id = U.CompanyId
                                  WHERE   u.ID = @UserID and u.[ActiveStatusID] = 1
                          --Phones

                          select
                          phone.[PhoneNumber] PhoneNumber
                          ,phoneType.[Name] PhoneType
                          ,phone.PhoneTypeID PhoneTypeCode
                          FROM            Authentication.Users AS u
                          INNER JOIN      Authentication.Profile AS profile ON u.ProfileID = profile.ID
                          INNER JOIN      Authentication.PhoneBook AS phone ON phone.ProfileID = profile.ID
                          INNER JOIN      Basic.PhonType AS phoneType ON phoneType.ID = phone.PhoneTypeID
                          WHERE        (u.ID = @UserID) and u.[ActiveStatusID] =1
                          group by u.ID, phone.[PhoneNumber],phoneType.[Name],phone.PhoneTypeID

                          --Positions

                          select distinct   department.Name AS Department, department.Code AS DepartmentCode
                        , position.Name AS Position, position.Code AS PositionCode,
                                    (SELECT        A1.FirstName + ' ' + A1.LastName   FROM     Authentication.Profile A1  WHERE  (A1.ID = staff.ManagerID)) AS Manager
                        ,staff.ManagerID as ManagerId,[Basic].[Miladi_To_Persian](staff.ActiveFrom) as ActiveFrom,[Basic].[Miladi_To_Persian](staff.Activeto) as Activeto

                        FROM            Authentication.Users AS u
                        INNER JOIN      Authentication.Profile AS profile ON u.ProfileID = profile.ID
                            INNER JOIN      Organization.Company AS company ON company.ID = u.CompanyID
                        INNER JOIN      Organization.Staff AS staff ON staff.ProfileID = profile.ID
                        INNER JOIN      Organization.Position AS position ON position.ID = staff.PositionID
                        INNER JOIN      Organization.Department AS department ON department.ID = position.DepartmentID
                        WHERE        (u.ID = @UserID) and u.[ActiveStatusID]=1

                          --Address

                          select
                          addressType.Name AS AddressType,
                          addressType.Code AS AddressTypeCode
                          , address.PostalCode, address.Address
                          ,ll.Code CityCode
                          ,LL.name City
                            ,[Basic].[FN_ParentLocationName] (LL.id,2) Province
                            ,[Basic].[FN_ParentLocationCode](LL.id,2) ProvinceCode
                          FROM            Authentication.Users AS u
                          INNER JOIN      Authentication.Profile AS profile ON u.ProfileID = profile.ID
                          INNER JOIN      Authentication.AddressBook AS address ON address.ProfileID = profile.ID
                          INNER JOIN      Basic.AddressType AS addressType ON addressType.ID = address.AddressTypeID
                          inner join [Basic].[Location] LL  on address.[LocationID]=LL.[ID]
                          
                          WHERE        (u.ID = @UserID)";
            using (var multi = await connection.QueryMultipleAsync(query, new { UserID = userId }))
            {
                response.UserInfo = multi.ReadAsync<UserInfo>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.UserNotFoundError);
                response.Phones = multi.ReadAsync<PhoneResult>().GetAwaiter().GetResult().ToList();
                response.Positions = multi.ReadAsync<PositionResult>().GetAwaiter().GetResult().ToList();
                response.Addresses = multi.ReadAsync<AddressResult>().GetAwaiter().GetResult().ToList();

            }
        }
        return response;
    }
    public async Task<GetProfileByProfileIdQueryResult> GetProfileByProfileId(long profileId)
    {
        var response = new GetProfileByProfileIdQueryResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = $@"
                          ----profile
                          SELECT    profile.FirstName, profile.LastName, profile.FatherName
                          , Basic.Gender.Code AS GenderCode, profile.NationalID,
                          Basic.Miladi_To_Persian(profile.Brithday) AS BirthDate
                          FROM            Authentication.Profile AS profile
                          INNER JOIN      Basic.Gender ON profile.GenderID = Basic.Gender.ID
                          where profile.[ActiveStatusID]=1 and profile.[ID]=@ProfileID
                          
                          --Phones
                          
                          select
                          phone.[PhoneNumber] PhoneNumber
                          ,phoneType.[Name] PhoneType
                          ,phone.PhoneTypeID PhoneTypeCode
                          FROM      Authentication.Profile AS profile
                          INNER JOIN      Authentication.PhoneBook AS phone ON phone.ProfileID = profile.ID
                          INNER JOIN      Basic.PhonType AS phoneType ON phoneType.ID = phone.PhoneTypeID
                          WHERE   profile.[ActiveStatusID]=1 and profile.[ID]=@ProfileID
                          group by profile.ID,phone.[PhoneNumber],phoneType.[Name],phone.PhoneTypeID
                          
                          --Address
                          
                         select
                         addressType.Name AS AddressType,
                         addressType.Code AS AddressTypeCode
                         , address.PostalCode, address.Address
                         ,ll.code cityCode
                         ,LL.name city
                          ,[Basic].[FN_ParentLocationName] (LL.id,2) Province
                         ,[Basic].[FN_ParentLocationCode](LL.id,2) ProvinceCode
                         FROM     Authentication.Profile AS profile
                         inner JOIN      Authentication.AddressBook AS address ON address.ProfileID = profile.ID
                         inner JOIN      Basic.AddressType AS addressType ON addressType.ID = address.AddressTypeID
                         inner join [Basic].[Location] LL  on address.[LocationID]=LL.[ID]
                          
                          --user
                          select distinct    u.Username, company.Name AS Company,company.Code AS CompanyCode
                          FROM           Authentication.Profile AS profile
                          INNER JOIN      Authentication.Users AS u  ON u.ProfileID = profile.ID
                              INNER JOIN      Organization.Company AS company ON company.ID = u.CompanyID
                          where profile.[ActiveStatusID]=1 and profile.[ID]=@ProfileID
                          --Positions
                          
                         -- select distinct    
                          
                          --department.Name AS Department, department.Code AS DepartmentCode
                         -- , position.Name AS position, position.Code AS PositionCode,
                                    --  (SELECT        A1.FirstName + ' ' + A1.lastName   FROM     Authentication.Profile A1  WHERE  (A1.ID = staff.ManagerID)) AS manager
                         -- ,staff.ManagerID,[Basic].[Miladi_To_Persian](staff.ActiveFrom) as ActiveFrom,[Basic].[Miladi_To_Persian](staff.Activeto) as Activeto
                          
                         -- FROM            Authentication.Profile AS profile
                          --INNER JOIN      Authentication.Users AS u ON u.ProfileID = profile.ID
                            --  INNER JOIN      Organization.Company AS company ON company.ID = u.CompanyID
                         -- INNER JOIN      Organization.Staff AS staff ON staff.ProfileID = profile.ID
                        --  INNER JOIN      Organization.Position AS position ON position.ID = staff.PositionID
                        --  INNER JOIN      Organization.Department AS department ON department.ID = position.DepartmentID
                      --    WHERE        profile.[ActiveStatusID]=1 and profile.[ID]=@ProfileID
";

            using (var multi = await connection.QueryMultipleAsync(query, new { ProfileID = profileId }))
            {
                response.ProfileInfo = multi.ReadAsync<ProfileInfo>()?.GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
                response.Phones = multi.ReadAsync<PhoneResult>().GetAwaiter().GetResult().ToList();
                response.Addresses = multi.ReadAsync<AddressResult>().GetAwaiter().GetResult().ToList();
                response.Users = multi.ReadAsync<UserResult>().GetAwaiter().GetResult().ToList();
                //foreach (var item in response.Users)
                //{
                //    item.Positions = multi.ReadAsync<PositionResult>().GetAwaiter().GetResult().ToList();
                //}
            }
        }


        return response;
    }
    public async Task<long> GetProfileIdyUserId(long userId)
    {
        var user = await _readContext.Users.FirstOrDefaultAsync(u => u.Id == new UserId(userId));
        user.NullCheck();
        return user?.ProfileId?.Value ?? throw new SimaResultException(CodeMessges._100055Code, Messages.ProfileNotFoundError);
    }
    public async Task<GetUserQueryResult> FindByIdQuery(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT U.ID as Id,
                    C.Name as CompanyName,
                    (P.FirstName + ' ' + P.LastName) as FullName,
                    U.Username
                    ,U.ActiveStatusId
                    ,A.Name as ActiveStatus
                      FROM [Authentication].[Users] U
                      INNER JOIN [Authentication].[Profile] P on U.ProfileID = P.ID
                      INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
                      join [Basic].[ActiveStatus] A on A.Id = U.ActiveStatusID
              WHERE U.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetUserQueryResult>(query, new { Id = id });
            if (result is null) throw new SimaResultException(CodeMessges._100051Code, Messages.UserNotFoundError);
            if (result.IsDeleted) throw new SimaResultException(CodeMessges._100008Code, Messages.UserIsDeletedError);
            if (result.IsDeactivated) throw new SimaResultException(CodeMessges._100009Code, Messages.UserIsDeactiveError);
            return result;
        }

    }
    public async Task<Result<IEnumerable<GetUserQueryResult>>> GetAll(GetAllUserQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @" WITH Query as(
						  SELECT DISTINCT U.ID as Id,
      		C.Name as CompanyName,
      		(P.FirstName + ' ' + P.LastName) as FullName,
      		U.Username,
      		U.ActiveStatusId,
      		A.Name as ActiveStatus
      		,u.[CreatedAt]
FROM [Authentication].[Users] U
INNER JOIN [Authentication].[Profile] P on U.ProfileID = P.ID
INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
join [Basic].[ActiveStatus] A on A.Id = U.ActiveStatusID
WHERE U.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";
            string query = $@" WITH Query as(
							SELECT DISTINCT U.ID as Id,
      		C.Name as CompanyName,
      		(P.FirstName + ' ' + P.LastName) as FullName,
      		U.Username,
      		U.ActiveStatusId,
      		A.Name as ActiveStatus
      		,u.[CreatedAt]
FROM [Authentication].[Users] U
INNER JOIN [Authentication].[Profile] P on U.ProfileID = P.ID
INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
join [Basic].[ActiveStatus] A on A.Id = U.ActiveStatusID
WHERE U.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetUserQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }
    public async Task<GetUserRoleQueryResult> GetUserRole(long userRoleId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT DISTINCT
	   [Id]
      ,[UserId]
      ,[RoleId]
  FROM [Authentication].[UserRole]
  WHERE Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetUserRoleQueryResult>(query, new { Id = userRoleId });
            result.NullCheck();
            return result;
        }
    }
    public async Task<Result<List<GetUserPermissionQueryResult>>> GetUserPermission(long userId, long formId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                           Select 
                             U.Id UserId
                            ,F.Id FormId 
                            ,F.Name FormName 
                            ,F.Title FormTitle
                            ,P.Name PermissionName 
                            ,P.Id PermissionId
                            from Authentication.Users U
                            join Authentication.UserPermission UP on UP.UserId = U.Id and UP.ActiveStatusId <>3
                            join Authentication.FormPermission FP on FP.PermissionId= UP.PermissionId and FP.ActiveStatusId <>3
                            join Authentication.Permission P on UP.PermissionId = P.Id and P.ActiveStatusId <>3
                            join Authentication.Form F on FP.FormId = F.Id and F.ActiveStatusId <>3
                          where FP.FormId = @formId and UP.UserId = @userId
";
            var result = await connection.QueryAsync<GetUserPermissionQueryResult>(query, new { userId, formId });
            return Result.Ok(result.ToList());
        }
    }
    public async Task<GetUserLocationQueryResult> GetUserLocation(long userLocationId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT DISTINCT
	   [Id]
      ,[UserId]
      ,[LocationId]
  FROM [Authentication].[UserLocationAccess]
  WHERE ActiveStatusId = 1 AND Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetUserLocationQueryResult>(query, new { Id = userLocationId });
            result.NullCheck();
            return result;
        }
    }
    public async Task<GetUserAggregateQueryResult> GetUserAggreagate(long userId)
    {
        var response = new GetUserAggregateQueryResult();

        using (var connection = new SqlConnection(_connectionString))
        {
            string query = @"
                           

SELECT
                            U.Id,
                            U.ProfileId,
                            U.CompanyId,
                            U.Username
                            FROM Authentication.Users  U
                            WHERE U.Id = @UserID;
                            
                           --FormUsers

                             Select 
	                           F.Id  FormId
	                          ,F.Name FormName 
	                          ,F.Title FormTitle 
	                          ,D.Id DomainId
	                          ,D.Name DomainName 
	                          from Authentication.Users U
	                          join Authentication.FormUser FU on FU.UserId = U.Id
	                          join Authentication.Form F on F.Id = FU.FormId 
	                          join Authentication.DomainForms DF on F.Id  = DF.FormId
	                          join Authentication.Domain D on DF.DomainId = D.Id
	                          where U.ID = @UserId and FU.ActiveStatusId <> 3
                                                        
                            --UserPermission

                            Select 
                             U.Id UserId
                            ,FP.FormId FormId 
                            ,P.Name PermissionName 
                            ,P.Id PermissionId
                            from Authentication.Users U
                            join Authentication.UserPermission UP on UP.UserId = U.Id and UP.ActiveStatusId <>3
                            join Authentication.FormPermission FP on FP.PermissionId= UP.PermissionId and FP.ActiveStatusId <>3
                            join Authentication.Permission P on UP.PermissionId = P.Id and P.ActiveStatusId <>3
                          where  UP.UserId = @userId order by FP.FormId
                            
                            --UserRole

                            SELECT
                            R.name RoleName 
                            ,R.id RoleId
                            ,UR.id UserRoleId
                            FROM  Authentication.Users U 
                            INNER JOIN Authentication.UserRole UR ON U.ID =UR.UserID 
                            INNER JOIN Authentication.Role  R ON UR.RoleID = R.ID
                            where  U.ID = @UserID and UR.[ActiveStatusID]<>3 and R.[ActiveStatusID]<>3 
                            group by UR.id,R.name ,R.id;

                        --UserGroup

                             SELECT
				               UG.UserId 
				              ,G.id GroupId
				              ,G.name GroupName 
				             FROM  Authentication.Users U 
				             INNER JOIN Authentication.UserGroup UG ON U.ID =UG.UserID 
				             INNER JOIN Authentication.Groups G ON UG.GroupId = G.ID
				             where  U.ID = @UserID and UG.[ActiveStatusID]<>3 and G.ActiveStatusId <> 3 
				             group by UG.UserId, G.id, G.name ;
                            
                            --UserLocationAccess

                            SELECT
                            ULA.id UserLocationId
                            ,LL.name as LocationName
                            ,ll.id as LocationId
                            ,LL.LocationTypeID as LocationTypeId
                            ,LT.Name as  LocationTypeName
                            FROM  Authentication.Users AS U
                            INNER JOIN Authentication.UserLocationAccess AS ULA ON U.ID = ULA.UserID
                            INNER JOIN Basic.Location LL ON ULA.LocationID = LL.ID 
                            INNER JOIN Basic.LocationType AS LT ON LL.LocationTypeID = LT.ID
                            where  U.ID = @UserID and LL.[ActiveStatusID]<>3 and ULA.[ActiveStatusID]<>3 
                            group by   ULA.id  ,LL.name ,ll.id,LL.LocationTypeID ,LT.Name;
";
            using (var multi = await connection.QueryMultipleAsync(query, new { UserID = userId }))
            {
                response = multi.ReadAsync<GetUserAggregateQueryResult>().GetAwaiter().GetResult().FirstOrDefault() ?? throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
                var formUsers = multi.ReadAsync<GetFormUserQuery>().GetAwaiter().GetResult().ToList();
                var userPermissions = multi.ReadAsync<GetUserPermissionQueryResult>().GetAwaiter().GetResult().ToList();
                response.UserRoles = multi.ReadAsync<GetUserRoleQueryForAggregate>().GetAwaiter().GetResult().ToList();
                response.UserGroups = multi.ReadAsync<GetUserGroupsQuery>().GetAwaiter().GetResult().ToList();
                response.UserLocations = multi.ReadAsync<GetUserLocationQueryForAggregate>().GetAwaiter().GetResult().ToList();

                var formPermission = formUsers.Select(form => new GetUserFormPermissions
                {
                    Form = form,
                    Permissions = userPermissions.Where(p => p.FormId == form.FormId).ToList()
                }).ToList();

                response.FormPermissions = formPermission;
            }
        }
        return response;
    }
    public async Task<bool> IsCompanyMatchPersonCompany(long companyId, long profileId)
    {
        bool result = false;
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
select s.Id from [Organization].Staff s

inner join [Organization].Position p on p.id = s.PositionId and p.ActiveStatusId = 1
inner join [Organization].Department d on d.Id = p.DepartmentId and d.ActiveStatusId = 1
inner join [Organization].Company c on c.Id = d.CompanyId and c.ActiveStatusId = 1
where s.ProfileId = @ProfileId and c.Id = @CompanyId and s.ActiveStatusId = 1
";
            var dbResult = (await connection.QueryAsync<int>(query, new { ProfileId = profileId, CompanyId = companyId })).ToList();
            if (dbResult.Count > 0)
            {
                result = true;
            }
        }
        return result;
    }
    public async Task<string> GetUserMobileNumber(long userId)
    {
        string mobileNumber = string.Empty;
        var query = @"
                    select top 1 PB.PhoneNumber from 
                      Authentication.Users U
                      join Authentication.Profile P on p.Id = u.ProfileID and P.ActiveStatusId<>3
                      join [Authentication].[PhoneBook] PB on PB.ProfileId = P.Id and PB.ActiveStatusId<>3
                      join [Basic].[PhonType] PT on PB.PhoneTypeId = PT.Id and PT.ActiveStatusId<>3
                      where u.Id = @Id and PT.Id = 3 -- phoneTypeId = 3 is mobile 
";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var response = await connection.QueryFirstOrDefaultAsync<string>(query, new { Id = userId });
            if (string.IsNullOrEmpty(response)) throw new SimaResultException(CodeMessges._100068Code, Messages.NoMobileFoundError);
            else mobileNumber = response;
        }
        return mobileNumber;

    }
    public async Task<List<long>> GetUserPermissonByFormId(long formId, long userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var mainQuery = @"
                            select 
                            UP.PermissionId 
                            from Authentication.Form F
                            join Authentication.FormPermission FP on F.Id = FP.FormId
                            join Authentication.UserPermission UP on UP.PermissionId = FP.PermissionId
                            where F.Id = @formId and UP.UserId = @userId  and FP.ActiveStatusId <> 3 and UP.ActiveStatusId <> 3
							";
            using (var multi = await connection.QueryMultipleAsync(mainQuery, new { formId, userId }))
            {
                var response = await multi.ReadAsync<long>();
                return response.ToList();
            }

        }
    }
}
