using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.ApiMethodActions;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Args;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Contracts;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.ApiMethodActions;

public class ApiMethodActionCommandHandler : ICommandHandler<CreateApiMethodActionCommand, Result<long>>,
    ICommandHandler<ModifyApiMethodActionCommand, Result<long>>, ICommandHandler<DeleteApiMethodActionCommand, Result<long>>
{
    private readonly IApiMethodActionRepository _repository;
    private readonly IApiMethodActionDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ApiMethodActionCommandHandler(IApiMethodActionRepository repository, IApiMethodActionDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateApiMethodActionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateApiMethodActionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ApiMethodAction.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyApiMethodActionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ApiMethodActionId(request.Id));
        var arg = _mapper.Map<ModifyApiMethodActionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteApiMethodActionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ApiMethodActionId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}