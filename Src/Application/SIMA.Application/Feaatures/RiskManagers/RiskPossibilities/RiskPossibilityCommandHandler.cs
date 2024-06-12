using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.RiskPossibilities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.RiskPossibilities
{
    public class RiskPossibilityCommandHandler : ICommandHandler<CreateRiskPossibilityCommand, Result<long>>, ICommandHandler<ModifyRiskPossibilityCommand, Result<long>>
    , ICommandHandler<DeleteRiskPossibilityCommand, Result<long>>
    {
        private readonly IRiskPossibilityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRiskPossibilityDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public RiskPossibilityCommandHandler(IRiskPossibilityRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IRiskPossibilityDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateRiskPossibilityCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateRiskPossibilityArgs>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await RiskPossibility.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyRiskPossibilityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyRiskPossibilityArgs>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteRiskPossibilityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Delete();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
