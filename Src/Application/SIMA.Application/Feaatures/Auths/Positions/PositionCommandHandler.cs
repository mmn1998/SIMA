using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Positions;
using SIMA.Domain.Models.Features.Auths.Positions.Args;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.Interfaces;
using SIMA.Domain.Models.Features.Auths.Positions.Repositories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Positions;

public class PositionCommandHandler : ICommandHandler<CreatePositionCommand, Result<long>>, ICommandHandler<DeletePositionCommand, Result<long>>,
    ICommandHandler<ModifyPositionCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IPositionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPositionService _service;
    private readonly ISimaIdentity _simaIdentity;

    public PositionCommandHandler(IMapper mapper, IPositionRepository repository,
        IUnitOfWork unitOfWork, IPositionService service, ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreatePositionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Position.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyPositionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyPositionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
