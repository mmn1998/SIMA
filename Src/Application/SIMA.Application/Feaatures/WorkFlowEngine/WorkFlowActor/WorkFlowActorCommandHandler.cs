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
    public class WorkFlowActorCommandHandler : ICommandHandler<CreateWorkFlowActorCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorCommand, Result<long>>, ICommandHandler<ModifyWorkFlowActorCommand, Result<long>>,
        ICommandHandler<CreateWorkFlowActorRoleCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorRoleCommand, Result<long>>,
        ICommandHandler<CreateWorkFlowActorUserCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorUserCommand, Result<long>>,
        ICommandHandler<CreateWorkFlowActorGroupCommand, Result<long>>, ICommandHandler<DeleteWorkFlowActorGroupCommand, Result<long>>
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
            try
            {
                var arg = _mapper.Map<CreateWorkFlowActorArg>(request);
                var actor = Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites.WorkFlowActor.New(arg);
                await _repository.Add(actor);
                //request.Id = new WorkFlowActorId(actor.Id);

                if (request.RoleId is not null)
                    actor.AddActorRole(request.RoleId);

                if (request.GroupId is not null)
                    actor.AddActorGroup(request.GroupId);

                if (request.UserId is not null)
                    actor.AddActorUser(request.UserId);

                await _unitOfWork.SaveChangesAsync();



                return Result.Ok(actor.Id.Value);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<long>> Handle(ModifyWorkFlowActorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.Id);
                var arg = _mapper.Map<ModifyWorkFlowActorArg>(request);
                entity.Modify(arg);
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
                entity.Deactive();
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
            try
            {
                var entity = await _repository.GetById(request.WorkFlowActorId);
                entity.AddActorRole(request.RoleId);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(request.WorkFlowActorId);
            }
            catch (Exception ex)
            {
                throw;
            }

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
            try
            {
                var entity = await _repository.GetById(request.WorkFlowActorId);
                entity.AddActorUser(request.UserId);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(request.WorkFlowActorId);
            }
            catch (Exception ex)
            {
                throw;
            }

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
            try
            {
                var entity = await _repository.GetById(request.WorkFlowActorId);
                entity.AddActorGroup(request.GroupId);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(request.WorkFlowActorId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<long>> Handle(DeleteWorkFlowActorGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowActorId);
            entity.DeactiveGroup(request.Id);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

    }
}
