using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowActor;

namespace SIMA.Application.Query.Features.WorkFlowEngine.WorkFlowActor
{
    public class WorkFlowActorQueryHandler : IQueryHandler<GetWorkFlowActorQuery, Result<GetWorkFlowActorQueryResult>>, IQueryHandler<GetAllWorkFlowActorsQuery, Result<List<GetWorkFlowActorQueryResult>>>
    {
        private readonly IMapper _mapper;
        private readonly IWorkFlowActorQueryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkFlowActorQueryHandler(IMapper mapper, IWorkFlowActorQueryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetWorkFlowActorQueryResult>> Handle(GetWorkFlowActorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var actor = await _repository.FindById(request.Id);
                var result = _mapper.Map<GetWorkFlowActorQueryResult>(actor);
                return Result.Ok(result);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<Result<List<GetWorkFlowActorQueryResult>>> Handle(GetAllWorkFlowActorsQuery request, CancellationToken cancellationToken)
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
    }
}
