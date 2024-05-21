using SIMA.Application.Query.Contract.Features.Auths.ViewLists;
using SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.ViewLists
{
    public interface IViewListQueryRepositorty : IQueryRepository
    {
        Task<Result<List<GetViewListQueryResult>>> GetAll(GetAllViewListQuery request);
        Task<Result<List<GetViewFieldQueryResult>>> GetAllViewFeild(GetAllViewFieldQuery request);
    }
}
