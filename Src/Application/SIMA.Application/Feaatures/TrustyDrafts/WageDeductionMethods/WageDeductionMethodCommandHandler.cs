using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.WageDeductionMethods;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.WageDeductionMethods;

public class WageDeductionMethodCommandHandler : ICommandHandler<CreateWageDeductionMethodCommand, Result<long>>,
    ICommandHandler<ModifyWageDeductionMethodCommand, Result<long>>, ICommandHandler<DeleteWageDeductionMethodCommand, Result<long>>
{
    private readonly IWageDeductionMethodRepository _repository;
    private readonly IWageDeductionMethodDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public WageDeductionMethodCommandHandler(IWageDeductionMethodRepository repository, IWageDeductionMethodDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateWageDeductionMethodCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateWageDeductionMethodArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await WageDeductionMethod.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyWageDeductionMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyWageDeductionMethodArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteWageDeductionMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}