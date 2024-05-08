using AutoMapper;
using Microsoft.Extensions.Logging;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.SysConfigs;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Args;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.SysConfigs;

public class SysConfigCommandHandler : ICommandHandler<DeleteSysConfigCommand, Result<long>>,
    ICommandHandler<CreateSystemConfigurationCommand, Result<long>>
{
    private readonly ISysConfigRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public SysConfigCommandHandler(ISysConfigRepository repository,
        IUnitOfWork unitOfWork, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(DeleteSysConfigCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateSystemConfigurationCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSysConfigArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await SysConfig.Create(arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
