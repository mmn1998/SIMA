using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.ScenarioExecutionHistories;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Args;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Contracts;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.ScenarioExecutionHistories
{
    public class ScenarioExecutionHistoryCommandHandler : ICommandHandler<CreateScenarioExecutionHistoryCommand, Result<long>>,
    ICommandHandler<ModifyScenarioExecutionHistoryCommand, Result<long>>, ICommandHandler<DeleteScenarioExecutionHistoryCommand, Result<long>>
    {
        private readonly IScenarioExecutionHistoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public ScenarioExecutionHistoryCommandHandler(IScenarioExecutionHistoryRepository repository,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateScenarioExecutionHistoryCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateScenarioExecutionHistoryArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = ScenarioExecutionHistory.Create(arg);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(ModifyScenarioExecutionHistoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            var arg = _mapper.Map<ModifyScenarioExecutionHistoryArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(DeleteScenarioExecutionHistoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
