using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.RiskTypes;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.RiskTypes
{
    public class RiskTypeCommandHandler : ICommandHandler<CreateRiskTypeCommand, Result<long>>, ICommandHandler<ModifyRiskTypeCommand, Result<long>>
    , ICommandHandler<DeleteRiskTypeCommand, Result<long>>
    {
        private readonly IRiskTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRiskTypeDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public RiskTypeCommandHandler(IRiskTypeRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IRiskTypeDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateRiskTypeCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateRiskTypeArgs>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await RiskType.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyRiskTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyRiskTypeArgs>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteRiskTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Delete();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
