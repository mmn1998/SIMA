using SIMA.Application.Query.Contract.Features.Auths.ViewLists;
using SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.ViewLists;

namespace SIMA.Application.Query.Features.Auths.ViewLists
{
    public class ViewListQueryHandler : IQueryHandler<GetAllViewListQuery, Result<List<GetViewListQueryResult>>>, IQueryHandler<GetAllViewFieldQuery, Result<List<GetViewFieldQueryResult>>>
    {
        private readonly IViewListQueryRepositorty _repository;

        public ViewListQueryHandler(IViewListQueryRepositorty repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<GetViewListQueryResult>>> Handle(GetAllViewListQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<List<GetViewFieldQueryResult>>> Handle(GetAllViewFieldQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllViewFeild(request);
        }
    }
}
