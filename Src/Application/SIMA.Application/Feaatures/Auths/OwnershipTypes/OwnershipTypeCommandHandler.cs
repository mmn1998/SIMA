using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.OwnershipTypes;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Args;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.OwnershipTypes;

public class OwnershipTypeCommandHandler : ICommandHandler<CreateOwnershipTypeCommand, Result<long>>,
    ICommandHandler<ModifyOwnershipTypeCommand, Result<long>>, ICommandHandler<DeleteOwnershipTypeCommand, Result<long>>
{
    private readonly IOwnershipTypeRepository _repository;
    private readonly IOwnershipTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public OwnershipTypeCommandHandler(IOwnershipTypeRepository repository, IOwnershipTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateOwnershipTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateOwnershipTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await OwnershipType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyOwnershipTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyOwnershipTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteOwnershipTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}