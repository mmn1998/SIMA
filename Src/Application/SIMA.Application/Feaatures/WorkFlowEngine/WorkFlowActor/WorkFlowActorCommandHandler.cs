using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.WorkFlowActor
{
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

        public WorkFlowActorCommandHandler(IWorkFlowActorRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateWorkFlowActorCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateWorkFlowActorArg>(request);
            var actor = Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites.WorkFlowActor.New(arg);
            await _repository.Add(actor);

            if (request.RoleId is not null)
            {
                var args = _mapper.Map<List<CreateWorkFlowActorRoleArg>>(request.RoleId);
                actor.AddActorRoles(args);
            }

            if (request.GroupId is not null)
            {
                var args = _mapper.Map<List<CreateWorkFlowActorGroupArg>>(request.GroupId);
                actor.AddActorGroups(args);
            }

            if (request.UserId is not null)
            {
                var args = _mapper.Map<List<CreateWorkFlowActorUserArg>>(request.UserId);
                actor.AddActorUsers(args);
            }

            await _unitOfWork.SaveChangesAsync();



            return Result.Ok(actor.Id.Value);

        }
        public async Task<Result<long>> Handle(ModifyWorkFlowActorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyWorkFlowActorArg>(request);
            entity.Modify(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        public async Task<Result<long>> Handle(DeleteWorkFlowActorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Deactive();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);

        }

        public async Task<Result<long>> Handle(CreateWorkFlowActorRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowActorId);
            var args = _mapper.Map<List<CreateWorkFlowActorRoleArg>>(request.RoleId);
            entity.AddActorRoles(args);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.WorkFlowActorId);
        }
        public async Task<Result<long>> Handle(DeleteWorkFlowActorRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowActorId);
            entity.DeactiveRole(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(CreateWorkFlowActorUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowActorId);
            var args = _mapper.Map<List<CreateWorkFlowActorUserArg>>(request.UserId);
            entity.AddActorUsers(args);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.WorkFlowActorId);

        }
        public async Task<Result<long>> Handle(DeleteWorkFlowActorUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowActorId);
            entity.DeactiveUser(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(CreateWorkFlowActorGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowActorId);
            var args = _mapper.Map<List<CreateWorkFlowActorGroupArg>>(request.GroupId);
            entity.AddActorGroups(args);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.WorkFlowActorId);

        }
        public async Task<Result<long>> Handle(DeleteWorkFlowActorGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowActorId);
            entity.DeactiveGroup(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(CreateRelatedWorkFlowActorEntitiesCommand request, CancellationToken cancellationToken)
        {
            var actor = await _repository.GetById(request.Id);

            if (request.RoleId is not null)
            {
                var args = _mapper.Map<List<CreateWorkFlowActorRoleArg>>(request.RoleId);
                actor.AddActorRoles(args);
            }

            if (request.GroupId is not null)
            {
                var args = _mapper.Map<List<CreateWorkFlowActorGroupArg>>(request.GroupId);
                actor.AddActorGroups(args);
            }

            if (request.UserId is not null)
            {
                var args = _mapper.Map<List<CreateWorkFlowActorUserArg>>(request.UserId);
                actor.AddActorUsers(args);
            }

            await _unitOfWork.SaveChangesAsync();



            return Result.Ok(actor.Id.Value);
        }
    }
}
