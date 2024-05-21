using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Events;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;
using SIMA.Resources;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly SIMADBContext _context;

    public UserRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetById(long id)
    {
        var entity = await _context.Users
        .Include(u => u.UserConfigs)
        .Include(u => u.UserGroups)
        .Include(u => u.UserPermissions)
        .Include(u => u.UserRoles)
        .Include(u => u.AdminLocationAccesses)
        .Include(u => u.UserDomainAccesses)
            .FirstOrDefaultAsync(u => u.Id == new UserId(id));
        if (entity is null) throw new SimaResultException("10051",Messages.UserNotFoundError);
        return entity;
    }

    public async Task<User> GetByUserName(string userName)
    {
        var entity = await _context.Users
        .Include(u => u.UserConfigs)
        .Include(u => u.UserGroups)
        .Include(u => u.UserPermissions)
        .Include(u => u.UserRoles)
        .Include(u => u.AdminLocationAccesses)
        .Include(u => u.UserDomainAccesses)
            .FirstOrDefaultAsync(u => u.Username == userName);
        entity.NullCheck();
        return entity;
    }

    public async Task<SSOInfoUserEvent> GetUserInfoWithSSO(string tiket)
    {
        var username = await GetUserNameSSO(tiket);
        SSOInfoUserEvent ssoInfo = await GetSSOUserInfo(username);
        return ssoInfo;
    }
    private async Task<string> GetUserNameSSO(string tiket)
    {
        string UserName = string.Empty;

        string apiUrl = "http://172.20.156.218:8059/api/AuthenticationSSO/GetUsernameByTiket";
        string wsUsername = "yaas_sales56t";
        string wsPassword = "u8T%5E%23WF%25_%29%28_%2195%2102%2114_13%2119%21";
        string ticket = tiket;

        string fullUrl = $"{apiUrl}?wsUsername={wsUsername}&wsPassword={wsPassword}&ticket={ticket}";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    UserName = responseBody;
                }
                else
                {
                    return UserName;
                }
            }
            catch (Exception ex)
            {
                return UserName;
            }

            return UserName;
        }
    }
    private async Task<SSOInfoUserEvent> GetSSOUserInfo(string username)
    {
        SSOInfoUserEvent result = new SSOInfoUserEvent();
        string apiUrl = "http://172.20.156.218:8059/api/UserProfile/GetByEmployeeCode";
        string wsUsername = "yaas_sales56t";
        string wsPassword = "u8T%5E%23WF%25_%29%28_%2195%2102%2114_13%2119%21";
        string employeeCode = username;

        string fullUrl = $"{apiUrl}?wsUsername={wsUsername}&wsPassword={wsPassword}&employeeCode={employeeCode}";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var replaceBody = responseBody.Replace("@", "");
                    result = JsonConvert.DeserializeObject<SSOInfoUserEvent>(replaceBody);
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return result;
            }

            return result;
        }
    }
}
