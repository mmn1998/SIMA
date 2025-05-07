using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.AccessTypes;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Args;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.AccessTypes;

public class AccessTypeCommandHandler : ICommandHandler<CreateAccessTypeCommand, Result<long>>,
    ICommandHandler<ModifyAccessTypeCommand, Result<long>>, ICommandHandler<DeleteAccessTypeCommand, Result<long>>
{
    private readonly IAccessTypeRepository _repository;
    private readonly IAccessTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public AccessTypeCommandHandler(IAccessTypeRepository repository, IAccessTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateAccessTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAccessTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await AccessType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyAccessTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAccessTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteAccessTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}