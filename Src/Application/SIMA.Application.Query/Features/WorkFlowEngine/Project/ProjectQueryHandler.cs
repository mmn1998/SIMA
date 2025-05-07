using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Project;

namespace SIMA.Application.Query.Features.WorkFlowEngine.Project
{
    public class ProjectQueryHandler : IQueryHandler<GetAllProjectsQuery, Result<IEnumerable<GetProjectQueryResult>>>, IQueryHandler<GetProjectQuery, Result<GetProjectQueryResult>>
                                     , IQueryHandler<GetProjectsByDomainQuery, Result<List<GetProjectQueryResult>>>
    {
        private readonly IMapper _mapper;
        private readonly IProjectQueryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectQueryHandler(IMapper mapper, IProjectQueryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetProjectQueryResult>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var actor = await _repository.FindById(request.Id);
            var result = _mapper.Map<GetProjectQueryResult>(actor);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetProjectQueryResult>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<List<GetProjectQueryResult>>> Handle(GetProjectsByDomainQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByDomainId(request.DomainId);
            return Result.Ok(entity);
        }
    }
}
