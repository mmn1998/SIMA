using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.BusinessCriticalities;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Categories;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Categories;

public class CategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Result<long>>,
    ICommandHandler<ModifyCategoryCommand, Result<long>>, ICommandHandler<DeleteCategoryCommand, Result<long>>
{
    private readonly ICategoryRepository _repository;
    private readonly ICategoryDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public CategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }

    public  async Task<Result<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCategoryArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Category.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyCategoryArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}