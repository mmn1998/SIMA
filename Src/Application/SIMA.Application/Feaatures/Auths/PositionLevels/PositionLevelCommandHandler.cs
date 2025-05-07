using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.PositionLevels;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Args;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Contracts;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Entities;
using SIMA.Domain.Models.Features.Auths.PositionLevels.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.PositionLevels;

public class PositionLevelCommandHandler : ICommandHandler<CreatePositionLevelCommand, Result<long>>,
    ICommandHandler<ModifyPositionLevelCommand, Result<long>>, ICommandHandler<DeletePositionLevelCommand, Result<long>>
{
    private readonly IPositionLevelRepository _repository;
    private readonly IPositionLevelDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public PositionLevelCommandHandler(IPositionLevelRepository repository, IPositionLevelDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreatePositionLevelCommand request, CancellationToken cancellationToken)
    {

        var arg = _mapper.Map<CreatePositionLevelArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await PositionLevel.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyPositionLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new PositionLevelId(request.Id));
        var arg = _mapper.Map<ModifyPositionLevelArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeletePositionLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new PositionLevelId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}