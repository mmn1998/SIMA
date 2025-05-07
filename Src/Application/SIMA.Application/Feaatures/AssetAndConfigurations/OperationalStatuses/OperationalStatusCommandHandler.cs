using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseTypes;
using SIMA.Application.Contract.Features.AssetAndConfigurations.OperationalStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.OperationalStatuses;

public class OperationalStatusCommandHandler: ICommandHandler<CreateOperationalStatusCommand, Result<long>>,
    ICommandHandler<ModifyOperationalStatusCommand, Result<long>>, ICommandHandler<DeleteOperationalStatusCommand, Result<long>>
{
    private readonly IOperationalStatusRepository _repository;
    private readonly IOperationalStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public OperationalStatusCommandHandler(IOperationalStatusRepository repository, IOperationalStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }

    public async Task<Result<long>> Handle(CreateOperationalStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateOperationalStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await OperationalStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyOperationalStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new OperationalStatusId(request.Id));
        var arg = _mapper.Map<ModifyOperationalStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteOperationalStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new OperationalStatusId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}