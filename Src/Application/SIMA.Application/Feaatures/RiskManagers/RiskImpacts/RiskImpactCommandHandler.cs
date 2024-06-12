using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.RiskImpacts;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.RiskImpacts
{
    public class RiskImpactCommandHandler : ICommandHandler<CreateRiskImpactCommand, Result<long>>, ICommandHandler<ModifyRiskImpactCommand, Result<long>>
    , ICommandHandler<DeleteRiskImpactCommand, Result<long>>
    {
        private readonly IRiskImpactRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRiskImpactDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public RiskImpactCommandHandler(IRiskImpactRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IRiskImpactDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateRiskImpactCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateRiskImpactArgs>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await RiskImpact.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyRiskImpactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyRiskImpactArgs>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteRiskImpactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Delete();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
