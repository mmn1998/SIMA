using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Groups;
using SIMA.Domain.Models.Features.Auths.Groups;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Groups;

public class GroupCommandHandler : ICommandHandler<CreateGroupCommand, Result<long>>, ICommandHandler<DeleteGroupCommand, Result<long>>,
    ICommandHandler<UpdateGroupCommand, Result<long>>,
    ICommandHandler<CreateGroupPermissionCommand, Result<long>>, ICommandHandler<UpdateGroupPermissionCommand, Result<long>>,
    ICommandHandler<CreateGroupUserCommand, Result<long>>,
    ICommandHandler<UpdateGroupUserCommand, Result<long>>, ICommandHandler<DeleteUserGroupCommand, Result<long>>,
    ICommandHandler<DeleteGroupPermissionCommand, Result<long>>, ICommandHandler<CreateGroupAggregate, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IGroupRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGroupService _service;
    private readonly ISimaIdentity _simaIdentity;

    public GroupCommandHandler(IMapper mapper, IGroupRepository repository,
        IUnitOfWork unitOfWork, IGroupService service, ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGroupArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Group.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyGroupArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateGroupPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.GroupId);
        var arg = _mapper.Map<CreateGroupPermissionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        await entity.AddGroupPermission(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(UpdateGroupPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.GroupId);
        var arg = _mapper.Map<ModifyGroupPermissionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.ModifyGroupPermission(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateGroupUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.GroupId);
        var arg = _mapper.Map<CreateUserGroupArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        await entity.AddGroupUser(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(UpdateGroupUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.GroupId);
        var arg = _mapper.Map<ModifyUserGroupArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.ModifyGroupUser(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.GroupId);
        entity.DeleteUserGroup(request.UserGroupId, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteGroupPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.GroupId);
        entity.DeleteUserGroup(request.GroupPermissionId, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateGroupAggregate request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGroupArg>(request.Group);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Group.Create(arg, _service);
        await _repository.Add(entity);
        if (request.UserGroups != null && request.UserGroups.Count != 0)
        {
            foreach (var group in request.UserGroups) group.GroupId = entity.Id.Value;
            var args = _mapper.Map<List<CreateUserGroupArg>>(request.UserGroups);
            foreach (var GroupArg in args) GroupArg.CreatedBy = _simaIdentity.UserId;
            await entity.AddGroupUsers(args);
        }
        if (request.GroupPermissions != null && request.GroupPermissions.Count != 0)
        {
            foreach (var permission in request.GroupPermissions) permission.GroupId = entity.Id.Value;
            var args = _mapper.Map<List<CreateGroupPermissionArg>>(request.GroupPermissions);
            foreach (var GroupArg in args) GroupArg.CreatedBy = _simaIdentity.UserId;
            await entity.AddGroupPermissions(args);
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
