using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.StrategyTypes;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Args;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Contracts;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.StrategyTypes;

public class StrategyTypeCommandHandler : ICommandHandler<CreateStrategyTypeCommand, Result<long>>,
    ICommandHandler<ModifyStrategyTypeCommand, Result<long>>, ICommandHandler<DeleteStrategyTypeCommand, Result<long>>
{
    private readonly IStrategyTypeRepository _repository;
    private readonly IStrategyTypeDomianService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public StrategyTypeCommandHandler(IStrategyTypeRepository repository, IStrategyTypeDomianService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateStrategyTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateStrategyTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await StrategyType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyStrategyTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyStrategyTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteStrategyTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}