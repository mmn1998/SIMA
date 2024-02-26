using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Profiles;

public interface IProfileQueryRepository : IQueryRepository
{
    Task<Result<List<GetAddressBookQueryResult>>> FindWithAddressBook(long id, BaseRequest? baseRequest = null);
    Task<List<GetPhoneBookQueryResult>> FindWithPhoneBook(long id);
    Task<GetProfileQueryResult> FindById(long id);
    Task<List<GetShortProfileQueryResult>> GetShort();
    Task<Result<List<GetProfileQueryResult>>> GetAll(BaseRequest? baseRequest = null);
    Task<Result<List<GetPhoneBookQueryResult>>> GetAllPhoneBooks(int profileId, BaseRequest? baseRequest = null);
    Task<List<SelectModel>> GetMangersByCompanyId(long id);
}
