using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.LicenseTypes;

public class LicenseTypeCommandHandler : ICommandHandler<CreateLicenseTypeCommand, Result<long>>,
    ICommandHandler<ModifyLicenseTypeCommand, Result<long>>, ICommandHandler<DeleteLicenseTypeCommand, Result<long>>
{
    private readonly ILicenseTypeRepository _repository;
    private readonly ILicenseTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public LicenseTypeCommandHandler(ILicenseTypeRepository repository, ILicenseTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateLicenseTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateLicenseTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await LicenseType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyLicenseTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new LicenseTypeId(request.Id));
        var arg = _mapper.Map<ModifyLicenseTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteLicenseTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new LicenseTypeId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}