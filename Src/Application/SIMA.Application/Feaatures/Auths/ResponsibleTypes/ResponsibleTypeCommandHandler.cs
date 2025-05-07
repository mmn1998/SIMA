using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.ResponsibleTypes;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Args;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.ResponsibleTypes;

public class ResponsibleTypeCommandHandler : ICommandHandler<CreateResponsibleTypeCommand, Result<long>>, ICommandHandler<DeleteResponsibleTypeCommand, Result<long>>,
    ICommandHandler<ModifyResponsibleTypeCommands, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IResponsibleTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResponsibleTypeDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public ResponsibleTypeCommandHandler(IMapper mapper, IResponsibleTypeRepository repository,
        IUnitOfWork unitOfWork, IResponsibleTypeDomainService service, ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateResponsibleTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateResponsibleTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ResponsibleType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyResponsibleTypeCommands request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyResponsibleTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteResponsibleTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
