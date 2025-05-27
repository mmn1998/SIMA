using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.LicenseStatuses;

public class LicenseStatusCommandHandler : ICommandHandler<CreateLicenseStatusCommand, Result<long>>,
    ICommandHandler<ModifyLicenseStatusCommand, Result<long>>, ICommandHandler<DeleteLicenseStatusCommand, Result<long>>
{
    private readonly ILicenseStatusRepository _repository;
    private readonly ILicenseStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;



    public LicenseStatusCommandHandler(ILicenseStatusRepository repository, ILicenseStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateLicenseStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateLicenseStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await LicenseStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyLicenseStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyLicenseStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteLicenseStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}