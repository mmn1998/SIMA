using SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Products;

namespace SIMA.Application.Query.Features.ServiceCatalog.Product;

public class ProductQueryHandler : IQueryHandler<GetProductQuery, Result<GetProductQueryResult>>,
    IQueryHandler<GetAllProductQuery, Result<IEnumerable<GetProductQueryResult>>>

    {
    private readonly IProductQueryRepository _repository;

    public ProductQueryHandler(IProductQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetProductQueryResult>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetProductQueryResult>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll(request);
        return result;
    }
}