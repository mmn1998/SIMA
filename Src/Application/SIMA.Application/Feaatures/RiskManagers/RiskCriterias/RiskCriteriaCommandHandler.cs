using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.RiskCriterias;
using SIMA.Application.Contract.Features.RiskManagers.RiskCriterias;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.RiskCriterias;

public class RiskCriteriaCommandHandler : ICommandHandler<CreateRiskCriteriaCommand, Result<long>>, ICommandHandler<ModifyRiskCriteriaCommand, Result<long>>,
    ICommandHandler<DeleteRiskCriteriaCommand, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IRiskCriteriaDomainService _service;
    private readonly IRiskCriteriaRepository _repository;

    public RiskCriteriaCommandHandler(IUnitOfWork unitOfWork,IMapper mapper, ISimaIdentity simaIdentity, IRiskCriteriaDomainService service, IRiskCriteriaRepository repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
        _service = service;
        _repository = repository;
    }

    public async Task<Result<long>> Handle(CreateRiskCriteriaCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateRiskCriteriaArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await RiskCriteria.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyRiskCriteriaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyRiskCriteriaArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteRiskCriteriaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
