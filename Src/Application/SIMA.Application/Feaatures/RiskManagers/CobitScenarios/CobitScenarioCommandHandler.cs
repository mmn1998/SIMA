using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.CobitScenarios;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Args;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.CobitScenarios;

public class CobitScenarioCommandHandler : ICommandHandler<CreateCobitScenarioCommand, Result<long>>, ICommandHandler<ModifyCobitScenarioCommand, Result<long>>
, ICommandHandler<DeleteCobitScenarioCommand, Result<long>>
{
    private readonly ICobitScenarioRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICobitScenarioDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public CobitScenarioCommandHandler(ICobitScenarioRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ICobitScenarioDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateCobitScenarioCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCobitScenarioArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = CobitScenario.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyCobitScenarioCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyCobitScenarioArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteCobitScenarioCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}