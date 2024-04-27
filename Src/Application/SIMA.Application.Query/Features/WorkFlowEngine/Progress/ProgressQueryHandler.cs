using AutoMapper;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Query.Features.WorkFlowEngine.Progress
{
    public class ProgressQueryHandler : IQueryHandler<GetAllProgressQuery, Result<IEnumerable<GetProgressQueryResult>>>, IQueryHandler<GetProgressQuery, Result<GetProgressQueryResult>>
                                     
    {
        private readonly IMapper _mapper;
        private readonly IProgressQueryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProgressQueryHandler(IMapper mapper, IProgressQueryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetProgressQueryResult>> Handle(GetProgressQuery request, CancellationToken cancellationToken)
        {
            var actor = await _repository.FindById(request.Id);
            var result = _mapper.Map<GetProgressQueryResult>(actor);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetProgressQueryResult>>> Handle(GetAllProgressQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
