using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.Back_UpPeriods;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Args;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Contracts;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.Back_UpPeriods;

public class BackupPeriodCommandHandler : ICommandHandler<CreateBackupPeriodCommand, Result<long>>,
    ICommandHandler<ModifyBackupPeriodCommand, Result<long>>, ICommandHandler<DeleteBackupPeriodCommand, Result<long>>
{
    private readonly IBackupPeriodRepository _repository;
    private readonly IBackupPeriodDomianService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BackupPeriodCommandHandler(IBackupPeriodRepository repository, IBackupPeriodDomianService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBackupPeriodCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBackupPeriodArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await BackupPeriod.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyBackupPeriodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBackupPeriodArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteBackupPeriodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}