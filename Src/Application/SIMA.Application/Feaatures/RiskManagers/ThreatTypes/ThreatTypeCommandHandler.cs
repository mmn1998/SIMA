using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.ThreatTypes;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Args;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.ThreatTypes
{
    public class ThreatTypeCommandHandler : ICommandHandler<CreateThreatTypeCommand, Result<long>>, ICommandHandler<ModifyThreatTypeCommand, Result<long>>
    , ICommandHandler<DeleteThreatTypeCommand, Result<long>>
    {
        private readonly IThreatTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IThreatTypeDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public ThreatTypeCommandHandler(IThreatTypeRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IThreatTypeDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateThreatTypeCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateThreatTypeArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await ThreatType.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyThreatTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyThreatTypeArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteThreatTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            long userId = _simaIdentity.UserId;entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
