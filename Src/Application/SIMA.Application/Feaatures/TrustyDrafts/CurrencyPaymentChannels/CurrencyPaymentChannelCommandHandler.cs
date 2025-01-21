using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.CurrencyPaymentChannels;

public class CurrencyPaymentChannelCommandHandler : ICommandHandler<CreateCurrencyPaymentChannelCommand, Result<long>>,
    ICommandHandler<ModifyCurrencyPaymentChannelCommand, Result<long>>, ICommandHandler<DeleteCurrencyPaymentChannelCommand, Result<long>>
{
    private readonly ICurrencyPaymentChannelRepository _repository;
    private readonly ICurrencyPaymentChannelDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public CurrencyPaymentChannelCommandHandler(ICurrencyPaymentChannelRepository repository, ICurrencyPaymentChannelDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateCurrencyPaymentChannelCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCurrencyPaymentChannelArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await CurrencyPaymentChannel.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyCurrencyPaymentChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyCurrencyPaymentChannelArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteCurrencyPaymentChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}