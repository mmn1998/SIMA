using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.FinancialSuppliers;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Args;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.FinancialSuppliers
{
    public class FinancialSupplierCommandHandler : ICommandHandler<CreateFinancialSupplierCommand, Result<long>>, ICommandHandler<ModifyFinancialSupplierCommand, Result<long>>, ICommandHandler<DeleteFinancialSupplierCommand, Result<long>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFinancialSupplierRepository _repository;
        private readonly IFinancialSupplierDomainService _domainService;
        private readonly ISimaIdentity _simaIdentity;

        public FinancialSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IFinancialSupplierRepository repository, IFinancialSupplierDomainService domainService,
            ISimaIdentity simaIdentity)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _domainService = domainService;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateFinancialSupplierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var arg = _mapper.Map<CreateFinancialSupplierArg>(request);
                arg.CreatedBy = _simaIdentity.UserId;
                var entity = await FinancialSupplier.Create(arg, _domainService);
                await _repository.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(entity.Id.Value);
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }
        public async Task<Result<long>> Handle(ModifyFinancialSupplierCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyFinancialSupplierArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _domainService);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(DeleteFinancialSupplierCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
    }
}
