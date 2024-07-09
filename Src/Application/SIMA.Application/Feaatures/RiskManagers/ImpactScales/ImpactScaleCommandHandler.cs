using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.ImpactScales;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Args;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.ImpactScales
{
    public class ImpactScaleCommandHandler : ICommandHandler<CreateImpactScaleCommand, Result<long>>, ICommandHandler<ModifyImpactScaleCommand, Result<long>>
    , ICommandHandler<DeleteImpactScaleCommand, Result<long>>
    {
        private readonly IImpactScaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImpactScaleDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public ImpactScaleCommandHandler(IImpactScaleRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IImpactScaleDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateImpactScaleCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateImpactScaleArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await ImpactScale.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyImpactScaleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyImpactScaleArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteImpactScaleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            long userId = _simaIdentity.UserId;entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
