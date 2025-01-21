using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.CurrencyOprationTypes;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Enitites;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.CurrencyOprationTypes;

public class CurrencyOprationTypeCommandHandler : ICommandHandler<CreateCurrencyOperationTypeCommand, Result<long>>, ICommandHandler<ModifyCurrencyOperationTypeCommand, Result<long>>, ICommandHandler<DeleteCurrencyOperationTypeCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrencyOprationTypeRepository _repository;
    private readonly ICurrencyOprationTypeDomainService _domainService;
    private readonly ISimaIdentity _simaIdentity;

    public CurrencyOprationTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICurrencyOprationTypeRepository repository, ICurrencyOprationTypeDomainService domainService,
        ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _domainService = domainService;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateCurrencyOperationTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCurrencyOprationTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await CurrencyOprationType.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyCurrencyOperationTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyCurrencyOprationTypeArg>(request);
        arg.ModifyBy = _simaIdentity.UserId;
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteCurrencyOperationTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
