using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceCategories;

public class ServiceCategoryCommandHandler : ICommandHandler<CreateServiceCategoryCommand, Result<long>>,
    ICommandHandler<ModifyServiceCategoryCommand, Result<long>>, ICommandHandler<DeleteServiceCategoryCommand, Result<long>>
{
    private readonly IServiceCategoryRepository _repository;
    private readonly IServiceCategoryDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ServiceCategoryCommandHandler(IServiceCategoryRepository repository, IServiceCategoryDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateServiceCategoryArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ServiceCategory.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceCategoryId(request.Id));
        var arg = _mapper.Map<ModifyServiceCategoryArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceCategoryId(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}