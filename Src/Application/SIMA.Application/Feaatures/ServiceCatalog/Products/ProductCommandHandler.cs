using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.Products;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Args;

namespace SIMA.Application.Feaatures.ServiceCatalog.Products;

public class ProductCommandHandler : ICommandHandler<CreateProductCommand, Result<long>>, ICommandHandler<DeleteProductCommand, Result<long>>, ICommandHandler<ModifyProductCommand, Result<long>>

{
    private readonly IProductRepository _repository;
    private readonly IProductDomainService _productDomainService;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
        IProductDomainService productDomainService, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productDomainService = productDomainService;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateProductArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Product.Create(arg);
        if (request.Channels is not null && request.Channels.Count > 0)
        {
            var channelArg = _mapper.Map<List<CreateProductChannelArg>>(request.Channels);
            foreach (var item in channelArg)
            {
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.CreateChannel(channelArg, entity.Id.Value);
        }
        if (request.ProductResponsibles is not null && request.ProductResponsibles.Count > 0)
        {
            var responsibleArg = _mapper.Map<List<CreateProductResponsibleArg>>(request.ProductResponsibles);
            foreach (var item in responsibleArg)
            {
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.CreateResponsible(responsibleArg, entity.Id.Value);
        }
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ProductId(request.Id));
        var arg = _mapper.Map<ModifyProductArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg);

        if (request.Channels is not null && request.Channels.Count > 0)
        {
            var channelArg = _mapper.Map<List<CreateProductChannelArg>>(request.Channels);
            foreach (var item in channelArg)
            {
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.CreateChannel(channelArg, entity.Id.Value);
        }
        if (request.ProductResponsibles is not null && request.ProductResponsibles.Count > 0)
        {
            var responsibleArg = _mapper.Map<List<CreateProductResponsibleArg>>(request.ProductResponsibles);
            foreach (var item in responsibleArg)
            {
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.CreateResponsible(responsibleArg, entity.Id.Value);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ProductId(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

}