using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Profiles;

public interface IProfileQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetAddressBookQueryResult>>> FindWithAddressBook(long id, BaseRequest? baseRequest = null);
    Task<List<GetPhoneBookQueryResult>> FindWithPhoneBook(long id);
    Task<GetProfileQueryResult> FindById(long id);
    Task<List<GetShortProfileQueryResult>> GetShort();
    Task<Result<IEnumerable<GetProfileQueryResult>>> GetAll(GetAllProfileQuery request);
    Task<Result<IEnumerable<GetPhoneBookQueryResult>>> GetAllPhoneBooks(int profileId, BaseRequest? baseRequest = null);
    Task<List<SelectModel>> GetMangersByCompanyId(long id);
}
