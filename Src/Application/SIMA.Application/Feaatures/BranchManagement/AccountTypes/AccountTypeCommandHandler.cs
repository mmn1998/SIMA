using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.AccountTypes;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.AccountTypes;

public class AccountTypeCommandHandler : ICommandHandler<CreateAccountTypeCommand, Result<long>>,
    ICommandHandler<ModifyAccountTypeCommand, Result<long>>, ICommandHandler<DeleteAccountTypeCommand, Result<long>>
{
    private readonly IAccountTypeRepository _repository;
    private readonly IAccountTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public AccountTypeCommandHandler(IAccountTypeRepository repository, IAccountTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateAccountTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAccountTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await AccountType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyAccountTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAccountTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteAccountTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}