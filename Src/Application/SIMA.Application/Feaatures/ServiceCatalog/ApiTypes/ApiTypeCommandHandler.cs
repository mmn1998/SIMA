using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ApiTypes;

public class ApiTypeCommandHandler : ICommandHandler<CreateApiTypeCommand, Result<long>>,
ICommandHandler<ModifyApiTypeCommand, Result<long>>, ICommandHandler<DeleteApiTypeCommand, Result<long>>
{
    private readonly IApiTypeRepository _repository;
    private readonly IApiTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ApiTypeCommandHandler(IApiTypeRepository repository, IApiTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateApiTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateApiTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ApiType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyApiTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ApiTypeId(request.Id));
        var arg = _mapper.Map<ModifyApiTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteApiTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ApiTypeId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}