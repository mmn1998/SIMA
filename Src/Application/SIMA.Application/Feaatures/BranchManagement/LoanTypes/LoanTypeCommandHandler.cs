using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.LoanTypes;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.LoanTypes;

public class LoanTypeCommandHandler : ICommandHandler<CreateLoanTypeCommand, Result<long>>,
    ICommandHandler<ModifyLoanTypeCommand, Result<long>>, ICommandHandler<DeleteLoanTypeCommand, Result<long>>
{
    private readonly ILoanTypeRepository _repository;
    private readonly ILoanTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public LoanTypeCommandHandler(ILoanTypeRepository repository, ILoanTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateLoanTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateLoanTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await LoanType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyLoanTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyLoanTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteLoanTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}