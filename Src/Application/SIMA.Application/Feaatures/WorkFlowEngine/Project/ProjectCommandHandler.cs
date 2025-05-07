using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.Project;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.Project;

public class ProjectCommandHandler : ICommandHandler<CreateProjectCommand, Result<long>>, ICommandHandler<DeleteProjectCommand, Result<long>>, ICommandHandler<ModifyProjectCommand, Result<long>>
    , /*ICommandHandler<CreateProjectGroupCommand, Result<long>>,*/ ICommandHandler<DeleteProjectGroupCommand, Result<long>>
    , /*ICommandHandler<CreateProjectMemberCommand, Result<long>>,*/ ICommandHandler<DeleteProjectMemberCommand, Result<long>>
{
    private readonly IProjectRepository _repository;
    private readonly IWorkFlowRepository _workFlowRepository;
    private readonly IProjectDomainService _projectDomainService;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectCommandHandler(IProjectRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
        IWorkFlowRepository workFlowRepository, IProjectDomainService projectDomainService,
        ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _workFlowRepository = workFlowRepository;
        _projectDomainService = projectDomainService;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateProjectArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Domain.Models.Features.WorkFlowEngine.Project.Entites.Project.New(arg, _projectDomainService);
        await _repository.Add(entity);

        if (request.GroupId is not null && request.GroupId.Count > 0)
        {
            var groupArg = _mapper.Map<List<CreateProjectGroupArg>>(request.GroupId);
            foreach (var item in groupArg) item.CreatedBy = _simaIdentity.UserId;
            entity.AddProjectGroup(groupArg, entity.Id.Value);
        }

        if (request.ProjectMember is not null && request.ProjectMember.Count > 0)
        {
            var memberArg = _mapper.Map<List<CreateProjectMemberArg>>(request.ProjectMember);
            foreach (var item in memberArg) item.CreatedBy = _simaIdentity.UserId;
            entity.AddProjectMember(memberArg, entity.Id.Value);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(entity.Id.Value);

    }
    public async Task<Result<long>> Handle(ModifyProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyProjectArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _projectDomainService);

        if (request.GroupId is not null && request.GroupId.Count > 0)
        {
            var groupArg = _mapper.Map<List<CreateProjectGroupArg>>(request.GroupId);
            foreach (var item in groupArg) item.CreatedBy = _simaIdentity.UserId;
            entity.AddProjectGroup(groupArg, request.Id);
        }
        if (request.ProjectMember is not null && request.ProjectMember.Count > 0)
        {
            var memberArg = _mapper.Map<List<CreateProjectMemberArg>>(request.ProjectMember);
            foreach (var item in memberArg) item.CreatedBy = _simaIdentity.UserId;
            entity.AddProjectMember(memberArg, entity.Id.Value);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    //public async Task<Result<long>> Handle(CreateProjectGroupCommand request, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        var entity = await _repository.GetById(request.ProjectId);
    //        entity.AddProjectGroup(request.GroupId);
    //        await _unitOfWork.SaveChangesAsync();
    //        return Result.Ok(request.ProjectId);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }

    //}
    public async Task<Result<long>> Handle(DeleteProjectGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.ProjectId);
        entity.DeleteProjectGroup(request.Id, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    //public async Task<Result<long>> Handle(CreateProjectMemberCommand request, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        var entity = await _repository.GetById(request.ProjectId);
    //        var arg = _mapper.Map<List<CreateProjectMemberArg>>(request.ProjectMember);
    //        entity.AddProjectMember(arg);
    //        await _unitOfWork.SaveChangesAsync();
    //        return Result.Ok(request.ProjectId);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }

    //}
    public async Task<Result<long>> Handle(DeleteProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.ProjectId);
        entity.DeleteProjectMmeber(request.Id, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }


}
