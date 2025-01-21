using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.WageRates;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.WageRates;

public class WageRateCommandHandler : ICommandHandler<CreateWageRateCommand, Result<long>>,
    ICommandHandler<ModifyWageRateCommand, Result<long>>, ICommandHandler<DeleteWageRateCommand, Result<long>>
{
    private readonly IWageRateRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public WageRateCommandHandler(IWageRateRepository repository,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateWageRateCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateWageRateArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = WageRate.Create(arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyWageRateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyWageRateArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        entity.Modify(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteWageRateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}