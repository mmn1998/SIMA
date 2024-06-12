using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.RiskDegrees;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.RiskDegrees
{
    public class RiskDegreeCommandHandler : ICommandHandler<CreateRiskDegreeCommand, Result<long>>, ICommandHandler<ModifyRiskDegreeCommand, Result<long>>
    , ICommandHandler<DeleteRiskDegreeCommand, Result<long>>
    {
        private readonly IRiskDegreeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRiskDegreeDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public RiskDegreeCommandHandler(IRiskDegreeRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IRiskDegreeDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateRiskDegreeCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateRiskDegreeArgs>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await RiskDegree.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyRiskDegreeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyRiskDegreeArgs>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteRiskDegreeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Delete();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
