using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.BusinessCriticalities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.BusinessCriticalities;

public class BusinessCriticalityCommandHandler : ICommandHandler<CreateBusinessCriticalityCommand, Result<long>>,
    ICommandHandler<ModifyBusinessCriticalityCommand, Result<long>>, ICommandHandler<DeleteBusinessCriticalityCommand, Result<long>>
{
    private readonly IBusinessCriticalityRepository _repository;
    private readonly IBusinessCriticalityDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BusinessCriticalityCommandHandler(IBusinessCriticalityRepository repository, IBusinessCriticalityDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBusinessCriticalityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBusinessCriticalityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await BusinessCriticality.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyBusinessCriticalityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBusinessCriticalityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteBusinessCriticalityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}