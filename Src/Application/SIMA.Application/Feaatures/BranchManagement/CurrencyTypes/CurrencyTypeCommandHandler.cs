using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.CurrencyTypes
{
    public class CurrencyTypeCommandHandler : ICommandHandler<CreateCurrencyTypeCommand, Result<long>>, ICommandHandler<ModifyCurrencyTypeCommand, Result<long>>, ICommandHandler<DeleteCurrencyTypeCommand, Result<long>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyTypeRepository _repository;
        private readonly ICurrencyTypeDomainService _domainService;

        public CurrencyTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICurrencyTypeRepository repository, ICurrencyTypeDomainService domainService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _domainService = domainService;
        }
        public async Task<Result<long>> Handle(CreateCurrencyTypeCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateCurrencyTypeArg>(request);
            var entity = await CurrencyType.Create(arg, _domainService);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(ModifyCurrencyTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyCurrencyTypeArg>(request);
            await entity.Modify(arg, _domainService);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(DeleteCurrencyTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            await entity.Deactive();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
    }
}
