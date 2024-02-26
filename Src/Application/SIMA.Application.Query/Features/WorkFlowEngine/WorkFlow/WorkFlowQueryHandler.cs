using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;

namespace SIMA.Application.Query.Features.WorkFlowEngine.WorkFlow
{
    public class WorkFlowQueryHandler : IQueryHandler<GetAllWorkFlowsQuery, Result<List<GetWorkFlowQueryResult>>>, IQueryHandler<GetWorkFlowQuery, Result<GetWorkFlowQueryResult>>,
        IQueryHandler<GetAllStepsQuery, Result<List<GetStepQueryResult>>>, IQueryHandler<GetStepQuery, Result<GetStepQueryResult>>,
        IQueryHandler<GetAllStatesQuery, Result<List<GetStateQueryResult>>>
        , IQueryHandler<GetStateQuery, Result<GetStateQueryResult>>
        , IQueryHandler<GetWorkFlowByProjectQuery, Result<List<GetWorkFlowQueryResult>>>
        , IQueryHandler<GetStatesByWorkFlowQuery, Result<List<GetStateQueryResult>>>
         , IQueryHandler<GetStepsByWorkFlowQuery, Result<List<GetStepQueryResult>>>
    {
        private readonly IMapper _mapper;
        private readonly IWorkFlowQueryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkFlowQueryHandler(IMapper mapper, IWorkFlowQueryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<GetWorkFlowQueryResult>> Handle(GetWorkFlowQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var actor = await _repository.FindById(request.Id);
                return Result.Ok(actor);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<List<GetWorkFlowQueryResult>>> Handle(GetAllWorkFlowsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entites = await _repository.GetAll();
                return Result.Ok(entites);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<GetStepQueryResult>> Handle(GetStepQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var step = await _repository.GetStepById(request.Id);
                return Result.Ok(step);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<List<GetStepQueryResult>>> Handle(GetAllStepsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entites = await _repository.GetAllStep();
                return Result.Ok(entites);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<Result<GetStateQueryResult>> Handle(GetStateQuery request, CancellationToken cancellationToken)
        {
            var state = await _repository.GetStateById(request.Id);
            return Result.Ok(state);
        }
        public async Task<Result<List<GetStateQueryResult>>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entites = await _repository.GetAllStates();
                return Result.Ok(entites);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Result<List<GetWorkFlowQueryResult>>> Handle(GetWorkFlowByProjectQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByProjectId(request.ProjectId);
            return Result.Ok(entity);
        }

        public async Task<Result<List<GetStateQueryResult>>> Handle(GetStatesByWorkFlowQuery request, CancellationToken cancellationToken)
        {
           var res=await _repository.GetAllStatesByWorkFlowId(request.Id);
            return Result.Ok(res);
        }

        public async Task<Result<List<GetStepQueryResult>>> Handle(GetStepsByWorkFlowQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entites = await _repository.GetAllStepByWorkFlowId(request.Id);
                return Result.Ok(entites);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
