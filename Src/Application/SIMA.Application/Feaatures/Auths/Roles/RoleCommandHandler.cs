using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Roles;
using SIMA.Domain.Models.Features.Auths.Roles.Args;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Roles;


public class RoleCommandHandler : ICommandHandler<DeleteRoleCommand, Result<long>>, ICommandHandler<CreateRoleCommand, Result<long>>,
    /*, ICommandHandler<CreateRolePermissionCommand, Result<long>>,*/ ICommandHandler<UpdateRolePermissionCommand, Result<long>>,
    ICommandHandler<UpdateRoleCommand, Result<long>>, 
    ICommandHandler<DeleteRolePermissionCommand, Result<long>>, ICommandHandler<CreateRoleAggregate, Result<long>>

{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;
    private readonly ISimaIdentity _simaIdentity;

    public RoleCommandHandler(IRoleRepository repository,
        IUnitOfWork unitOfWork, IMapper mapper, IRoleService roleService, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _roleService = roleService;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateRoleArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Role.Create(_roleService, arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    //public async Task<Result<long>> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    //{
    //    var entity = await _repository.GetById(request.RoleId);
    //    var arg = _mapper.Map<CreateRolePermissionArg>(request);
    //    arg.CreatedBy = _simaIdentity.UserId;
    //    await entity.AddRolePermission(arg);
    //    await _unitOfWork.SaveChangesAsync();
    //    return Result.Ok(entity.Id.Value);
    //}

    public async Task<Result<long>> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.RoleId);
        var arg = _mapper.Map<ModifyRolePermissionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.ModifyRolePermission(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyRoleArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _roleService);

        if (request.RolePermissions != null && request.RolePermissions.Count != 0)
        {
            var args = _mapper.Map<List<CreateRolePermissionArg>>(request.RolePermissions);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.RoleId = entity.Id.Value;
            }
            await entity.AddRolePermission(args, entity.Id.Value);
        }


        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.RoleId);
        entity.DeleteRolePermission(request.RolePermissionId, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateRoleAggregate request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateRoleArg>(request.Role);
        var entity = await Role.Create(_roleService, arg);
        await _repository.Add(entity);
        if (request.RolePermissions != null && request.RolePermissions.Count != 0)
        {
            var args = _mapper.Map<List<CreateRolePermissionArg>>(request.RolePermissions);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.RoleId = entity.Id.Value;
            }
            await entity.AddRolePermission(args , entity.Id.Value);
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    
}
