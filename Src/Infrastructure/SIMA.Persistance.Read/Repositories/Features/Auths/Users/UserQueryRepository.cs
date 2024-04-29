using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Persistence;

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

    public async Task<bool> IsUsernameUnique(string username)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<int>("SELECT top 1 1 FROM Authentication.Users WHERE Username = @Username", new { Username = username });
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
    public async Task<LoginUserQueryResult> GetByUsernameAndPassword(string username, string password)
    {
        try
        {
            var response = new LoginUserQueryResult();

            var user = await _readContext.Users
                        .FirstOrDefaultAsync(u => u.Username == username);
            if (user is null) throw SimaResultException.InvalidUsernameOrPasswordError;
            user.Password.Verify(password);

            var query = @"
                        --user

                        select distinct
                        u.Id UserId, u.CompanyId,u.Username
                        from Authentication.Users u
                        where U.Id=@UserId  

                        --Roles
                        select distinct r.id RoleId
                        --r.Name rolename
                        from Authentication.Users u

                        inner join Authentication.UserRole ur on u.Id = ur.UserId
                        inner join Authentication.Role r on r.Id = ur.RoleId
                        where U.Id=@UserId  

                        --groups
                        select distinct g.id GroupId
                        --, g.Name groupname
                        from Authentication.Users u

                        inner  join Authentication.UserGroup ug on u.Id = ug.UserId
                        inner join Authentication.Groups g on g.Id = ug.GroupId
                        where U.Id=@UserId  

                        --permission
                        select distinct p1.Code Code
                          --,p1.Name namepermission
                        from Authentication.Users u
                        inner  join Authentication.UserPermission up on u.Id = up.UserId
                        inner join [Authentication].[Permission] P1 on P1.Id=up.PermissionId
                        where U.Id=@UserId  

                        union 

                        select distinct p2.Code Code
                         --,p2.Name namepermission
                        from Authentication.Users u
                        inner join Authentication.UserRole ur on u.Id = ur.UserId
                        inner join Authentication.Role r on r.Id = ur.RoleId
                        inner join Authentication.RolePermission rp on r.Id = rp.RoleId
                        inner join [Authentication].[Permission] P2 on P2.Id=rp.PermissionId
                        where U.Id=@UserId

                        union 

                        select distinct P3.Code Code
                         -- ,p3.Name namepermission
                        from Authentication.Users u
                        inner  join Authentication.UserGroup ug on u.Id = ug.UserId
                        inner join Authentication.Groups g on g.Id = ug.GroupId
                        inner join Authentication.GroupPermission gp on g.Id = gp.GroupId
                        inner join [Authentication].[Permission] P3 on P3.Id=gp.PermissionId
                        where U.Id=@UserId  

                        --Menus

                        SELECT distinct
                        UDA.DomainId,d.Name Name,'' Code

                         FROM  [Authentication].[UserDomainAccess] UDA
                         inner join [Authentication].[Users] U on U.Id=UDA.UserId
                         inner join Authentication.Domain D on D.Id=UDA.DomainId
                         where UDA.[UserId]=@UserId and   UDA.[ActiveStatusId]=1
                         and cast(getdate() as char(12))  between  UDA.[ActiveFrom] and  UDA.[ActiveTo]

                        union 

                        SELECT distinct
                        UDA.DomainId,f.Title Name,F.Code Code
                        FROM  [Authentication].[UserDomainAccess] UDA
                        inner join [Authentication].[Users] U on U.Id=UDA.UserId
                        inner join Authentication.Domain D on D.Id=UDA.DomainId
                        inner join  Authentication.FormUser fu on fu.UserId = u.Id
                        inner join Authentication.Form f on F.Id=Fu.FormId and f.DomainId=D.Id
                        where UDA.[UserId]=@UserId and   UDA.[ActiveStatusId]=1
                            and cast(getdate() as char(12))  between  UDA.[ActiveFrom] and  UDA.[ActiveTo]

                        union 

                        SELECT distinct
                        UDA.DomainId,f.Title Name,F.Code Code
                        FROM  [Authentication].[UserDomainAccess] UDA
                        inner join [Authentication].[Users] U on U.Id=UDA.UserId
                        inner join Authentication.Domain D on D.Id=UDA.DomainId
                        inner join Authentication.UserRole ur on u.Id = ur.UserId
                        inner join Authentication.Role r on r.Id = ur.RoleId
                        inner join  Authentication.FormRole fr on fr.RoleId = r.Id
                        inner join Authentication.Form f on F.Id=fr.FormId and f.DomainId=D.Id
                        where UDA.[UserId]=@UserId and   UDA.[ActiveStatusId]=1
                         and cast(getdate() as char(12))  between  UDA.[ActiveFrom] and  UDA.[ActiveTo]

                        union 

                        SELECT distinct
                        UDA.DomainId,f.Title Name,F.Code Code
                        FROM  [Authentication].[UserDomainAccess] UDA
                        inner join [Authentication].[Users] U on U.Id=UDA.UserId
                        inner join Authentication.Domain D on D.Id=UDA.DomainId
                            inner  join Authentication.UserGroup ug on u.Id = ug.UserId
                        inner join Authentication.Groups g on g.Id = ug.GroupId
                        inner join  Authentication.FormGroup fg on fg.GroupId= g.Id
                        inner join Authentication.Form f on F.Id=fg.FormId  and f.DomainId=D.Id
                        where UDA.[UserId]=@UserId and   UDA.[ActiveStatusId]=1
                         and cast(getdate() as char(12))  between  UDA.[ActiveFrom] and  UDA.[ActiveTo]
                    ";


            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync(query, new { UserId = user.Id.Value }))
                {
                    response.UserInfoLogin = multi.ReadAsync<UserInfoLogin>().GetAwaiter().GetResult().FirstOrDefault() ?? throw SimaResultException.NotFound;
                    response.RoleIds = await multi.ReadAsync<long>();
                    response.GroupIds = await multi.ReadAsync<long>();
                    response.Permissions = await multi.ReadAsync<int>();
                    response.TempMenues = await multi.ReadAsync<Menue>();
                }
            }
            response.Menue = new List<Menue>();
            foreach (var item in response.TempMenues)
            {
                if (string.IsNullOrEmpty(item.Code) && response.TempMenues.Where(it => it.DomainId == item.DomainId).Count() > 1)
                {
                    var subMenu = response.TempMenues.Where(it => it.DomainId == item.DomainId && it.Code != "").Select(it => new SubMenue
                    {
                        Code = it.Code,
                        Name = it.Name,
                    }).ToList();
                    response.Menue.Add(new Menue
                    {
                        Code = "",
                        DomainId = 0,
                        Name = item.Name,
                        SubMenues = subMenu,

                    });
                }
            }
            response.TempMenues = null;
            return response;
        }
        catch (Exception ex)
        {
            throw;
        }

    }


    public async Task<GetInfoByUserIdQueryResult> GetInfoByUserId(long userId)
    {

        var response = new GetInfoByUserIdQueryResult();
        using (var connection = new SqlConnection(_connectionString))
        {

            var query = $@" SELECT    distinct  
                          u.ID AS UserId,U.[Username] Username
                          , profile.FirstName, profile.LastName, profile.FatherName
                          , profile.GenderID  AS Gender, gender.Code AS GenderCode
                          , profile.NationalID, [Basic].[Miladi_To_Persian](profile.Brithday)Brithday
                          FROM            Authentication.Users AS u
                          INNER JOIN      Authentication.Profile AS profile ON u.ProfileID = profile.ID
                          INNER JOIN      Basic.Gender AS gender ON gender.ID = profile.GenderID
                          WHERE        (u.ID = @UserID) and u.[ActiveStatusID] = 1

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
                response.UserInfo = multi.ReadAsync<UserInfo>().GetAwaiter().GetResult().FirstOrDefault() ?? throw SimaResultException.UserNotFoundError;
                response.Phones = multi.ReadAsync<PhoneResult>().GetAwaiter().GetResult().ToList();
                response.Positions = multi.ReadAsync<PositionResult>().GetAwaiter().GetResult().ToList();
                response.Addresses = multi.ReadAsync<AddressResult>().GetAwaiter().GetResult().ToList();

            }
        }
        return response;
    }
    public async Task<GetProfileByProfileIdQueryResult> GetProfileByProfileId(long profileId)
    {
        ///todo sanaz
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
                response.ProfileInfo = multi.ReadAsync<ProfileInfo>()?.GetAwaiter().GetResult().FirstOrDefault() ?? throw SimaResultException.NotFound;
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
        return user?.ProfileId?.Value ?? throw SimaResultException.ProfileNotFoundError;
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
            if (result is null) throw SimaResultException.UserNotFoundError;
            if (result.IsDeleted) throw SimaResultException.UserIsDeletedError;
            if (result.IsDeactivated) throw SimaResultException.UserIsDeactiveError;
            return result;
        }

    }

    public async Task<Result<IEnumerable<GetUserQueryResult>>> GetAll(GetAllUserQuery? request = null)
    {

        

        using (var connection = new SqlConnection(_connectionString))
        {

            string queryCount = @"
                        SELECT Count(*) Result
                        FROM [Authentication].[Users] U
                        INNER JOIN [Authentication].[Profile] P on U.ProfileID = P.ID
                        INNER JOIN [Organization].[Company] C on C.ID = U.CompanyID
                        join [Basic].[ActiveStatus] A on A.Id = U.ActiveStatusID
                        WHERE (@SearchValue is null OR C.Name like @SearchValue OR P.FirstName like @SearchValue OR
                        P.LastName like @SearchValue OR U.Username like @SearchValue) and U.ActiveStatusId != 3";
            await connection.OpenAsync();
            string query = $@"
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
                        WHERE (@SearchValue is null OR C.Name like @SearchValue OR P.FirstName like @SearchValue OR
                        P.LastName like @SearchValue OR U.Username like @SearchValue) and U.ActiveStatusId != 3
                        order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                        OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetUserQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
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

    public async Task<GetUserPermissionQueryResult> GetUserPermission(long userPermissionId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT DISTINCT
	   [Id]
      ,[UserId]
      ,[PermissionId]
  FROM [Authentication].[UserPermission]
  WHERE ActiveStatusId = 1 AND Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetUserPermissionQueryResult>(query, new { Id = userPermissionId });
            result.NullCheck();
            return result;
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

    public async Task<GetUserDomainQueryResult> GetUserDomain(long userDomainId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
SELECT DISTINCT
	   [Id]
      ,[DomainId]
      ,[UserId]
  FROM [Authentication].[UserDomainAccess]
  WHERE ActiveStatusId = 1 AND Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetUserDomainQueryResult>(query, new { Id = userDomainId });
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
                SELECT U.Id,U.ProfileId,U.CompanyId,U.Username
                FROM Authentication.Users  U
                WHERE U.Id = @UserId;

                SELECT  distinct   D.name as  DomainName ,D.id as DomainId, UDA.id  as UserDomainId
                FROM            Authentication.Users  U
                INNER JOIN      Authentication.UserDomainAccess UDA ON U.ID = UDA.UserID
                INNER JOIN     Authentication.Domain D ON UDA.DomainID = D.ID
                where U.ID = @UserID and D.[ActiveStatusID]=1
                group by UDA.Id, D.name, D.id;

                SELECT distinct P.name PermissionName ,P.id  PermissionId , UP.id UserPermissionId, D.name as  DomainName ,D.id as DomainId
                FROM            Authentication.Users U INNER JOIN
                Authentication.UserPermission UP  ON U.ID =UP.UserID INNER JOIN
                Authentication.Permission P ON UP.PermissionID = P.ID
                INNER JOIN      Authentication.UserDomainAccess UDA ON U.ID = UDA.UserID
                INNER JOIN     Authentication.Domain D ON UDA.DomainID = D.ID
                where U.ID = @UserID and P.[ActiveStatusID]=1
                group by UP.id,P.name ,P.id, D.name, D.id;

                SELECT   R.name RoleName ,R.id RoleId,UR.id UserRoleId
                FROM            Authentication.Users U INNER JOIN
                Authentication.UserRole UR ON U.ID =UR.UserID   INNER JOIN
                Authentication.Role  R ON UR.RoleID = R.ID
                where  U.ID = @UserID and UR.[ActiveStatusID]=1
                group by UR.id,R.name ,R.id;

                SELECT ULA.id UserLocationId ,LL.name as  LocationName,ll.id as  LocationId,LL.LocationTypeID as  LocationTypeId,LT.Name as  LocationTypeName
                FROM            Authentication.Users AS U INNER JOIN
                Authentication.UserLocationAccess AS ULA ON U.ID = ULA.UserID INNER JOIN
                Basic.Location LL ON ULA.LocationID = LL.ID  INNER JOIN
                Basic.LocationType AS LT ON LL.LocationTypeID = LT.ID
                where  U.ID = @UserID and LL.[ActiveStatusID]=1
                group by   ULA.id  ,LL.name ,ll.id,LL.LocationTypeID ,LT.Name;
";
            using (var multi = await connection.QueryMultipleAsync(query, new { UserID = userId }))
            {
                response.User = multi.ReadAsync<GetUserQueryForAggregate>().GetAwaiter().GetResult().FirstOrDefault() ?? throw SimaResultException.NotFound;
                response.UserPermissions = multi.ReadAsync<GetUserPermissionQueryForAggregate>().GetAwaiter().GetResult().ToList();
                response.UserRoles = multi.ReadAsync<GetUserRoleQueryForAggregate>().GetAwaiter().GetResult().ToList();
                response.UserDomains = multi.ReadAsync<GetUserDomainQueryForAggregate>().GetAwaiter().GetResult().ToList();
                response.UserLocations = multi.ReadAsync<GetUserLocationQueryForAggregate>().GetAwaiter().GetResult().ToList();
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
}
