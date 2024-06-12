using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskLevelMeasures;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.RiskLevelMeasures
{
    public class RiskLevelMeasureCommandHandler : ICommandHandler<CreateRiskLevelMeasureCommand, Result<long>>, ICommandHandler<ModifyRiskLevelMeasureCommand, Result<long>>,
        ICommandHandler<DeleteRiskLevelMeasureCommand, Result<long>>
    {
        private readonly IRiskLevelMeasureRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRiskLevelMeasureDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public RiskLevelMeasureCommandHandler(IRiskLevelMeasureRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IRiskLevelMeasureDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateRiskLevelMeasureCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateRiskLevelMeasureArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await RiskLevelMeasure.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyRiskLevelMeasureCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyRiskLevelMeasureArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteRiskLevelMeasureCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Delete();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
