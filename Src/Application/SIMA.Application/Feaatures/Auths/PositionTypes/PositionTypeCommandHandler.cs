using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.PositionTypes;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Args;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.PositionTypes;

public class PositionTypeCommandHandler : ICommandHandler<CreatePositionTypeCommand, Result<long>>,
    ICommandHandler<ModifyPositionTypeCommand, Result<long>>, ICommandHandler<DeletePositionTypeCommand, Result<long>>
{
    private readonly IPositionTypeRepository _repository;
    private readonly IPositionTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public PositionTypeCommandHandler(IPositionTypeRepository repository, IPositionTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreatePositionTypeCommand request, CancellationToken cancellationToken)
    {

        var arg = _mapper.Map<CreatePositionTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await PositionType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyPositionTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new PositionTypeId(request.Id));
        var arg = _mapper.Map<ModifyPositionTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeletePositionTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new PositionTypeId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}