using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Permission;
using SIMA.Domain.Models.Features.Auths.Permissions;
using SIMA.Domain.Models.Features.Auths.Permissions.Args;
using SIMA.Domain.Models.Features.Auths.Permissions.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Permision;

public class PermissionCommandHandler :
     ICommandHandler<CreatePermissionCommand, Result<long>>
    ,ICommandHandler<DeletePermissionCommand, Result<long>>
    ,ICommandHandler<AddPermissionCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IPermissionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissionService _service;
    private readonly ISimaIdentity _simaIdentity;

    public PermissionCommandHandler(IMapper mapper, IPermissionRepository repository,
        IUnitOfWork unitOfWork, IPermissionService service, ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreatePermissionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Domain.Models.Features.Auths.Permissions.Entities.Permission.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var args = await _repository.AddPermissionNotExist();
            foreach (var arg in args)
            {
                var entity = await Domain.Models.Features.Auths.Permissions.Entities.Permission.Create(arg, _service);
                await _repository.Add(entity);
            }
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(args[0].Id);
        }
        catch(Exception ex)
        {
            throw;
        }
        
    }
    public async Task<Result<long>> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
