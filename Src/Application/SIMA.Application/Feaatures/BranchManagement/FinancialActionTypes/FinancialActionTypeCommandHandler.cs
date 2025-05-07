using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.FinancialActionTypes;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.BranchManagement.FinancialActionTypes
{
    public class FinancialActionTypeCommandHandler : ICommandHandler<CreateFinancialActionTypeCommand, Result<long>>, ICommandHandler<ModifyFinancialActionTypeCommand, Result<long>>, ICommandHandler<DeleteFinancialActionTypeCommand, Result<long>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFinancialActionTypeRepository _repository;
        private readonly IFinancialActionTypeDomainService _domainService;
        private readonly ISimaIdentity _simaIdentity;

        public FinancialActionTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IFinancialActionTypeRepository repository, IFinancialActionTypeDomainService domainService,
            ISimaIdentity simaIdentity)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _domainService = domainService;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateFinancialActionTypeCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateFinancialActionTypeArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await FinancialActionType.Create(arg, _domainService);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(ModifyFinancialActionTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyFinancialActionTypeArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _domainService);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(DeleteFinancialActionTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
    }
}
