using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Back_UpMethods;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Back_UpMethods;

public class BackupMethodCommandHandler : ICommandHandler<CreateBackupMethodCommand, Result<long>>,
    ICommandHandler<ModifyBackupMethodCommand, Result<long>>, ICommandHandler<DeleteBackupMethodCommand, Result<long>>
{
    private readonly IBackupMethodRepository _repository;
    private readonly IBackupMethodDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BackupMethodCommandHandler(IBackupMethodRepository repository, IBackupMethodDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBackupMethodCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBackupMethodArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await BackupMethod.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyBackupMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBackupMethodArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteBackupMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}