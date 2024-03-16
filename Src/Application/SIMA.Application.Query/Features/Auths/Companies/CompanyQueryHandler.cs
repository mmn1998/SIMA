using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.Auths.Companies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Companies;

namespace SIMA.Application.Query.Features.Auths.Companies
{
    public class CompanyQueryHandler : IQueryHandler<GetCompanyByIdQuery, Result<GetCompanyQueryResult>>, IQueryHandler<GetAllCompanyQuery, Result<IEnumerable<GetCompanyQueryResult>>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyQueryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyQueryHandler(IMapper mapper, ICompanyQueryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetCompanyQueryResult>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindById(request.Id);
            var result = _mapper.Map<GetCompanyQueryResult>(entity);
            return Result.Ok(result);
        }

        public async Task<Result<IEnumerable<GetCompanyQueryResult>>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }
    }
}
