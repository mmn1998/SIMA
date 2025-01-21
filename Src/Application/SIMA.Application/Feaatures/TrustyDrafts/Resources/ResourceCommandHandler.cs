using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.Resources;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.Resources;

public class ResourceCommandHandler : ICommandHandler<CreateResourceCommand, Result<long>>,
    ICommandHandler<ModifyResourceCommand, Result<long>>, ICommandHandler<DeleteResourceCommand, Result<long>>
{
    private readonly IResourceRepository _repository;
    private readonly IResourceDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ResourceCommandHandler(IResourceRepository repository, IResourceDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateResourceArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Resource.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyResourceArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}