using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.Origins;
using SIMA.Domain.Models.Features.BCP.Origins.Args;
using SIMA.Domain.Models.Features.BCP.Origins.Contracts;
using SIMA.Domain.Models.Features.BCP.Origins.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.Origins;

public class OriginCommandHandler : ICommandHandler<CreateOriginCommand, Result<long>>,
    ICommandHandler<ModifyOriginCommand, Result<long>>, ICommandHandler<DeleteOriginCommand, Result<long>>
{
    private readonly IOriginRepository _repository;
    private readonly IOriginDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public OriginCommandHandler(IOriginRepository repository, IOriginDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateOriginCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateOriginArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Origin.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyOriginCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyOriginArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteOriginCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}