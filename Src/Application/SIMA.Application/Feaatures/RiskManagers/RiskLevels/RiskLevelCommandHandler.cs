using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.RiskLevels;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.RiskLevels
{
    public class RiskLevelCommandHandler : ICommandHandler<CreateRiskLevelCommand, Result<long>>, ICommandHandler<ModifyRiskLevelCommand, Result<long>>
    , ICommandHandler<DeleteRiskLevelCommand, Result<long>>
    {
        private readonly IRiskLevelRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRiskLevelDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public RiskLevelCommandHandler(IRiskLevelRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IRiskLevelDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateRiskLevelCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateRiskLevelArgs>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await RiskLevel.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyRiskLevelCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyRiskLevelArgs>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteRiskLevelCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Delete();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
