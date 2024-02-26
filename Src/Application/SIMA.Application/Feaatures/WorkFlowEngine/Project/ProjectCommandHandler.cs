using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.Project;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.Project
{
    public class ProjectCommandHandler : ICommandHandler<CreateProjectCommand, Result<long>>, ICommandHandler<DeleteProjectCommand, Result<long>>, ICommandHandler<ModifyProjectCommand, Result<long>>
        , ICommandHandler<CreateProjectGroupCommand, Result<long>>, ICommandHandler<DeleteProjectGroupCommand, Result<long>>
        , ICommandHandler<CreateProjectMemberCommand, Result<long>>, ICommandHandler<DeleteProjectMemberCommand, Result<long>>
    {
        private readonly IProjectRepository _repository;
        private readonly IWorkFlowRepository _workFlowRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectCommandHandler(IProjectRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IWorkFlowRepository workFlowRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _workFlowRepository = workFlowRepository;
        }

        public async Task<Result<long>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var arg = _mapper.Map<CreateProjectArg>(request);
                var entity = await Domain.Models.Features.WorkFlowEngine.Project.Entites.Project.New(arg);
                await _repository.Add(entity);
                await _unitOfWork.SaveChangesAsync();

                entity.AddProjectGroup(request.GroupId);
                var memberArg = _mapper.Map<List<CreateProjectMemberArg>>(request.ProjectMember);
                entity.AddProjectMember(memberArg);
                await _unitOfWork.SaveChangesAsync();

                return Result.Ok(entity.Id.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Result<long>> Handle(ModifyProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyProjectArg>(request);
            entity.Modify(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        public async Task<Result<long>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Deactive();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        public async Task<Result<long>> Handle(CreateProjectGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.ProjectId);
                entity.AddProjectGroup(request.GroupId);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(request.ProjectId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<long>> Handle(DeleteProjectGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.ProjectId);
            entity.DeactiveProjectGroup(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        public async Task<Result<long>> Handle(CreateProjectMemberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.ProjectId);
                var arg = _mapper.Map<List<CreateProjectMemberArg>>(request.ProjectMember);
                entity.AddProjectMember(arg);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(request.ProjectId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<long>> Handle(DeleteProjectMemberCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.ProjectId);
            entity.DeactiveProjectMmeber(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }


    }
}
