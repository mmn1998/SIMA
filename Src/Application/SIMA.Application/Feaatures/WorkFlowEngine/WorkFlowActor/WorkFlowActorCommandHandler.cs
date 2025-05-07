using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.WorkFlowActor;

public class WorkFlowActorCommandHandler : ICommandHandler<CreateWorkFlowActorCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorCommand, Result<long>>,
    ICommandHandler<ModifyWorkFlowActorCommand, Result<long>>,
    ICommandHandler<CreateWorkFlowActorRoleCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorRoleCommand, Result<long>>,
    ICommandHandler<CreateWorkFlowActorUserCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorUserCommand, Result<long>>,
    ICommandHandler<CreateWorkFlowActorGroupCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorGroupCommand, Result<long>>,
    ICommandHandler<CreateRelatedWorkFlowActorEntitiesCommand, Result<long>>

{
    private readonly IWorkFlowActorRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IWorkFlowActorDomainService _workFlowActorDomainService;

    public WorkFlowActorCommandHandler(IWorkFlowActorRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ISimaIdentity simaIdentity , IWorkFlowActorDomainService workFlowActorDomainService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
        _workFlowActorDomainService = workFlowActorDomainService;
    }
    public async Task<Result<long>> Handle(CreateWorkFlowActorCommand request, CancellationToken cancellationToken)
    {

        var arg = _mapper.Map<WorkFlowActorArg>(request);
        var userId = _simaIdentity.UserId;
        var actor = Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites.WorkFlowActor.New(arg, userId);
        await _repository.Add(actor);
        //request.Id = new WorkFlowActorId(actor.Id);

        if (request.RoleId is not null)
        {
            var args = _mapper.Map<List<CreateWorkFlowActorRoleArg>>(request.RoleId);
            foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
            actor.AddActorRoles(args , actor.Id.Value);
        }

        if (request.GroupId is not null)
        {
            var args = _mapper.Map<List<CreateWorkFlowActorGroupArg>>(request.GroupId);
            foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
            actor.AddActorGroups(args, actor.Id.Value);
        }

        if (request.UserId is not null)
        {
            var args = _mapper.Map<List<CreateWorkFlowActorUserArg>>(request.UserId);
            foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
            actor.AddActorUsers(args, actor.Id.Value);
        }

        await _unitOfWork.SaveChangesAsync();



        return Result.Ok(actor.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyWorkFlowActorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyWorkFlowActorArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg , _workFlowActorDomainService);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public async Task<Result<long>> Handle(DeleteWorkFlowActorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetById(request.Id);
            long userId = _simaIdentity.UserId;entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public async Task<Result<long>> Handle(CreateWorkFlowActorRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.WorkFlowActorId);
        var args = _mapper.Map<List<CreateWorkFlowActorRoleArg>>(request.RoleId);
        foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
        entity.AddActorRoles(args, request.WorkFlowActorId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.WorkFlowActorId);
    }
    public async Task<Result<long>> Handle(DeleteWorkFlowActorRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.WorkFlowActorId);
        entity.DeleteRole(request.Id, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(CreateWorkFlowActorUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.WorkFlowActorId);
        var args = _mapper.Map<List<CreateWorkFlowActorUserArg>>(request.UserId);
        foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
        entity.AddActorUsers(args , request.WorkFlowActorId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.WorkFlowActorId);

    }
    public async Task<Result<long>> Handle(DeleteWorkFlowActorUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.WorkFlowActorId);
        entity.DeleteUser(request.Id, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(CreateWorkFlowActorGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.WorkFlowActorId);
        var args = _mapper.Map<List<CreateWorkFlowActorGroupArg>>(request.GroupId);
        foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
        entity.AddActorGroups(args, request.WorkFlowActorId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.WorkFlowActorId);

    }
    public async Task<Result<long>> Handle(DeleteWorkFlowActorGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.WorkFlowActorId);
        entity.DeleteGroup(request.Id, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(CreateRelatedWorkFlowActorEntitiesCommand request, CancellationToken cancellationToken)
    {
        var actor = await _repository.GetById(request.Id);

        if (request.RoleId is not null)
        {
            var args = _mapper.Map<List<CreateWorkFlowActorRoleArg>>(request.RoleId);
            foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
            actor.AddActorRoles(args, request.Id);
        }

        if (request.GroupId is not null)
        {
            var args = _mapper.Map<List<CreateWorkFlowActorGroupArg>>(request.GroupId);
            foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
            actor.AddActorGroups(args, request.Id);
        }

        if (request.UserId is not null)
        {
            var args = _mapper.Map<List<CreateWorkFlowActorUserArg>>(request.UserId);
            foreach (var item in args) item.CreatedBy = _simaIdentity.UserId;
            actor.AddActorUsers(args, request.Id);
        }

        await _unitOfWork.SaveChangesAsync();



        return Result.Ok(actor.Id.Value);
    }
}
