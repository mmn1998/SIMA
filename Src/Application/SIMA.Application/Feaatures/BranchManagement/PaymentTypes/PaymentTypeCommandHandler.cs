using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.PaymentTypes;

public class PaymentTypeCommandHandler : ICommandHandler<AddPaymentTypeCommand, Result<long>>, ICommandHandler<EditPaymentTypeCommand, Result<long>>
    , ICommandHandler<DeletePaymentTypeCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentTypeRepository _repository;
    private readonly IPaymentTypeDomainService _domainService;

    public PaymentTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IPaymentTypeRepository repository, IPaymentTypeDomainService domainService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _domainService = domainService;
    }
    public async Task<Result<long>> Handle(AddPaymentTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreatePaymentTypeArg>(request);
        var entity = await PaymentType.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(EditPaymentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyPaymentTypeArg>(request);
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeletePaymentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
