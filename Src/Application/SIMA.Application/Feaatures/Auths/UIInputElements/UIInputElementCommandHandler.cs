using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.UIInputElements;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Args;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Contracts;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Entities;
using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.UIInputElements;

public class UIInputElementCommandHandler : ICommandHandler<CreateUIInputElementCommand, Result<long>>,
    ICommandHandler<ModifyUIInputElementCommand, Result<long>>, ICommandHandler<DeleteUIInputElementCommand, Result<long>>
{
    private readonly IUIInputElementRepository _repository;
    private readonly IUIInputElementDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public UIInputElementCommandHandler(IUIInputElementRepository repository, IUIInputElementDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateUIInputElementCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUIInputElementArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await UIInputElement.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyUIInputElementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new UIInputElementId(request.Id));
        var arg = _mapper.Map<ModifyUIInputElementArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteUIInputElementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new UIInputElementId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}